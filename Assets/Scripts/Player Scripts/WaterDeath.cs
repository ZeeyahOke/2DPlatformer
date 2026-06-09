using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDeath : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Water") {
			GameManager.instance.PlayerEnteredWater();
		}
	}

} // class
