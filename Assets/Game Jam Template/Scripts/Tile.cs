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

	// Use this for initialization
	IEnumerator Start ()
	{
		me = null;
		int i = 0;
		army = new Army ();
		panelStart = true;
		valueDropdown = 0;

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

		//me.add_units (1);
		//main_behavior.units_hold--;
		me.add_units (valueDrop);
		main_behavior.units_hold[main_behavior.index_player] = main_behavior.units_hold[main_behavior.index_player]-valueDrop;	

		Debug.Log ("Añadida unidad, quedan " + main_behavior.units_hold[main_behavior.index_player]);
		main_behavior.reparte = main_behavior.units_hold[main_behavior.index_player] > 0;

		GameObject panelControl =GameObject.Find ("PanelController");
		panel = panelControl.GetComponent<PanelSoldado>();
		panel.DoUnvisible();
		panelStart = false;
		Tile.droppeddown = true;
		paintUnits ();
		updateCount ();
	}


	

	IEnumerator OnMouseUp ()
	{
		if (Tile.performing == false && Tile.droppeddown == true) {
			
			Army temporal;
			Tile.performing = true;
			if (origen != null && (me.isAdyacent (origen.me) != true && this != origen)) {
				Debug.Log ("Sólo te puedes mover a casillas adyacentes!");
			} else if (((string)main_behavior.jugadores [main_behavior.index_player]).Contains (me.getOwner ()) == true && Tile.origen == null) {
				if (main_behavior.reparte == true) {
					Tile.droppeddown = false;
					List<string> m_DropOptions = new List<string> ();
					for (int i = 0; i <= main_behavior.units_hold[main_behavior.index_player]; i++) {
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
						Debug.Log ("No se puede elegir como origen una casilla vacía!");
					} else {
						Debug.Log ("Elegida la casilla");
						origen = this;
					}
				}
				/*User selects this tile*/
			} else if (((string)main_behavior.jugadores [main_behavior.index_player]).Contains (me.getOwner ()) == true && Tile.origen != null) {
				if ((origen.me != me)) {
					Tile.droppeddown = false;
					Debug.Log ("Origen Iniciales" + origen.me.getUnits ());
					Debug.Log ("Destino Iniciales" + me.getUnits ());
					print ("adyacente");

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

					yield return new WaitForSeconds (6.2f);

					if (valueDropdown == 0) {
						yield return new WaitForSeconds (1.3f);
				
						bool stop = true;
						while (stop) {
							if (valueDropdown > 0)
								stop = false;
							else
								yield return new WaitForSeconds (1.2f);
						}
					}
					print ("value drop " + valueDropdown);

					ret = origen.me.put_on_hold (valueDropdown);
					if (ret == true) {
						origen.me.move_Units (me);
						Debug.Log ("Origen Finales" + origen.me.getUnits ());
						Debug.Log ("Destino Finales" + me.getUnits ());
						origen.updateCount ();
						origen.paintUnits ();

						temporal = movable ();
						Vector3 dir = getDirection (origen.me);
						for (int i = 0; i < 20; i++) {
							temporal.move (dir);
							yield return new WaitForSeconds (0.05f);
						}

						temporal.deinstantiate ();
						paintUnits ();

					}	

				}
				Tile.reset_origen ();
				Debug.Log ("Deseleccionada la casilla");
				/*User deselects this tile or moves to another tile*/

			} else if (((string)main_behavior.jugadores [main_behavior.index_player]).Contains (me.getOwner ()) != true && Tile.origen != null) {
				/*Attack!*/
				int unidades = origen.me.getUnits ();
				int numero_de_dados = 0;
				if (unidades > 3)
					unidades = 3;

				temporal = movable (unidades);



				origen.me.put_on_hold (unidades);
				origen.paintUnits ();
				Vector3 dir = getDirection (origen.me);

				for (int i = 0; i < 10; i++) {
					temporal.move (dir);
					yield return new WaitForSeconds (0.1f);
				}
				unidades = me.getUnits ();
				do {
					numero_de_dados = temporal.getUnits ();

					if (unidades == 0) {
						me.conquer (origen.me.getOwner (), me.getUnits ());
						set_color ();
					} else {

						if (unidades > 2)
							unidades = 2;

						numero_de_dados += unidades;

						DiceSwipeControl.set_num_dices (numero_de_dados);
						GameObject diceControl = GameObject.Find ("SwipeController");
						diceControl.GetComponent<DiceSwipeControl> ().manualStart ();
						army.put_on_hold(unidades);
						//Recovering dices result
						yield return new WaitForSeconds (10.0f);
						bool stop = true;	

						List<int> resultados = DiceSwipeControl.results;
						if (resultados == null || resultados.Count != numero_de_dados) {

							while (stop) {

								yield return new WaitForSeconds (0.2f);
								resultados = DiceSwipeControl.results;
								if (resultados != null && resultados.Count == numero_de_dados) {
									stop = false;
									foreach (int resu in resultados) {
										Debug.Log ("resu " + resu);			
									}
								}
							}
							print ("resultado if tile " + resultados.Count);
						} else {
							print ("resultado tile " + resultados.Count + " " + numero_de_dados);

							int breakpoint = resultados.Count / 2;
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
				} while(temporal.getUnits () != 0 && me.getUnits () != 0);

				if (temporal.getUnits () == 0) {
					Debug.Log ("Pierdes");
				} else {
					Debug.Log ("Ganas");
					me.conquer (origen.me.getOwner (), temporal.getUnits ());
					set_color ();
				}
				me.reset_hold ();
				temporal.deinstantiate ();

				paintUnits ();
				Tile.reset_origen ();
				Debug.Log ("Deseleccionada la casilla");
			} else if (((string)main_behavior.jugadores [main_behavior.index_player]).Contains (me.getOwner ()) != true && Tile.origen == null) {
				/*Do nothing, it's an error*/
				Debug.Log ("No se pueden seleccionar casillas rivales!");
			}
			Tile.performing = false;
			updateCount ();
		} else {
			Debug.Log ("Performing action");
		}
		yield return 0;
		//StartOptions.partida.FinPartida ();
	}
}
