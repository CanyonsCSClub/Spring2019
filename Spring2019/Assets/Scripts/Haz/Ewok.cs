using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Programer: Dylan Rodriguez
/*01001101 01100001 01100011 00100111 01110011 00100000 00111101 00100000 
 01001000 01101111 01110010 01110010 01101001 01100010 01101100 01100101*/
public class Ewok : MonoBehaviour {
	//I coded it kind of like i did with the lap system
	public GameObject Ewokes;
	public GameObject Pressure;
	public float speed = 10f; //speed of which the tree moves at, make the tree come out of no where (like John Ceina) and try to kill player
	private int time = 0;

	private void Update(){
		if (Ewokes == true) {
			if(time <=10){
				transform.Translate (0, 0, speed * Time.deltaTime * 6);//pushes the player into trap or wall to kill them
				time++;
			}
			if (time >= 10) {
				Ewokes.SetActive (false);
			}
		}

	}


	void OnTriggerEnter () {

		Debug.Log ("It works YAAAA");// Makes sure my Program works, it needs to be there because Mac's are really shity for coding and making things work on unity

	}
}
