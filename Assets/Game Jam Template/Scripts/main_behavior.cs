using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

public class main_behavior : MonoBehaviour
{

	static ArrayList casas = null;
	static public ArrayList jugadores = null;
	static public ArrayList casillas = null;
	static public int index_player;//Indice del jugador que tiene el turno.
	static public Pool mypool = null;
	static public int units_hold=0;
	static public bool reparte = true;

    

	//Se usa para inicializar la partida
	void Start ()
	{

		/**Leer información del jugador*/

		jugadores = new ArrayList ();
		casas = new ArrayList ();
		casillas = new ArrayList ();
		mypool = new Pool ();
		if (PlayerPrefs.HasKey ("Jugador1")) {
			jugadores.Add (PlayerPrefs.GetString ("Jugador1"));
			casas.Add ("Baratheon");
		}

		if (PlayerPrefs.HasKey ("Jugador2")) {
			jugadores.Add (PlayerPrefs.GetString ("Jugador2"));
			casas.Add ("Lannister");
		}

		if (PlayerPrefs.HasKey ("Jugador3")) {
			jugadores.Add (PlayerPrefs.GetString ("Jugador3"));
			casas.Add ("Stark");
		}

		if (PlayerPrefs.HasKey ("Jugador4")) {
			jugadores.Add (PlayerPrefs.GetString ("Jugador4"));
			casas.Add ("Targaryen");
		}
			
		/*Rellena la información de las casillas*/
		GameObject[] allObjects = GameObject.FindGameObjectsWithTag ("Provincias");
	

		foreach (GameObject object3d in allObjects) {
			/*Extract name*/
			string name = object3d.name;
			string owner;

			float prob = Random.Range (0f, 1f);
			float probJ1 = 0f;
			float probJ2 = 0f;
			float probJ3 = 0f;
			float probJ4 = 0f;
			/*Use probability to assign tile to a player using a russian roulette approach*/
			switch (casas.Count) {
			case 2:
				probJ1 = 0.5f;
				probJ2 = 1f;

				break;
			case 3:
				probJ1 = 0.33f;
				probJ2 = 0.67f;
				probJ3 = 1f;
				break;
			case 4:
				probJ1 = 0.25f;
				probJ2 = 0.5f;
				probJ3 = 0.75f;
				probJ4 = 1f;
				break;
			}

			if (prob < probJ1){
				owner = jugadores [0].ToString ();
				units_hold++; //Así el jugador inicial puede asignar soldados también
			}else if (prob < probJ2)
				owner = jugadores [1].ToString ();
			else if (prob < probJ3)
				owner = jugadores [2].ToString ();
			else
				owner = jugadores [3].ToString ();
			int units = (int)Random.Range (1.0f, 3.9f);

			casillas.Add (new Casilla (name, owner, units));
		}
		units_hold /= 3; //El número bueno ya
		/*Cargamos el grafo*/

		StreamReader sr = new StreamReader ("Assets/Grafo.txt");
		while (sr.Peek () >= 0) {
			string linea = sr.ReadLine ();
            
			string[] provincias = linea.Split ('\\');
			Casilla actual = null;
            
			List<Casilla> adyacent = new List<Casilla> ();

			foreach (Casilla casilla in casillas) {
				string provincia = casilla.getName ();   
				if (provincia.Contains (provincias [0])) {
					    
					actual = casilla;
					break;
				}
			}

			foreach (Casilla casilla in casillas) {
				string provincia = casilla.getName (); 
                
				for (int i = 2; i < provincias.Length; i++) {
					if (provincia.Contains (provincias [i])) {
						adyacent.Add (casilla);

					}
				}
			}

			actual.setAdyacents (adyacent);

		}

		//Establece el estado inicial
		index_player = 0;

	}

	void Update ()
	{
		
	}

	public static Casilla getCasilla (string name)
	{

		if (main_behavior.casillas != null) {
			foreach (Casilla casilla in main_behavior.casillas) {
				
				if (casilla.getName ().Contains (name)) {
					return casilla;
				}
			}
		}
		return null;
	}
}
