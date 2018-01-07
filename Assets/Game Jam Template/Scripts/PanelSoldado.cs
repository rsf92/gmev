using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelSoldado : MonoBehaviour {


	private bool isVisible;		
	public  GameObject panel;						
	public static bool option;

	//Awake is called before Start()
	void Awake()
	{		
		panel.SetActive(false);
		option = false;
	}


	public  void DoUnvisible()
	{
		option=true;

		
		/*Dropdown drpSoldados = GameObject.Find ("drpSoldados").GetComponent<Dropdown>();
		int valueDrop = (int)drpSoldados.value;

				//me.add_units (1);
				//main_behavior.units_hold--;
		Casilla me = Tile.getCasilla();
		me.add_units (valueDrop);
		main_behavior.units_hold = main_behavior.units_hold-valueDrop;	
		Debug.Log ("Añadida unidad, quedan " + main_behavior.units_hold);
		main_behavior.reparte = main_behavior.units_hold > 0;
		*/
		panel.SetActive(false);
	}

	public  void DoVisible()
	{
		option=false;
		panel.SetActive(true);
	}
}
