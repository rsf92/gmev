using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Army {
	private GameObject army;
	private int units;
	private int units_hold;
	private Animator anim;
	public Army(){
		units = 0;
		army = null;
	}

	public void instantiate(int units, Tile position){

		int index = main_behavior.getIndexPlayer (position.me.getOwner());

		this.units = units;
		switch (units) {
			case 0:
				return;
			case 1:
			case 2:
			army = ((Pool)main_behavior.mypool[index]).getFromPool(1);
				break;
			case 3:
			case 4:
			army = ((Pool)main_behavior.mypool[index]).getFromPool(2);
				break;
			case 5:
			default:
			army = ((Pool)main_behavior.mypool[index]).getFromPool(3);
				break;
		}

		army.transform.position = position.transform.position;
		anim = army.GetComponent<Animator> ();
	}

	public void deinstantiate(){
		army.transform.position= Vector3.zero;
	}
		
	public void playMove(){
		anim.Play ("Walk");
	}

	public void playAttack(){
		anim.Play ("Fight");
	
	}


	public void move(Vector3 direction){
		
		army.transform.position += direction / 20;
	}

	public void resetRotation(){
		army.transform.rotation = Quaternion.identity;
	}

	public void rotate(float angle){

			army.transform.Rotate(Vector3.up, angle);
	}
	public void put_on_hold (int unidades){
		if (unidades <= units) {
			units -= unidades;
			units_hold = unidades;
		}
	}

	public bool activeSelf(){
		return army.activeSelf;
	}

	public Vector3 getpos(){
		return army.transform.position;
	}

	public int getUnits(){
		return units;
	}

	public void kill_unit(){
		units--;
	}
}
