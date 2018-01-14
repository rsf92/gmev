using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Casilla
{

	private GameObject objeto3d;
	private string owner;
	private int units, units_onHold;
	private List<Casilla> adyacent;

	public Casilla (string name, string owner, int units)
	{
		this.objeto3d = GameObject.Find (name);
		this.owner = owner;
		this.units = units;
		units_onHold = 0;
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
		//Añadimos las unidades "huidas" al hold para colocar en el siguiente turno
		main_behavior.units_hold[main_behavior.getIndexPlayer(owner)] = this.units;
		owner = String.Copy (new_owner);
		this.units = units;
		StartOptions.partida.FinPartida (); //Tiene más sentido comprobar si has terminado cuando conquistas algo
		return 0;

	}

	public bool isAdyacent(Casilla objetivo){
		foreach (Casilla adyacente in adyacent) {
			if (adyacente.getName () == objetivo.getName ()) {
				return true;
			}
		}
		return false;
	}

	public bool put_on_hold(int army){
		if (army > units)
			return false;
		units_onHold += army;
		units -= army;
		return true;
	}

	public int move_Units (Casilla objetivo)
	{
		int retValue = -1;
		if (units_onHold <= this.units) {
			objetivo.add_units (units_onHold);
			retValue = 0;
		}

		reset_hold ();
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

	public void reset_hold(){
		units_onHold = 0;
	}

	public void back_from_hold (){
		units += units_onHold;
		reset_hold ();
	}

	public void kill_unit(){
		units--;
	}

}
