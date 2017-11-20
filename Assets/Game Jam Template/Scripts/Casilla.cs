using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Casilla
{

	private GameObject objeto3d;
	private string owner;
	private int units;
	private List<Casilla> adyacent;

	public Casilla (string name, string owner, int units)
	{
		this.objeto3d = GameObject.Find (name);
		this.owner = owner;
		this.units = units;
	}

	public void setAdyacents (List<Casilla> adyacents)
	{
		this.adyacent = adyacents;
	}

	public Vector3 GetPosition ()
	{
		return this.objeto3d.transform.position;
	}

	public string getOwner ()
	{
		return this.owner;
	}

	public int getUnits ()
	{
		return this.units;
	}

	public void add_units (int units)
	{
		this.units += units;
	}

	public int conquer (string new_owner, int units)
	{
		if (new_owner.Equals (this.owner))
			return -1;

		owner = String.Copy (new_owner);
		this.units = units;
		return 0;

	}

	public int move_Units (Casilla objetivo)
	{
		int retValue = -1;
		if (0 < this.units) {
			foreach (Casilla adyacente in adyacent) {
				if (adyacente.getName () == objetivo.getName ()) {
					this.units -= 1;
					objetivo.add_units (1);
					retValue = 0;
					break;
				}

			}
		}


		return retValue;
	}

	public string getName ()
	{
		return this.objeto3d.ToString ();
	}

	public void printAdyacents ()
	{
		
		foreach (Casilla casilla in adyacent)
			Debug.Log ((casilla.getName ()));
		
	}

}
