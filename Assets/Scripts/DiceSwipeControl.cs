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
			poolDados = new PoolDados(orignalDice);
		}

		void Start ()
		{
			
			
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
			/*GameObject newDice = new GameObject();

			for (int i =0; i < NUMERO_DADOS; i++)
			{
				diceCloneList.Add(generateDice (newDice));
			}*/
			diceCloneList = poolDados.getFromPool(NUMERO_DADOS,new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z));

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
			diceCloneParam.GetComponent<Rigidbody>().useGravity= true;
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
			
			// wail for all dices reduces their velocity
			foreach (GameObject diceCloneParam in diceCloneParams)
			{
				while (diceCloneParam.GetComponent<Rigidbody>().velocity.magnitude > 0.05f) {
					yield return 0;
				}
			}
			

			foreach (GameObject diceCloneParam in diceCloneParams)
			{
				Time.timeScale = 1.0f;
				diceCount = diceCloneParam.GetComponent<Dice>().GetDiceCount ();
				results.Add(diceCount);
				diceCloneParam.GetComponent<Rigidbody>().constraints= RigidbodyConstraints.None;
				print ("se toma numero del dado");
			}
			
			yield return new WaitForSeconds (3.0f);

			///set active camera
			mainCamera.enabled = true;
			dicePlayCam.transform.position =initPos;
			dicePlayCam.transform.eulerAngles = initRot;
			dicePlayCam.enabled = false;
			
			//initialize dices
			foreach (GameObject diceCloneParam in diceCloneParams)
			{
				
				diceCloneParam.GetComponent<Rigidbody>().useGravity= false;
				diceCloneParam.transform.position = Vector3.zero;
			}

			isDiceThrowable = false;
			print ("termina el getdicecount");
			
		}

	public static void set_num_dices(int n_dices){
		if(n_dices > 0)
			NUMERO_DADOS = n_dices;
	}

}
