using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Programer: Dylan Rodriguez
//01000110 01110101 01100011 01101011 00100000 01001101 01100001 01100011
public class Plate : MonoBehaviour {
	//I coded it kind of like i did with the lap system
	public GameObject Ewokes;
	public GameObject Pressure;
	public bool checker = false;//to make the pressure plate activate the tree so it can start moving and try to kill Idoit that steped on it

	private void Update(){
		if (checker == true) {//The pressure plate does not want to be steped on so it triggered its friend the tree to be like John Ceina and attack
			Pressure.SetActive (false);
			Ewokes.SetActive (true);
		} 
		else if (checker == false) {
			Pressure.SetActive (true);
			Ewokes.SetActive (false);
		}
	}
	void OnTriggerEnter () {
		Debug.Log ("It works YAAAA"); //makes sure that my program works eventhough Im using mac so I dont know if it will work on Windows, I hope it does!
		if(checker == false)
		{
			checker = true;
		}
			
	}
}
