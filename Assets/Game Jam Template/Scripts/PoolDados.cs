using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PoolDados {

	private List<GameObject> dados = null;
	private int NUMERO_DADOS = 5;
	public GameObject orignalDice;

	public PoolDados(GameObject dice){
		orignalDice  = dice;
		dados = new List<GameObject> ();
		
		GameObject newDice = new GameObject();

		for (int i =0; i < NUMERO_DADOS; i++)
		{
			dados.Add(generateDice (newDice));
		}
	}
	
	GameObject generateDice (GameObject diceCloneParam)
	{
		diceCloneParam = GameObject.Instantiate (orignalDice, Vector3.zero, 
		Quaternion.Euler (Random.Range (0, 180), Random.Range (0, 180), Random.Range (0, 180))) as GameObject;	
		return diceCloneParam;
	}
		
	public List<GameObject> getFromPool(int units){
		List<GameObject> lista= new List<GameObject> ();
		
		for(int i =0; i < units ; i++){
			lista.Add(dados.ElementAt(i));
		}

		return lista;

	}
}
