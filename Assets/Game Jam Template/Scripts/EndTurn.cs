using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTurn : MonoBehaviour {

	public void TaskOnClick() {
		//Cambio de jugador
		main_behavior.index_player++;
		main_behavior.index_player%=main_behavior.jugadores.Count;
		main_behavior.reparte = true;
		main_behavior.units_hold[main_behavior.index_player] += StartOptions.partida.casillas_jugador () / 3;
		Debug.Log("Jugador " + main_behavior.index_player);
		Tile.reset_origen ();

	}
}
