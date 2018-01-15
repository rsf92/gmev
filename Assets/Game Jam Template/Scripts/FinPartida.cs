using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class FinPartida : MonoBehaviour {



	public int sceneToStart = 0;										//Index number in build settings of scene to load if changeScenes is true
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

	private Partida part = StartOptions.partida;

	void Awake()
	{
		//Get a reference to ShowPanels attached to UI object
		showPanels = GetComponent<ShowPanels> ();

		Text txtMsg= GameObject.Find("Ganador").GetComponent<Text>();
		txtMsg.text = "El ganador es "+ part.getJugGana();
	}


	public void StartButtonClicked()
	{
		showPanels.HideMenu();
		//Call the StartGameInScene function to start game without loading a new scene.
		Application.LoadLevel("menu");

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


}
