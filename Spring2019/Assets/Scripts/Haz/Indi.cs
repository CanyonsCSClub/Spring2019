using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Programer: Dylan Rodriguez
/*01001101 01100001 01100011 00100111 01110011 00100000 01100011 01100001 01101110 
 *00100000 01100111 01101111 00100000 01110100 01101111 00100000 01101000 01100101 01101100 01101100 */
public class Indi : MonoBehaviour {
	public GameObject Indiana;
	private float speed = 0.5f;
	private int time = 0;

	void Update () {
		if (Indiana == true) {
			if (time <= 60) {
				transform.Translate (speed, 0, 0);// moveing forward
			}
			if (time >= 60) {
				Indiana.SetActive (false);// makes it disapear
			}
		}
	}
	public void OnCollisionEnter(Collision collision) 
	{ 
		if (collision.gameObject.CompareTag("Player")) 
		{ 
			collision.gameObject.GetComponent<Health>().AddjustCurrentHealth(-5); 
			print("taking damage"); 
		} 
	}
}
