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

		if (army == null)
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

	public IEnumerator move(Casilla destino){

		Vector3 direction = destino.GetPosition () - army.transform.position;

		for (int i = 0; i < 10; i++) {
			army.transform.position += direction / 10;
			yield return new WaitForSeconds (0.1f);
		}
	}

	public bool activeSelf(){
		return army.activeSelf;
	}
}
