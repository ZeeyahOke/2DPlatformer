using System.Collections;
using UnityEngine;
 
public class PlayerDamage : MonoBehaviour {
 
	public float invincibleSeconds = 2f; // How long before the player can be hit again.
 
	private bool canDamage = true;
 
	// Called by every enemy/boss (Spider, Egg, Snail, Frog, Stone).
	public void DealDamage() {
		if (!canDamage) return;
 
		canDamage = false;
 
		if (GameManager.instance != null) {
			GameManager.instance.TakeEnemyDamage(); 
		}
 
		StartCoroutine(InvincibilityWindow());
	}
 
	IEnumerator InvincibilityWindow() {
		yield return new WaitForSeconds(invincibleSeconds);
		canDamage = true;
	}
 
} // class

