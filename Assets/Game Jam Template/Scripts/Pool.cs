using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool {

	private List<GameObject> soldados = null;
	private List<GameObject> caballeros = null;
	private List<GameObject> dragones=null;

	public Pool(){

		soldados = new List<GameObject> ();

		for (int i = 0; i < 150; i++) {
			soldados.Add (((GameObject)Resources.Load ("skeleton_swordsman", typeof(GameObject))));
		}

		caballeros = new List<GameObject> ();

		for (int i = 0; i < 150; i++) {
			caballeros.Add (((GameObject)Resources.Load ("skeleton_swordsman", typeof(GameObject))));
		}

		dragones = new List<GameObject> ();

		for (int i = 0; i < 80; i++) {
			dragones.Add (((GameObject)Resources.Load ("skeleton_swordsman", typeof(GameObject))));
		}
	}

	public GameObject getFromPool(int Unittype){
		List<GameObject> lista;

		switch (Unittype) {
			case 1:
				lista = soldados;
				break;
			case 2:
				lista = caballeros;
				break;
			case 3:
				lista = dragones;
				break;
			default:
				return null;
		}

		foreach(GameObject objeto in lista){
			if (objeto.activeSelf == false) {
				return objeto;
			}
		}

		return null;
			
	}
}
