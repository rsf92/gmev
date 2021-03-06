﻿using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartOptions : MonoBehaviour {



	public int sceneToStart = 1;										//Index number in build settings of scene to load if changeScenes is true
	public bool changeScenes=true;											//If true, load a new scene when Start is pressed, if false, fade out UI and continue in single scene
	public bool changeMusicOnStart;										//Choose whether to continue playing menu music or start a new music clip


	[HideInInspector] public bool inMainMenu = true;					//If true, pause button disabled in main menu (Cancel in input manager, default escape key)
	[HideInInspector] public Animator animColorFade; 					//Reference to animator which will fade to and from black when starting game.
	[HideInInspector] public Animator animMenuAlpha;					//Reference to animator that will fade out alpha of MenuPanel canvas group
	public AnimationClip fadeColorAnimationClip;		//Animation clip fading to color (black default) when changing scenes
	[HideInInspector] public AnimationClip fadeAlphaAnimationClip;		//Animation clip fading out UI elements alpha


	private PlayMusic playMusic;										//Reference to PlayMusic script
	private float fastFadeIn = .01f;									//Very short fade time (10 milliseconds) to start playing music immediately without a click/glitch
	private ShowPanels showPanels;										//Reference to ShowPanels script on UI GameObject, to show and hide panels

	static public Partida partida;

	void Awake()
	{
		//Get a reference to ShowPanels attached to UI object
		showPanels = GetComponent<ShowPanels> ();

		//Get a reference to PlayMusic attached to UI object
		playMusic = GetComponent<PlayMusic> ();
	}


	public void StartButtonClicked()
	{
		//If changeMusicOnStart is true, fade out volume of music group of AudioMixer by calling FadeDown function of PlayMusic, using length of fadeColorAnimationClip as time. 
		//To change fade time, change length of animation "FadeToColor"
		if (changeMusicOnStart) 
		{
			playMusic.FadeDown(fadeColorAnimationClip.length);
		}

		PlayerPrefs.DeleteAll ();

		GameObject inputFieldGo = GameObject.Find("InpCasa1");
		InputField inputFieldCo = inputFieldGo.GetComponent<InputField>();
		string jugador1= inputFieldCo.text.Trim();

		inputFieldGo = GameObject.Find("InpCasa2");
		inputFieldCo = inputFieldGo.GetComponent<InputField>();
		string jugador2= inputFieldCo.text.Trim();

		inputFieldGo = GameObject.Find("InpCasa3");
		inputFieldCo = inputFieldGo.GetComponent<InputField>();
		string jugador3= inputFieldCo.text.Trim();

		inputFieldGo = GameObject.Find("InpCasa4");
		inputFieldCo = inputFieldGo.GetComponent<InputField>();
		string jugador4= inputFieldCo.text.Trim();
		int cuenta = 0;
		if (jugador1.Length > 0) {
			PlayerPrefs.SetString("Jugador1", jugador1);
			cuenta++;
		}
		if (jugador2.Length > 0) {
			PlayerPrefs.SetString("Jugador2", jugador2);
			cuenta++;
		}
		if (jugador3.Length > 0) {
			PlayerPrefs.SetString("Jugador3", jugador3);
			cuenta++;
		}
		if (jugador4.Length > 0) {
			PlayerPrefs.SetString("Jugador4", jugador4);
			cuenta++;
		}
		
		Text txtMsg= GameObject.Find("TxtMsg").GetComponent<Text>();
		txtMsg.text = "";
				
		if (cuenta > 1) {
			if (      (jugador1.ToString().Equals( jugador2.ToString()) && (jugador1.Length > 0 &&jugador2.Length > 0) )  
				|| (jugador1.ToString().Equals(jugador3.ToString())  && (jugador1.Length > 0 && jugador3.Length > 0) )
				|| (jugador1.ToString().Equals( jugador4.ToString())  && (jugador1.Length > 0 &&jugador4.Length > 0) )
				|| (jugador2.ToString().Equals( jugador3.ToString())  && (jugador2.Length > 0 &&jugador3.Length > 0))
				|| (jugador2.ToString().Equals( jugador4.ToString())  && (jugador2.Length > 0 &&jugador4.Length > 0))
				|| (jugador3.ToString().Equals( jugador4.ToString()) && (jugador3.Length > 0 &&jugador4.Length > 0))){
				txtMsg.text = "Los nombres de los jugadores no pueden ser iguales";
				//Debug.Log ("nombres iguales");
			}
			else{
				PlayerPrefs.Save();

				GameObject modoJuego = GameObject.FindGameObjectWithTag ("ModoJuego");
				Text dropModoJuego = modoJuego.GetComponent<Text> ();
				string textoModoJuego = dropModoJuego.text;
				//Debug.Log ("Imprimimos el modo del juego:"+ textoModoJuego);

				partida = new Partida(textoModoJuego, cuenta);
				//Debug.Log (partida.imprimeDatos ());


			
				PlayerPrefs.SetInt("Cuenta", cuenta);

				//Use invoke to delay calling of LoadDelayed by half the length of fadeColorAnimationClip
				Invoke ("LoadDelayed", fadeColorAnimationClip.length * .5f);

				//Set the trigger of Animator animColorFade to start transition to the FadeToOpaque state.
				//animColorFade.SetTrigger ("fade");

				//Call the StartGameInScene function to start game without loading a new scene.
				Application.LoadLevel("game");
			}

			

		}else{
			txtMsg.text = "Ingrese al menos dos jugadores";
		}


		
	}

	void OnEnable()
	{
		SceneManager.sceneLoaded += SceneWasLoaded;
	}

	void OnDisable()
	{
		SceneManager.sceneLoaded -= SceneWasLoaded;
	}

	//Once the level has loaded, check if we want to call PlayLevelMusic
	void SceneWasLoaded(Scene scene, LoadSceneMode mode)
	{
		//if changeMusicOnStart is true, call the PlayLevelMusic function of playMusic
		if (changeMusicOnStart)
		{
			playMusic.PlayLevelMusic ();
		}	
	}


	public void LoadDelayed()
	{
		//Pause button now works if escape is pressed since we are no longer in Main menu.
		inMainMenu = false;

		//Hide the main menu UI element
		showPanels.HideOptionsPanel ();

		showPanels.HideMenu ();

		//Load the selected scene, by scene index number in build settings
		SceneManager.LoadScene (sceneToStart);

	}

	public void HideDelayed()
	{
		//Hide the main menu UI element after fading out menu for start game in scene
		showPanels.HideOptionsPanel ();

		showPanels.HideMenu();

	}

	public void StartGameInScene()
	{
		//Pause button now works if escape is pressed since we are no longer in Main menu.
		inMainMenu = false;

		//If changeMusicOnStart is true, fade out volume of music group of AudioMixer by calling FadeDown function of PlayMusic, using length of fadeColorAnimationClip as time. 
		//To change fade time, change length of animation "FadeToColor"
		if (changeMusicOnStart) 
		{
			//Wait until game has started, then play new music
			Invoke ("PlayNewMusic", fadeAlphaAnimationClip.length);
		}
		//Set trigger for animator to start animation fading out Menu UI
		animMenuAlpha.SetTrigger ("fade");
		Invoke("HideDelayed", fadeAlphaAnimationClip.length);
		//Debug.Log ("Game started in same scene! Put your game starting stuff here.");
	}


	public void PlayNewMusic()
	{
		//Fade up music nearly instantly without a click 
		playMusic.FadeUp (fastFadeIn);
		//Play music clip assigned to mainMusic in PlayMusic script
		playMusic.PlaySelectedMusic (1);
	}
}
