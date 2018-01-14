using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool {

	private List<GameObject> soldados = null;
	private List<GameObject> caballeros = null;
	private List<GameObject> dragones=null;

	public Pool(string casa){

		soldados = new List<GameObject> ();

		GameObject soldado = ((GameObject)Resources.Load (casa+"/Soldado", typeof(GameObject)));
		soldado.GetComponent<Animator>().runtimeAnimatorController = RuntimeAnimatorController.Instantiate(Resources.Load(casa+"/Soldado")) as RuntimeAnimatorController;
		soldado.transform.position= Vector3.zero;;
		for (int i = 0; i < 70; i++) {
			soldados.Add (GameObject.Instantiate(soldado));
		}

		caballeros = new List<GameObject> ();
		GameObject caballero = ((GameObject)Resources.Load (casa+"/Caballero", typeof(GameObject)));
		caballero.transform.position= Vector3.zero;
		caballero.GetComponent<Animator>().runtimeAnimatorController = RuntimeAnimatorController.Instantiate(Resources.Load(casa+"/Caballero")) as RuntimeAnimatorController;
		for (int i = 0; i < 70; i++) {
			caballeros.Add (GameObject.Instantiate(caballero));
		}

		dragones = new List<GameObject> ();
		GameObject dragon = ((GameObject)Resources.Load (casa+"/Dragon", typeof(GameObject)));
		dragon.transform.position= Vector3.zero;
		dragon.GetComponent<Animator>().runtimeAnimatorController = RuntimeAnimatorController.Instantiate(Resources.Load(casa+"/Dragon")) as RuntimeAnimatorController;
		for (int i = 0; i < 70; i++) {
			dragones.Add (GameObject.Instantiate(dragon));
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
			
			if (objeto.transform.position == Vector3.zero) {
				return objeto;
			}
		}

		return null;

	}
}
