using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;


public static class LogText {

	public static void log(String text = "") {
		GameObject obj = GameObject.FindGameObjectWithTag ("LogText");
		if (obj == null) {
			Debug.LogWarning ("No se ha añadido el objeto de texto de TextMeshPro a LogText");
		} else {
			TextMeshProUGUI textTMPro = obj.GetComponent<TextMeshProUGUI> ();
			textTMPro.SetText (text);
			textTMPro.color = Color.white;
		}
	}

	public static void warning(String text = "") {
		GameObject obj = GameObject.FindGameObjectWithTag ("LogText");
		if (obj == null) {
			Debug.LogWarning ("No se ha añadido el objeto de texto de TextMeshPro a LogText");
		} else {
			TextMeshProUGUI textTMPro = obj.GetComponent<TextMeshProUGUI> ();
			textTMPro.SetText (text);
			textTMPro.color = Color.yellow;
		}
	}

	public static void error(String text = "") {
		GameObject obj = GameObject.FindGameObjectWithTag ("LogText");
		if (obj == null) {
			Debug.LogWarning ("No se ha añadido el objeto de texto de TextMeshPro a LogText");
		} else {
			TextMeshProUGUI textTMPro = obj.GetComponent<TextMeshProUGUI> ();
			textTMPro.SetText (text);
			textTMPro.color = Color.red;
		}
	}
}
