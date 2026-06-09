using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour {

	public int maxHealth = 5;        // Raise this to make the boss tougher.
	public float hitCooldown = 1f;   // Seconds before the boss can be hit again.
	public Image healthFill;         // The bar's fill image (Image Type = Filled).
	public float winDelay = 1.5f;    // Let the death animation play before the win panel.

	private int currentHealth;
	private Animator anim;
	private bool canDamage;

	void Awake () {
		anim = GetComponent<Animator> ();
		canDamage = true;
		currentHealth = maxHealth;
		UpdateHealthBar();
	}

	void OnTriggerEnter2D(Collider2D target) {
		if (!canDamage) return;
		if (target.tag != MyTags.BULLET_TAG) return;

		currentHealth--;
		canDamage = false;
		UpdateHealthBar();

		if (currentHealth <= 0) {
			Defeated();
		} else {
			StartCoroutine (WaitForDamage ());
		}
	}

	IEnumerator WaitForDamage() {
		yield return new WaitForSeconds (hitCooldown);
		canDamage = true;
	}

	void UpdateHealthBar() {
		if (healthFill != null) {
			healthFill.fillAmount = (float)currentHealth / maxHealth;
		}
	}

	void Defeated() {
		GetComponent<BossScript>().DeactivateBossScript(); // Stop the stone attacks.
		anim.Play("BossDead");
		Invoke(nameof(WinNow), winDelay);                  // Win shortly after, so the death plays.
	}

	void WinNow() {
		if (GameManager.instance != null) {
			GameManager.instance.WinGame();
		}
	}

} // class
