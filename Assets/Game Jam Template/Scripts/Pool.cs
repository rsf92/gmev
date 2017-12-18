using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool {

	private List<GameObject> soldados = null;
	private List<GameObject> caballeros = null;
	private List<GameObject> dragones=null;

	public Pool(){

		soldados = new List<GameObject> ();

		GameObject soldado = ((GameObject)Resources.Load ("skeleton_swordsman", typeof(GameObject)));
		soldado.SetActive (false);
		for (int i = 0; i < 150; i++) {
			soldados.Add (soldado);
		}

		caballeros = new List<GameObject> ();
		GameObject caballero = ((GameObject)Resources.Load ("skeleton_swordsman", typeof(GameObject)));
		caballero.SetActive (false);
		for (int i = 0; i < 150; i++) {
			caballeros.Add (caballero);
		}

		dragones = new List<GameObject> ();
		GameObject dragon = ((GameObject)Resources.Load ("skeleton_swordsman", typeof(GameObject)));
		dragon.SetActive (false);
		for (int i = 0; i < 80; i++) {
			dragones.Add (dragon);
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
