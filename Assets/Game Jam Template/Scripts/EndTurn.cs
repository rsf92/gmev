using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTurn : MonoBehaviour {

	public void TaskOnClick() {
		//Cambio de jugador
		main_behavior.index_player++;
		main_behavior.index_player%=main_behavior.jugadores.Count;
		//Establecimiento de repartición
		if(main_behavior.reparte == true)
			main_behavior.units_hold = 0;
		main_behavior.reparte = true;
		main_behavior.units_hold += StartOptions.partida.casillas_jugador ();
		Debug.Log("Jugador " + main_behavior.index_player);
		Tile.reset_origen ();

	}
}
