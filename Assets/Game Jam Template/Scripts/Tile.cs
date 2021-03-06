using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tile : MonoBehaviour
{
	public static Tile origen = null;
	public Army army;
	public Casilla me;
	//Material material;
	Renderer rend;
	PanelSoldado panel;
	private bool panelStart;
	private bool ret;
	private static int valueDropdown ;
	private static bool performing = false;
	public static bool droppeddown = true;
	TextMeshProUGUI textNumArmyPro; // Texto de TextMeshPro que muestra el numero de soldados en la casilla
	private Camera cameraB;
	private Camera mainCamera;
	private Vector3 initPosCamera;
	private Vector3 initRotCamera;

	// Use this for initialization
	IEnumerator Start ()
	{
		me = null;
		int i = 0;
		army = new Army ();
		panelStart = true;
		valueDropdown = 0;
		GameObject camAux =  GameObject.Find("Main Camera");
		mainCamera =  camAux.GetComponent<Camera>();
		mainCamera.enabled = true;
		

		///set active camera
		GameObject camAuxB =  GameObject.Find("CameraB");
		cameraB =  camAuxB.GetComponent<Camera>();
		cameraB.enabled = false;
		Vector3 currentPos = new Vector3(cameraB.transform.position.x,cameraB.transform.position.y,cameraB.transform.position.z);
		initPosCamera = currentPos;
		Vector3 currentRot =new Vector3(cameraB.transform.eulerAngles.x,cameraB.transform.eulerAngles.y,cameraB.transform.eulerAngles.z);
		initRotCamera = currentRot;		
		
		yield return new WaitForSeconds (0.01f);
		do {
			me = main_behavior.getCasilla (this.name);

			if (GameObject.Find ("TextMeshPro Text") != null) {
				textNumArmyPro = this.GetComponentInChildren<TextMeshProUGUI> ();
				updateArmyCountText ();
			}

			i++;
		} while(me == null);

		int unidades = me.getUnits ();

		army.instantiate (unidades, this);

		set_color ();
		
	}
	
	

	void set_color(){
		rend = GetComponent<Renderer> ();
		rend.enabled = true;
		// Color/Material de la casilla segun la casa propietaria
		if (me.getOwner ().Equals (PlayerPrefs.GetString ("Jugador1"))) {
			rend.material = Resources.Load("Materials/Houses/Baratheon", typeof(Material)) as Material;
		} else if (me.getOwner ().Equals (PlayerPrefs.GetString ("Jugador2"))) {
			rend.material = Resources.Load("Materials/Houses/Lannister", typeof(Material)) as Material;
		} else if (me.getOwner ().Equals (PlayerPrefs.GetString ("Jugador3"))) {
			rend.material = Resources.Load("Materials/Houses/Stark", typeof(Material)) as Material;
		} else if (me.getOwner ().Equals (PlayerPrefs.GetString ("Jugador4"))) {
			rend.material = Resources.Load("Materials/Houses/Targaryen", typeof(Material)) as Material;
		} else {
			rend.material = Resources.Load("Materials/NoHouse", typeof(Material)) as Material;
		}
	}

	/* Actualiza el numero que aparece encima de las casillas */
	void updateArmyCountText() {
		if (textNumArmyPro != null) {
			if (me.getUnits () > 0) {
				textNumArmyPro.SetText (me.getUnits ().ToString ());
			} else {
				textNumArmyPro.SetText ("");
			}
		}
	}

	void updateCount(){
		textNumArmyPro = this.GetComponentInChildren<TextMeshProUGUI> ();
		updateArmyCountText ();
	}

	void paintUnits ()
	{
		int unidades = me.getUnits ();
		army.deinstantiate ();
		army.instantiate (unidades, this);
	}

	Army movable (int units=1)
	{
		Army myarmy = new Army ();
		myarmy.instantiate (units,origen);

		return myarmy;
	}

	// Update is called once per frame
	void Update ()
	{
		
	}
		
	private Vector3 getDirection (Casilla origen){
		return me.GetPosition () - origen.GetPosition ();
	}


	public static void reset_origen(){
		origen = null;
	}

	public void closePanelB(){
		Dropdown drpSoldados = GameObject.Find ("drpSoldadosB").GetComponent<Dropdown>();
		valueDropdown = (int)drpSoldados.value;

		GameObject panelControl =GameObject.Find ("PanelControllerB");
		panel = panelControl.GetComponent<PanelSoldado>();
		panel.DoUnvisible();
		panelStart = false;
		Tile.droppeddown = true;

	}

	public void closePanel(){
		
		Dropdown drpSoldados = GameObject.Find ("drpSoldados").GetComponent<Dropdown>();
		int valueDrop = (int)drpSoldados.value;

		origen.me.add_units (valueDrop);
		main_behavior.units_hold[main_behavior.index_player] = main_behavior.units_hold[main_behavior.index_player]-valueDrop;	

		LogText.log ("Añadidas " + valueDrop + " unidades, quedan " + main_behavior.units_hold[main_behavior.index_player]);

		main_behavior.reparte = main_behavior.units_hold[main_behavior.index_player] > 0;

		GameObject panelControl =GameObject.Find ("PanelController");
		panel = panelControl.GetComponent<PanelSoldado>();
		panel.DoUnvisible();
		panelStart = false;
		Tile.droppeddown = true;
		origen.paintUnits ();
		origen.updateCount ();
		Tile.reset_origen ();

	}

	IEnumerator OnMouseUp ()
	{
		if (Tile.performing == false && Tile.droppeddown == true) {
			
			mainCamera.enabled = false;
			cameraB.enabled = true;

			Army temporal;
			Tile.performing = true;
			if (origen != null && (me.isAdyacent (origen.me) != true && this != origen)) {
				LogText.log ("Sólo te puedes mover a casillas adyacentes!");

			} else if (((string)main_behavior.jugadores [main_behavior.index_player]).Contains (me.getOwner ()) == true && Tile.origen == null) {
				if (main_behavior.reparte == true) {
					Tile.droppeddown = false;
					origen = this;

					List<string> m_DropOptions = new List<string> ();
					for (int i = 0; i <= main_behavior.units_hold [main_behavior.index_player]; i++) {
						m_DropOptions.Add (i.ToString ());				
					}

					GameObject panelControl = GameObject.Find ("PanelController");

					panel = panelControl.GetComponent<PanelSoldado> ();
					panel.DoVisible ();
		
					valueDropdown = 0;

					Dropdown drpSoldados = GameObject.Find ("drpSoldados").GetComponent<Dropdown> ();

					//Clear the old options of the Dropdown menu
					drpSoldados.ClearOptions ();

					//Add the options created in the List above
					drpSoldados.AddOptions (m_DropOptions);
					drpSoldados.value = 0;
				} else {
					if (me.getUnits () == 0) {
						LogText.log ("No se puede elegir como origen una casilla vacía!");

					} else {
						LogText.log ("Elegida la casilla de la que mover");

						origen = this;
					}
				}
				/*User selects this tile*/
			} else if (((string)main_behavior.jugadores [main_behavior.index_player]).Contains (me.getOwner ()) == true && Tile.origen != null) {
				if (main_behavior.estado != false) {
					LogText.log ("En este turno no se pueden mover tropas!");
				} else if ((origen.me != me)) {
					Tile.droppeddown = false;

					List<string> m_DropOptions = new List<string> ();
					for (int i = 0; i <= origen.me.getUnits (); i++) {
						m_DropOptions.Add (i.ToString ());				
					}				

					GameObject panelControl = GameObject.Find ("PanelControllerB");
					panel = panelControl.GetComponent<PanelSoldado> ();
					panel.DoVisible ();
		
					valueDropdown = 0;

					Dropdown drpSoldados = GameObject.Find ("drpSoldadosB").GetComponent<Dropdown> ();
					//Clear the old options of the Dropdown menu
					drpSoldados.ClearOptions ();
					//Add the options created in the List above
					drpSoldados.AddOptions (m_DropOptions);
					drpSoldados.value = 0;

					yield return new WaitForSeconds (1.2f);

					if (valueDropdown == 0) {
						yield return new WaitForSeconds (0.3f);

						bool stop = true;
						while (stop) {
							if (valueDropdown > 0)
								stop = false;
							else {
								
								yield return new WaitForSeconds (0.2f);
							}
						}
					}
					//print ("value drop " + valueDropdown);

					ret = origen.me.put_on_hold (valueDropdown);
					if (ret == true) {
						//print ("x origen "+origen.transform.position.x);
						Vector3 newPos = new Vector3 (me.objeto3d.transform.position.x + 80, cameraB.transform.position.y - 20, me.objeto3d.transform.position.z);
						Vector3 newRot = new Vector3 (172, 90, 180);
						cameraB.transform.position = newPos;
						cameraB.transform.eulerAngles = newRot;
						cameraB.transform.LookAt (me.objeto3d.transform);

						origen.me.move_Units (me);

						origen.updateCount ();
						origen.paintUnits ();

						temporal = movable (valueDropdown);

						Vector3 dirOrigen = getDirection (origen.me);
						float angle = Mathf.Atan2 (dirOrigen.x, dirOrigen.z) * Mathf.Rad2Deg;

						temporal.rotate (angle);

						temporal.playMove ();
						for (int i = 0; i < 20; i++) {
							temporal.move (dirOrigen);
							yield return new WaitForSeconds (0.05f);
						}
						temporal.resetRotation ();

						temporal.deinstantiate ();
						paintUnits ();
						
						cameraB.transform.position = initPosCamera;
						cameraB.transform.eulerAngles = initRotCamera;
						cameraB.enabled = false;
						mainCamera.enabled = true;
						

					}	

				}
				Tile.reset_origen ();

				/*User deselects this tile or moves to another tile*/

			} else if (((string)main_behavior.jugadores [main_behavior.index_player]).Contains (me.getOwner ()) != true && Tile.origen != null) {
				/*Attack!*/


				if (main_behavior.estado != true) {
					LogText.log ("En este turno no se pueden atacar territorios!");
				} else {
					int unidades = origen.me.getUnits ();
					int numero_de_dados = 0;
					if (unidades > 3)
						unidades = 3;

					temporal = movable (unidades);

					origen.me.put_on_hold (unidades);
					origen.paintUnits ();
					origen.updateCount ();
					Vector3 dirOrigen = getDirection (origen.me);
					float angle = Mathf.Atan2 (dirOrigen.x, dirOrigen.z) * Mathf.Rad2Deg;

					temporal.rotate (angle);

					Vector3 newPos = new Vector3 (me.objeto3d.transform.position.x + 80, cameraB.transform.position.y - 20, me.objeto3d.transform.position.z);
					Vector3 newRot = new Vector3 (172, 90, 180);
					cameraB.transform.position = newPos;
					cameraB.transform.eulerAngles = newRot;
					cameraB.transform.LookAt (me.objeto3d.transform);

					for (int i = 0; i < 10; i++) {
						temporal.move (dirOrigen);
						yield return new WaitForSeconds (0.1f);
					}


					unidades = me.getUnits ();
					GameObject diceControl = GameObject.Find ("SwipeController");
					LogText.log ("Espera... Lanzando Dados... Suerte!!!");
					do {
						numero_de_dados = temporal.getUnits ();

						if (unidades != 0) {

							if (unidades > 2)
								unidades = 2;

							numero_de_dados += unidades;

							DiceSwipeControl.set_num_dices (numero_de_dados);

							diceControl.GetComponent<DiceSwipeControl> ().manualStart ();
							army.put_on_hold (unidades);
							//Recovering dices result
							yield return new WaitForSeconds (10.0f);
							bool stop = true;	
							print ("antes de recuperar ");
							List<int> resultados = DiceSwipeControl.results;
							int repet = 1; 
							print ("antes if " + resultados.Count);
							if (resultados == null || resultados.Count != numero_de_dados) {
								print ("en el  if ");
								while (stop) {

									yield return new WaitForSeconds (0.2f);
									resultados = DiceSwipeControl.results;
									if (resultados != null && resultados.Count == numero_de_dados) {
										stop = false;
									}


									if (repet == 5)
										stop = false;							
									repet++;


								}
								print ("resultado if tile " + resultados.Count);
								
								if(  resultados == null || resultados.Count == 0 ){
									resultados = new List<int>(new int[] {1,1,1,1,1});
									
								}

							} else {
								print ("resultado tile " + resultados.Count + " " + numero_de_dados);
								int atacante = temporal.getUnits ();
								int breakpoint = atacante > unidades ? unidades : atacante;
								int[] attacker = new int[breakpoint];
								int[] defender = new int[breakpoint];
								bool def = true;

								for (int i = 0; i < breakpoint; i++) {

									for (int j = breakpoint; j < breakpoint * 2; j++) {
										if (resultados [i] >= resultados [j]) {
											def = false;
											break;
										}
									}
									if (def == false) {
										
										me.kill_unit ();
										unidades--;
									} else {
										temporal.kill_unit ();
									}

									def = true;
								}
							}

						}
						
					} while(temporal.getUnits () > 0 && me.getUnits () > 0);
					
					string msj = " ";
					if (temporal.getUnits () <= 0) {
						
						army.playAttack();
						LogText.log ("Has perdido la batalla");
						msj = " No has podido conquistar el territorio y has perdido a los soldados con los que has luchado.";

					} else {
						
						temporal.playAttack();
						LogText.log ("Has ganado la batalla");


						me.conquer (origen.me.getOwner (), temporal.getUnits ());
						msj = "Enhorabuena, has conquistado un territorio más.\nTienes " + StartOptions.partida.casillas_jugador() + " de los " + StartOptions.partida.casillas_para_fin() + " que hacen falta para ganar.";
						set_color ();
					}
					me.reset_hold ();
					temporal.resetRotation ();
					temporal.deinstantiate ();

					paintUnits ();
					Tile.reset_origen ();
					LogText.log (msj + "\nSi quieres puedes volver a atacar, selecciona la casilla desde la que hacerlo.");

					cameraB.enabled = false;
					cameraB.transform.position = initPosCamera;
					cameraB.transform.eulerAngles = initRotCamera;
					mainCamera.enabled = true;
				}



			} else if (((string)main_behavior.jugadores [main_behavior.index_player]).Contains (me.getOwner ()) != true && Tile.origen == null) {
				
				LogText.log ("No se pueden seleccionar casillas rivales!");

			} else {
				if(main_behavior.estado == false)
					LogText.log ("No se puede atacar en turno de movimiento!");
				else
					LogText.log ("No se puede mover en turno de ataque!");
			}
			Tile.performing = false;
			updateCount ();
			cameraB.enabled = false;
			cameraB.transform.position =initPosCamera;
			cameraB.transform.eulerAngles = initRotCamera;
			mainCamera.enabled = true;
			
		} else {
			LogText.warning ("Performing action");

		}
		yield return 0;
		StartOptions.partida.FinPartida ();
	}
}
