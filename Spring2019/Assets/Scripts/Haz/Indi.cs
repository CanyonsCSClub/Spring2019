using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Programer: Dylan Rodriguez
/*01001101 01100001 01100011 00100111 01110011 00100000 01100011 01100001 01101110 
 *00100000 01100111 01101111 00100000 01110100 01101111 00100000 01101000 01100101 01101100 01101100 */
public class Indi : MonoBehaviour {
	public float speed = 1f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (0, 0, speed); //makes it roll
	}
}
