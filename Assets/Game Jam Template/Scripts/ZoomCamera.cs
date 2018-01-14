using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : MonoBehaviour {

	public float speed = 50f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 pos = transform.position;

		if (Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			pos.z += speed * Time.deltaTime;
			/*
			if (GetComponent<Camera> ().fieldOfView > 1)
			{
				GetComponent<Camera> ().fieldOfView--;
			}
			*/
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			pos.z -= speed * Time.deltaTime;
			/*
			if (GetComponent<Camera> ().fieldOfView < 100)
			{
				GetComponent<Camera> ().fieldOfView++;
			}
			*/
		}
		transform.position = pos;
	}
}
