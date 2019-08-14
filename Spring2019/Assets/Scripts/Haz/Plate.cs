using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Programer: Dylan Rodriguez
//01000110 01110101 01100011 01101011 00100000 01001101 01100001 01100011

// Dylan, please use the universal comment header I supplied... I'm broken. - Hunter Goodin 

public class Plate : MonoBehaviour {
	//I coded it kind of like i did with the lap system
	public GameObject ewokes;
    public GameObject indiana; 
	public GameObject pressure;
	public bool checker = false;//to make the pressure plate activate the tree so it can start moving and try to kill Idoit that steped on it
    public bool ewokPlate;
    public bool indiPlate; 


// Don't need anything in Update anymore so I'm commenting it out entirely (Hunter)

/*
	private void Update()
    {
		//if (checker == true)
  //      {//The pressure plate does not want to be steped on so it triggered its friend the tree to be like John Ceina and attack
		//	// Pressure.SetActive (false);
		//	Ewokes.SetActive (true);
		//} 
		//else if (checker == false)
  //      {
		//	Pressure.SetActive (true);
		//	Ewokes.SetActive (false);
		//}
	}
*/ 

// Changed this script to more of a preassure plate system (Hunter) 

	void OnTriggerEnter (Collider col)
    {
        if(col.name == "Player" && ewokPlate == true)
        {
            ewokes.gameObject.GetComponent<Ewok>().ToggleLog();
        }
        if (col.name == "Player" && indiPlate == true)
        {
            indiana.gameObject.GetComponent<Indi>().IndianaToggle();
        }
        checker = true; 
	}
}
