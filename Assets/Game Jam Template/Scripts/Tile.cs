using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	static Casilla origen = null;
	public GameObject army;
	Casilla me;
	// Use this for initialization
	IEnumerator Start ()
	{
		me = null;
		int i = 0;
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
		army = Instantiate (army);
        
	}

	void paintUnits ()
	{
		int unidades = me.getUnits ();
		DestroyObject (army);       
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
		default:
			return;
		}

		army.transform.position = transform.position;
		army = Instantiate (army);
	}

	GameObject movable ()
	{
		GameObject object3d;
		object3d = ((GameObject)Resources.Load ("skeleton_swordsman", typeof(GameObject)));
		object3d.transform.position = origen.GetPosition();
		object3d = Instantiate (object3d);

		return object3d;
	}

	// Update is called once per frame
	void Update ()
	{
		
	}


	IEnumerator OnMouseUp ()
	{
		int ret;
		GameObject temporal;
		if (((string)main_behavior.jugadores [main_behavior.index_player]).Contains (me.getOwner ()) == true && Tile.origen == null) {
			Debug.Log ("Elegida la casilla");
			origen = me;
			/*User selects this tile*/       
		} else if (((string)main_behavior.jugadores [main_behavior.index_player]).Contains (me.getOwner ()) == true && Tile.origen != null) {
			if (origen != me) {
				Debug.Log ("Origen Iniciales" + origen.getUnits ());
				ret = origen.move_Units (me);
				Debug.Log ("Origen Finales" + origen.getUnits ());
				paintUnits ();
				Vector3 direction = me.GetPosition () - origen.GetPosition ();
				if (ret == 0) {
					temporal = movable ();
					for (int i = 0; i < 10; i++) {
						temporal.transform.position += direction / 10;
						yield return new WaitForSeconds (0.1f);
					}
					DestroyObject (temporal);
				}

			}
			origen = null;  
			Debug.Log ("Deseleccionada la casilla");
			/*User deselects this tile or moves to another tile*/

		} else if (((string)main_behavior.jugadores [main_behavior.index_player]).Contains (me.getOwner ()) != true && Tile.origen != null) {
			/*Attack!*/
			int unidades = 1;
			Vector3 direction = me.GetPosition () - origen.GetPosition ();
			temporal = movable ();
			for (int i = 0; i < 10; i++) {
				temporal.transform.position += direction / 10;
				yield return new WaitForSeconds (0.1f);
			}
			DestroyObject (temporal);
			if (me.getUnits () == 0) {
				me.conquer (origen.getOwner (), unidades);
			} else {
				Debug.Log ("Not implemented yet");
			}
		} else if (((string)main_behavior.jugadores [main_behavior.index_player]).Contains (me.getOwner ()) != true && Tile.origen == null) {
			/*Do nothing, it's an error*/
			Debug.Log ("No se pueden seleccionar casillas rivales!");
		}

            
	}
}
