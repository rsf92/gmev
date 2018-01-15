using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool {

	private List<GameObject> soldados = null;
	private List<GameObject> caballeros = null;
	private List<GameObject> dragones = null;

	public Pool(string casa){

		soldados = new List<GameObject> ();
		GameObject soldado = ((GameObject)Resources.Load (casa+"/Soldado", typeof(GameObject)));
		soldado.transform.position= Vector3.zero;
		for (int i = 0; i < 30; i++) {
			GameObject aux = GameObject.Instantiate (soldado);
			aux.GetComponent<Animator>().runtimeAnimatorController = RuntimeAnimatorController.Instantiate(Resources.Load(casa+"/Soldado")) as RuntimeAnimatorController;
			//Debug.Log ("Instanciacion de controlador soldado: "+ aux.GetComponent<Animator>().runtimeAnimatorController.animationClips);
			soldados.Add (aux);
		}

		caballeros = new List<GameObject> ();
		GameObject caballero = ((GameObject)Resources.Load (casa+"/Caballero", typeof(GameObject)));
		caballero.transform.position= Vector3.zero;
		for (int i = 0; i < 30; i++) {
			GameObject aux = GameObject.Instantiate (caballero);
			aux.GetComponent<Animator>().runtimeAnimatorController = RuntimeAnimatorController.Instantiate(Resources.Load(casa+"/Caballero")) as RuntimeAnimatorController;
			Debug.Log ("Instanciacion de controlador caballero: "+ aux.GetComponent<Animator>().runtimeAnimatorController.animationClips);
			caballeros.Add (aux);
		}

		dragones = new List<GameObject> ();
		GameObject dragon = ((GameObject)Resources.Load (casa+"/Dragon", typeof(GameObject)));
		dragon.transform.position= Vector3.zero;
		for (int i = 0; i < 30; i++) {
			GameObject aux = GameObject.Instantiate (dragon);
			Debug.Log ("Instanciacion de dragon: "+ aux.name);
			aux.GetComponent<Animator>().runtimeAnimatorController = RuntimeAnimatorController.Instantiate(Resources.Load(casa+"/Dragon")) as RuntimeAnimatorController;
			//Debug.Log ("Instanciacion de controlador dragon: "+ aux.GetComponent<Animator>().runtimeAnimatorController.animationClips);
			dragones.Add (aux);
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

		GameObject objeto2 = GameObject.Instantiate(lista.ToArray()[0]);
		objeto2.transform.position= Vector3.zero;
		lista.Add (objeto2);

		return objeto2;

	}
}
