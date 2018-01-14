using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;


public class LogText {

	private static LogText instance;
	private TextMeshProUGUI textTMPro;

	private LogText(TextMeshProUGUI textTMPro) {
		this.textTMPro = textTMPro;
		this.textTMPro.color = Color.white;
	}

	public static LogText getInstance() {
		if (instance == null) {
			instance = new LogText(GameObject.FindGameObjectWithTag ("LogText").GetComponent<TextMeshProUGUI>());
		}
		return instance;
	}

	public void log(String text = "") {
		if (this.textTMPro == null) {
			this.textTMPro = GameObject.FindGameObjectWithTag ("LogText").GetComponent<TextMeshProUGUI> ();
		}
		if (this.textTMPro != null) {
			this.textTMPro.SetText (text);
			this.textTMPro.color = Color.white;
		} else {
			Debug.LogError ("No se ha añadido el objeto de texto de TextMeshPro a LogText");
		}
	}

	public void warning(String text = "") {
		if (this.textTMPro == null) {
			this.textTMPro = GameObject.FindGameObjectWithTag ("LogText").GetComponent<TextMeshProUGUI> ();
		}
		if (this.textTMPro != null) {
			this.textTMPro.SetText (text);
			this.textTMPro.color = Color.yellow;
		} else {
			Debug.LogError ("No se ha añadido el objeto de texto de TextMeshPro a LogText");
		}
	}

	public void error(String text = "") {
		if (this.textTMPro == null) {
			this.textTMPro = GameObject.FindGameObjectWithTag ("LogText").GetComponent<TextMeshProUGUI> ();
		}
		if (this.textTMPro != null) {
			this.textTMPro.SetText (text);
			this.textTMPro.color = Color.red;
		} else {
			Debug.LogError ("No se ha añadido el objeto de texto de TextMeshPro a LogText");
		}
	}
}
