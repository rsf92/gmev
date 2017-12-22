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

		if (instantiated() == false)
			return;
		
		army.transform.position = position.transform.position;
		army.SetActive (true);
	}

	public void deinstantiate(){
		army.SetActive (false);
	}

	public bool instantiated(){
		return army != null;
	}

	private Vector3 getDirection (Casilla destino){
		return destino.GetPosition () - army.transform.position;
	}
		
	public void move(Casilla destino){

		Vector3 direction = getDirection (destino);
		Debug.Log (direction);
		for (int i = 0; i < 10; i++) {
			
			army.transform.position += direction / 10;
			this.wait (0.1f);
		}
	}

	private void wait(float time){
		do{
			time -= Time.deltaTime;
		}while(time > 0);
	}

	public bool activeSelf(){
		return army.activeSelf;
	}

	public Vector3 getpos(){
		return army.transform.position;
	}
}
