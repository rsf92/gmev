using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Army {
	private GameObject army;
	private int units;

	public Army(){
		units = 0;
		army = null;
	}

	public void instantiate(int units, Tile position){

		this.units = units;
		switch (units) {
			case 0:
				return;
			case 1:
			case 2:
				army = main_behavior.mypool.getFromPool(1);
				break;
			case 3:
			case 4:
				army = main_behavior.mypool.getFromPool(2);
				break;
			case 5:
			default:
				army = main_behavior.mypool.getFromPool(3);
				break;
		}
			
		army.transform.position = position.transform.position;
		Debug.Log ("Se instancia en " +army.transform.position);
	}

	public void deinstantiate(){
		army.transform.position= Vector3.zero;
	}
		
		
	public void move(Vector3 direction){

		Debug.Log (direction);

		Debug.Log (army.transform.position);
		army.transform.position += direction / 10;

	}


	public bool activeSelf(){
		return army.activeSelf;
	}

	public Vector3 getpos(){
		return army.transform.position;
	}
}
