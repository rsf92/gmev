using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
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
			army = new GameObject ("Soldier");
			army.AddComponent<Rigidbody> ();
			army.AddComponent<BoxCollider>();
			break;
		case 3:
		case 4:
			army = new GameObject ("Knight");
			army.AddComponent<Rigidbody> ();
			army.AddComponent<BoxCollider>();

			break;
		case 5:
			army = new GameObject ("Dragon");
			army.AddComponent<Rigidbody> ();
			army.AddComponent<BoxCollider>();
			break;
		}
			
			
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
