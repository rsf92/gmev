using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTurn : MonoBehaviour {

	public void TaskOnClick() {
		//Cambio de jugador
		main_behavior.reparte = false;
		main_behavior.index_player++;
		main_behavior.turno++;
		main_behavior.index_player%=main_behavior.jugadores.Count;
		main_behavior.estado = (main_behavior.turno / main_behavior.jugadores.Count) % 2 != 0;
		if (main_behavior.estado == false) {//Solo se puede repartir en turno de movimiento
			main_behavior.reparte = true;
			main_behavior.units_hold [main_behavior.index_player] += StartOptions.partida.casillas_jugador () / 3;
		}
		//Debug.Log("Jugador " + main_behavior.index_player);
		LogText.log ("Jugador " + main_behavior.index_player);

		Tile.reset_origen ();

	}
}
