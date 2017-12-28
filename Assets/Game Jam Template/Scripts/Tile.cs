using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	static Tile origen = null;
	public Army army;
	Casilla me;
	// Use this for initialization
	IEnumerator Start ()
	{
		me = null;
		int i = 0;
		army = new Army ();

		yield return new WaitForSeconds (0.01f);
		do {
			me = main_behavior.getCasilla (this.name);
			i++;
		} while(me == null);

		int unidades = me.getUnits ();

		army.instantiate (unidades, this);
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

	IEnumerator OnMouseUp ()
	{
		bool ret;
		Army temporal;

		if (origen != null && (me.isAdyacent(origen.me) != true && this != origen)) {
			Debug.Log ("Sólo te puedes mover a casillas adyacentes!");
		}
		else if (((string)main_behavior.jugadores [main_behavior.index_player]).Contains (me.getOwner ()) == true && Tile.origen == null) {
			if (me.getUnits () == 0) {
				Debug.Log ("No se puede elegir como origen una casilla vacía!");
			} else {
				Debug.Log ("Elegida la casilla");
				origen = this;
			}
		/*User selects this tile*/
		} else if (((string)main_behavior.jugadores [main_behavior.index_player]).Contains (me.getOwner ()) == true && Tile.origen != null) {
			if ((origen.me != me)) {
				Debug.Log ("Origen Iniciales" + origen.me.getUnits ());
				Debug.Log ("Destino Iniciales" + me.getUnits ());
				ret =origen.me.put_on_hold (1);
				if (ret == true) {
					origen.me.move_Units (me);
					Debug.Log ("Origen Finales" + origen.me.getUnits ());
					Debug.Log ("Destino Finales" + me.getUnits ());

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
			do {
				numero_de_dados = temporal.getUnits ();
				unidades = me.getUnits ();
				if (unidades == 0) {
					me.conquer (origen.me.getOwner (), me.getUnits ());

				} else {

					if (unidades > 2)
						unidades = 2;
							
					numero_de_dados += unidades;

					DiceSwipeControl.set_num_dices (numero_de_dados);
					GameObject diceControl = GameObject.Find ("SwipeController");
					diceControl.GetComponent<DiceSwipeControl> ().manualStart ();
					
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
						bool def=true;

						for (int i = 0; i < breakpoint; i++) {
							
							for (int j = breakpoint; j < breakpoint*2; j++) {
								if(resultados[i] >= resultados[j]){
									def=false;
									break;
								}
							}
									if(def == false){
										me.kill_unit();
									}else{
										temporal.kill_unit();
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
				me.conquer (origen.me.getOwner(),temporal.getUnits());
			}
			temporal.deinstantiate ();

			paintUnits ();
			Tile.reset_origen ();
			Debug.Log ("Deseleccionada la casilla");
		} else if (((string)main_behavior.jugadores [main_behavior.index_player]).Contains (me.getOwner ()) != true && Tile.origen == null) {
			/*Do nothing, it's an error*/
			Debug.Log ("No se pueden seleccionar casillas rivales!");
		}
		yield return 0;
		//StartOptions.partida.FinPartida ();
	}
}
