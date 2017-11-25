using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTurn : MonoBehaviour {

	public void TaskOnClick() {
			main_behavior.index_player++;
			main_behavior.index_player%=main_behavior.jugadores.Count;
			Debug.Log("Jugador " + main_behavior.index_player);
	}
}
