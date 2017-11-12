using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    static Casilla origen = null;
	public GameObject army;
	Casilla me;
	// Use this for initialization
	IEnumerator Start () {
		me = null;
		int i = 0;
		yield return new WaitForSeconds (1.0f);
		do {
			
			me = main_behavior.getCasilla (this.name);
			i++;
		} while(me == null);
        
		int unidades = me.getUnits();
       
        
        
        
		switch (unidades) {
		case 1:
		case 2:
            army = ((GameObject)Resources.Load("skeleton_swordsman", typeof(GameObject)));
			break;
		case 3:
		case 4:
	        army = ((GameObject)Resources.Load("skeleton_swordsman", typeof(GameObject)));
			break;
		case 5:
            army = ((GameObject)Resources.Load("skeleton_swordsman", typeof(GameObject)));
			break;
		}

		army.transform.position = transform.position;
        army = Instantiate(army);
        
	}
	
    void paintUnits(){

        int unidades = me.getUnits();
        
        DestroyObject(army);       
		switch (unidades) {
		case 1:
		case 2:
            army = ((GameObject)Resources.Load("skeleton_swordsman", typeof(GameObject)));
			break;
		case 3:
		case 4:
	        army = ((GameObject)Resources.Load("skeleton_swordsman", typeof(GameObject)));
			break;
		case 5:
            army = ((GameObject)Resources.Load("skeleton_swordsman", typeof(GameObject)));
			break;
		}

		army.transform.position = transform.position;
        army = Instantiate(army);
    }

	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseUp() {
        if (((string)main_behavior.jugadores[main_behavior.index_player]).Contains(me.getOwner()) == true && Tile.origen == null){
            Debug.Log("Elegida la casilla");
            origen = me;
            /*User selects this tile*/       
        } else if(((string)main_behavior.jugadores[main_behavior.index_player]).Contains(me.getOwner()) == true && Tile.origen != null){
                   if(origen != me){
                        Debug.Log(me.getUnits());
                        origen.move_Units(me);
                        Debug.Log(me.getUnits());
                        paintUnits();
                    }
                    origen = null;  
                    Debug.Log("Deseleccionada la casilla") ;
                /*User deselects this tile or moves to another tile*/

        }else if(((string)main_behavior.jugadores[main_behavior.index_player]).Contains(me.getOwner()) != true && Tile.origen != null){
            /*Attack!*/
        }else if(((string)main_behavior.jugadores[main_behavior.index_player]).Contains(me.getOwner()) != true && Tile.origen == null){
            /*Do nothing, it's an error*/
        }

            
    }
}
