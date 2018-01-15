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
		diceCloneParam = GameObject.Instantiate(orignalDice) as GameObject;	
		diceCloneParam.transform.position  =  Vector3.zero;
		return diceCloneParam;
	}
		
	public List<GameObject> getFromPool(int units, Vector3 position){
		List<GameObject> lista= new List<GameObject> ();
		

		for(int i =0; i < units ; i++){
			GameObject dado= dados.ElementAt(i);
			dado.transform.position = position;
			lista.Add(dado);
		}
		
		

		return lista;

	}
}
