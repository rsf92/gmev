using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelSoldado : MonoBehaviour {


	private bool isVisible;		
	public  GameObject panel;						

	//Awake is called before Start()
	void Awake()
	{		
		DoUnvisible();
	}


	public  void DoUnvisible()
	{
		panel.SetActive(false);
	}

	public  void DoVisible()
	{
		panel.SetActive(true);
		
	}
}
