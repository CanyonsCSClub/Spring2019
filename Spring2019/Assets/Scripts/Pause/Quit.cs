using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour {
	public bool isWork; 

	void Start()
	{
		isWork = true;
	}

	public void DylanIsDumb(){
		// isWork = true; 
		Application.LoadLevel(0); 

	}
}
