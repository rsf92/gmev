using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class DiceSwipeControl : MonoBehaviour
{
		// Static Instance of the Dice
		public static DiceSwipeControl Instance;
    	
		// Orignal Dice
		public GameObject orignalDice;
		
		//dice play view camera...
		public Camera dicePlayCam;
		public Camera mainCamera;
		public Camera cameraB;
		//Can Throw Dice
		public bool isDiceThrowable = true;
        	public Transform diceCarrom;

		private static Vector3 initPos;
		private static int NUMERO_DADOS = 5;  
		private List<GameObject> diceCloneList;


		private static Vector3 initRot;
		public static List<int> results ;
		private PoolDados poolDados;


		void Awake ()
		{
			Instance = this;
			
		}

		void Start ()
		{
			
			//poolDados = new PoolDados(orignalDice);
		}

		void Update ()
		{
			
		}

		public void manualStart(){
			// Actual object instance			
			Instance = this;
			isDiceThrowable = true;
			GameObject camAux =  GameObject.Find("Main Camera");
			mainCamera =  camAux.GetComponent<Camera>();
			mainCamera.enabled = false;
				
			GameObject camAuxB =  GameObject.Find("CameraB");
			cameraB =  camAuxB.GetComponent<Camera>();
			cameraB.enabled = false;

			///set active camera
			dicePlayCam.enabled = true;
			

			//initialize dice list
			diceCloneList = new List<GameObject>() ;
			results = new List<int>();
			GameObject newDice = new GameObject();

			for (int i =0; i < NUMERO_DADOS; i++)
			{
				diceCloneList.Add(generateDice (newDice));
			}
			//diceCloneList = poolDados.getFromPool(NUMERO_DADOS);

				Vector3 currentPos = new Vector3(dicePlayCam.transform.position.x,dicePlayCam.transform.position.y,dicePlayCam.transform.position.z);
				initPos = currentPos;
				Vector3 currentRot = new Vector3(dicePlayCam.transform.eulerAngles.x,dicePlayCam.transform.eulerAngles.y,dicePlayCam.transform.eulerAngles.z);
				initRot = currentRot;

				Vector3 newPos = dicePlayCam.ScreenToWorldPoint (new Vector3(currentPos.x,currentPos.y,Mathf.Clamp(currentPos.y/10,5,70)));
				newPos.y = Mathf.Clamp(newPos.y, -114.5f, 100);
				newPos = dicePlayCam.ScreenToWorldPoint (currentPos);

				foreach (GameObject diceCloneParam in diceCloneList)
				{
					enableTheDice (diceCloneParam);
					addForce (newPos, diceCloneParam);
				}
				
				StartCoroutine (getDiceCount (diceCloneList));
			
		}

		void addForce (Vector3 lastPos, GameObject diceCloneParam)
		{
                	diceCloneParam.GetComponent<Rigidbody>().AddTorque(Vector3.Cross(lastPos, initPos) * 1000, ForceMode.Impulse);
			lastPos.y += 12;
			diceCloneParam.GetComponent<Rigidbody>().AddForce (((lastPos - initPos).normalized) * (Vector3.Distance (lastPos, initPos)) * 30 * diceCloneParam.GetComponent<Rigidbody>().mass);
		
		}

		void enableTheDice (GameObject diceCloneParam)
		{		
			diceCloneParam.transform.rotation = Quaternion.Euler (Random.Range (0, 180), Random.Range (0, 180), Random.Range (0, 180));
		}

		GameObject generateDice (GameObject diceCloneParam)
		{
				diceCloneParam = Instantiate (orignalDice, new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z), 
				Quaternion.Euler (Random.Range (0, 180), Random.Range (0, 180), Random.Range (0, 180))) as GameObject;	
				return diceCloneParam;
		}

		IEnumerator getDiceCount (List<GameObject> diceCloneParams)
		{	
			//dice resultant number
			int diceCount;

			Time.timeScale = 1.0f;	
			//wait for dice to stop
			yield return new WaitForSeconds (3.0f);
			print ("se detienen los dados");
			// wail for all dices reduces their velocity
			foreach (GameObject diceCloneParam in diceCloneParams)
			{
				while (diceCloneParam.GetComponent<Rigidbody>().velocity.magnitude > 0.05f) {
					yield return 0;
				}
			}
			
			//change camera position

			/*Time.timeScale = 0.2f;
			float startTime = Time.time;
			Vector3 risePos = dicePlayCam.transform.position;
			Vector3 setPos = new Vector3 (diceCarrom.position.x-5f, diceCarrom.transform.position.y -10f, diceCarrom.position.z-27f);
			float speed = 0.18f;
			float fracComplete = 0;
			
			while (Vector3.Distance(dicePlayCam.transform.position,setPos)>0.5f) 
            		{
				Vector3 center = (risePos + setPos) * 0.5f;
				center -= new Vector3 (0, 2, -1);
				Vector3 riseRelCenter = risePos - center;
				Vector3 setRelCenter = setPos - center;
				
				if (fracComplete > 0.85f && fracComplete < 1f) {
					speed += Time.deltaTime * 0.3f;
					Time.timeScale -= Time.deltaTime * 4f;
				} 
					
				dicePlayCam.transform.position = Vector3.Slerp (riseRelCenter, setRelCenter, fracComplete);
				dicePlayCam.transform.position += center;
				dicePlayCam.transform.LookAt (diceCarrom);
				fracComplete = (Time.time - startTime) / speed;

				yield return 0;
			}*/

			foreach (GameObject diceCloneParam in diceCloneParams)
			{
				Time.timeScale = 1.0f;
				diceCount = diceCloneParam.GetComponent<Dice>().GetDiceCount ();
				results.Add(diceCount);

				diceCloneParam.GetComponent<Rigidbody>().constraints= RigidbodyConstraints.None;
			}
			
			yield return new WaitForSeconds (3.0f);

			///set active camera
			mainCamera.enabled = true;
			dicePlayCam.transform.position =initPos;
			dicePlayCam.transform.eulerAngles = initRot;
			dicePlayCam.enabled = false;
			
			foreach (GameObject diceCloneParam in diceCloneParams)
			{
				GameObject.Destroy(diceCloneParam);
			}
			isDiceThrowable = false;
			
			
		}

	public static void set_num_dices(int n_dices){
		if(n_dices > 0)
			NUMERO_DADOS = n_dices;
	}

}
