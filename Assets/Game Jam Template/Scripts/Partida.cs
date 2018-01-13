using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Data = System.Collections.Generic.KeyValuePair<string, int>;
	
public class Partida
{
	private string tipo;
	private int num_casillas;
	private bool necesita_casilla;
	private bool tiene_casilla;

	public Partida (string tipo, int num_jugadores)
	{
		this.tipo = tipo;
		if (tipo.Equals("Conquest")) {
			this.num_casillas = 10 + (69/num_jugadores);
			this.necesita_casilla = false;
			this.tiene_casilla = false;
		} else if (tipo.Equals("Dominion")) {
			this.num_casillas = 69;
			this.necesita_casilla = false;
			this.tiene_casilla = false;
		} else { //Skirmish hay que cambiar cosas. Primero hay que decir qué provincia tienen que conquistar y luego añadirlo aquí
			this.necesita_casilla = true;
			this.tiene_casilla = false;
		}
	}


	public string imprimeDatos ()
	{
		if (this.num_casillas == 0) {
			return "Partida de tipo " + this.tipo + ": necesita una casilla especifica para ganar. Aún no está implementado";
		} else {
			return "Partida de tipo " + this.tipo + ": numero de casillas para ganar: " + num_casillas;
		}
	}

	public int casillas_jugador(){
		String jugador = "Jugador" + (main_behavior.index_player + 1);
		int casillas = 0;
		foreach (Casilla c in main_behavior.casillas) {
			if (c.getOwner ().Equals (PlayerPrefs.GetString (jugador))) {
				casillas++;
			}
		}
		return casillas;
	}

	public void FinPartida ()
	{
		var jug = new List<Data> ();
		int casillasj1 = 0;
		int casillasj2 = 0;
		int casillasj3 = 0;
		int casillasj4 = 0;
			
		foreach (Casilla c in main_behavior.casillas) {
			if (c.getOwner ().Equals (PlayerPrefs.GetString ("Jugador1"))) {
				casillasj1++;
			} else if (c.getOwner ().Equals (PlayerPrefs.GetString ("Jugador2"))) {
				casillasj2++;
			} else if (c.getOwner ().Equals (PlayerPrefs.GetString ("Jugador3"))) {
				casillasj3++;
			} else {
				casillasj4++;
			}
		}
			
		Debug.Log ("jugador1: " + casillasj1);
		Debug.Log ("jugador2: " + casillasj2);
		Debug.Log ("jugador3: " + casillasj3);
		Debug.Log ("jugador4: " + casillasj4);

		if (casillasj1 == this.num_casillas) {
			Debug.Log ("Fin del juego, gana jugador " + PlayerPrefs.GetString ("Jugador1"));
		} else if (casillasj2 == this.num_casillas) {
			Debug.Log ("Fin del juego, gana jugador " + PlayerPrefs.GetString ("Jugador2"));
		} else if (casillasj3 == this.num_casillas) {
			Debug.Log ("Fin del juego, gana jugador " + PlayerPrefs.GetString ("Jugador3"));
		} else if (casillasj4 == this.num_casillas) {
			Debug.Log ("Fin del juego, gana jugador " + PlayerPrefs.GetString ("Jugador4"));
		} else {
		}
		
	}

}


