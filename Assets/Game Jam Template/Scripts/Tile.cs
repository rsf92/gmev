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
		Debug.Log (army);
	}

	void paintUnits ()
	{
		int unidades = me.getUnits ();
     
		army.instantiate (unidades, this); 
	}

	Army movable ()
	{
		Army myarmy = new Army ();
		myarmy.instantiate (1,this); 
		return myarmy;
	}

	// Update is called once per frame
	void Update ()
	{
		
	}


	public static void reset_origen(){
		origen = null;
	}

	IEnumerator OnMouseUp ()
	{
		int ret;
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
				ret = origen.me.move_Units (me);
				Debug.Log ("Origen Finales" + origen.me.getUnits ());
				Debug.Log ("Destino Finales" + me.getUnits ());

				origen.paintUnits ();

				if (ret == 0) {
					
					temporal = movable ();
					temporal.move (me);

					temporal.deinstantiate ();
				}
				paintUnits ();
			}
			Tile.reset_origen ();
			Debug.Log ("Deseleccionada la casilla");
			/*User deselects this tile or moves to another tile*/

		} else if (((string)main_behavior.jugadores [main_behavior.index_player]).Contains (me.getOwner ()) != true && Tile.origen != null) {
			/*Attack!*/

			army.move (me);

			if (me.getUnits () == 0) {
				me.conquer (origen.me.getOwner (), me.getUnits());
			} else {
				Debug.Log ("Not implemented yet");
			}
			Tile.reset_origen ();
			Debug.Log ("Deseleccionada la casilla");
		} else if (((string)main_behavior.jugadores [main_behavior.index_player]).Contains (me.getOwner ()) != true && Tile.origen == null) {
			/*Do nothing, it's an error*/
			Debug.Log ("No se pueden seleccionar casillas rivales!");
		}
		return null;
		//StartOptions.partida.FinPartida ();   
	}
}
