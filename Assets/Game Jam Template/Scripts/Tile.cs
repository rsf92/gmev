using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	static Tile origen = null;
	public GameObject army;
	Casilla me;
	// Use this for initialization
	IEnumerator Start ()
	{
		me = null;
		int i = 0;
		GameObject go;
		yield return new WaitForSeconds (0.01f);
		do {
			
			me = main_behavior.getCasilla (this.name);
			i++;
		} while(me == null);
        
		int unidades = me.getUnits ();
        
		switch (unidades) {
		case 1:
		case 2:
			army = ((GameObject)Resources.Load ("skeleton_swordsman", typeof(GameObject)));
			break;
		case 3:
		case 4:
			army = ((GameObject)Resources.Load ("skeleton_swordsman", typeof(GameObject)));
			break;
		case 5:
			army = ((GameObject)Resources.Load ("skeleton_swordsman", typeof(GameObject)));
			break;
		}

		army.transform.position = transform.position;
		go = Instantiate (army);
		DestroyObject (army);
		army = go;
        
	}

	void paintUnits ()
	{
		int unidades = me.getUnits ();
		GameObject go;
		DestroyObject (army);       
		switch (unidades) {
		case 0: 
			return;
		case 1:
		case 2:
			army = ((GameObject)Resources.Load ("skeleton_swordsman", typeof(GameObject)));
			break;
		case 3:
		case 4:
			army = ((GameObject)Resources.Load ("skeleton_swordsman", typeof(GameObject)));
			break;
		case 5:
		default:
			army = ((GameObject)Resources.Load ("skeleton_swordsman", typeof(GameObject)));
			break;
		}

		army.transform.position = transform.position;
		go = Instantiate (army);
		DestroyObject (army);
		army = go;
	}

	GameObject movable ()
	{
		GameObject object3d;
		object3d = ((GameObject)Resources.Load ("skeleton_swordsman", typeof(GameObject)));
		object3d.transform.position = origen.me.GetPosition();
		object3d = Instantiate (object3d);

		return object3d;
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
		GameObject temporal;

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
				Vector3 direction = me.GetPosition () - origen.me.GetPosition ();
				if (ret == 0) {
					temporal = movable ();
					for (int i = 0; i < 10; i++) {
						temporal.transform.position += direction / 10;
						yield return new WaitForSeconds (0.1f);
					}
					paintUnits ();
					DestroyObject (temporal);
				}

			}
			Tile.reset_origen ();
			Debug.Log ("Deseleccionada la casilla");
			/*User deselects this tile or moves to another tile*/

		} else if (((string)main_behavior.jugadores [main_behavior.index_player]).Contains (me.getOwner ()) != true && Tile.origen != null) {
			/*Attack!*/
			int unidades = 1;
			Vector3 direction = me.GetPosition () - origen.me.GetPosition ();
			temporal = movable ();
			for (int i = 0; i < 10; i++) {
				temporal.transform.position += direction / 10;
				yield return new WaitForSeconds (0.1f);
			}
			DestroyObject (temporal);
			if (me.getUnits () == 0) {
				me.conquer (origen.me.getOwner (), unidades);
			} else {
				Debug.Log ("Not implemented yet");
			}
			Tile.reset_origen ();
			Debug.Log ("Deseleccionada la casilla");
		} else if (((string)main_behavior.jugadores [main_behavior.index_player]).Contains (me.getOwner ()) != true && Tile.origen == null) {
			/*Do nothing, it's an error*/
			Debug.Log ("No se pueden seleccionar casillas rivales!");
		}

		//StartOptions.partida.FinPartida ();   
	}
}
