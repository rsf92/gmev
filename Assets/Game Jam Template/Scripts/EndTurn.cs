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

		string nombre_jugador = main_behavior.jugadores[main_behavior.index_player].ToString();
		string nombre_casa = main_behavior.casas[main_behavior.index_player].ToString();
		string color;
		if (nombre_casa == "Baratheon") {
			color = "amarillo";
		} else if (nombre_casa == "Lannister") {
			color = "rojo";
		} else if (nombre_casa == "Stark") {
			color = "verde";
		}else {
			color = "azul";
		}
		LogText.log ("Es el turno de " + nombre_jugador + ", representando a la casa " + nombre_casa + " con el color "+ color + ".\nPara comenzar el turno tienes " + main_behavior.units_hold[main_behavior.index_player] + " unidades nuevas para colocar en tus territorios.\nSelecciona un territorio.");
		Tile.reset_origen ();

	}
}
