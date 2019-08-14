using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this is so it can spin and move at once
public class Indi2 : MonoBehaviour {
	private float speed = 5f;
	void Update () {
		transform.Rotate (0, 0, speed * -1);//spins

	}
}

// Dylan, please use the universal comment header I supplied... You're killing me here. - Hunter Goodin 
