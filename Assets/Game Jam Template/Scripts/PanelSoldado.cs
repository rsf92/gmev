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
		panel.SetActive(false);
	}

	public  void DoVisible()
	{
		option=false;
		panel.SetActive(true);
	}
}
