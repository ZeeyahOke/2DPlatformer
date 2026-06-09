using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
 
public class GameManager : MonoBehaviour {
 
	public static GameManager instance;
 
	[Header("Lives")]
	public int startingLives = 3;
 
	[Header("Timer")]
	public Text timerText;          // Drag your on-screen timer Text here.
	public float timeLimit = 60f;   // Seconds on the clock at the start.
	public float timePenalty = 5f;  // Seconds lost every time you die.
 
	[Header("UI Panels")]
	public GameObject endScenePanel; 
	public GameObject winPanel;
 
	private Text lifeText;
	private int lives;
	private float timeLeft;
	private bool isPlaying;
 
	private Transform player;
	private Rigidbody2D playerBody;
	private Vector3 lastSafePosition; // Last dry spot, used for respawning near water.
 
	void Awake() {
		if (instance == null) instance = this;
		else { Destroy(gameObject); return; }
	}
 
	void Start() {
		Time.timeScale = 1f;
		isPlaying = true;
 
		lives = startingLives;
		timeLeft = timeLimit;
 
		player = GameObject.FindGameObjectWithTag("Player").transform;
		playerBody = player.GetComponent<Rigidbody2D>();
		lastSafePosition = player.position;
 
		lifeText = GameObject.Find("LifeText").GetComponent<Text>();
 
		UpdateLifeText();
		UpdateTimerText();
		ShowPanel(endScenePanel, false);
		ShowPanel(winPanel, false);
	}
 
	void Update() {
		if (!isPlaying) return;
 
		// Count the clock down. Running out of time ends the game.
		timeLeft -= Time.deltaTime;
		if (timeLeft <= 0f) {
			timeLeft = 0f;
			UpdateTimerText();
			GameOver();
			return;
		}
		UpdateTimerText();
 
		// Remember the last still, dry spot so a respawn lands near where you died.
		if (Mathf.Abs(playerBody.linearVelocity.y) < 0.05f) {
			lastSafePosition = player.position;
		}
	}
 
	// --- Public hooks (called by other scripts) ---
 
	// WaterDeath.cs calls this when the player falls in water.
	public void PlayerEnteredWater() => LoseLife();
 
	// Called when an enemy hits the player.
	public void TakeEnemyDamage() => LoseLife();
 
	public void WinGame() {
		isPlaying = false;
		Time.timeScale = 0f;
		ShowPanel(winPanel, true);
	}
 
	// --- Shared loss logic (water + enemy both run through here) ---
 
	void LoseLife() {
		lives--;
		timeLeft = Mathf.Max(0f, timeLeft - timePenalty); // Losing also burns time.
 
		UpdateLifeText();
		UpdateTimerText();
 
		if (lives > 0) Respawn();
		else GameOver();
	}
 
	void Respawn() {
		// Drop back in just above and left of the last safe spot.
		player.position = lastSafePosition + Vector3.up * 1.5f + Vector3.left * 2f;
		playerBody.linearVelocity = Vector2.zero;
	}
 
	void GameOver() {
		isPlaying = false;
		Time.timeScale = 0f;
		ShowPanel(endScenePanel, true);
	}
 
	// --- Small helpers (cut the repeated lines) ---
 
	void UpdateLifeText() {
		if (lifeText != null) lifeText.text = "x" + Mathf.Max(0, lives);
	}
 
	void UpdateTimerText() {
    if (timerText == null) return;

    int totalSeconds = Mathf.CeilToInt(timeLeft);
    int minutes = totalSeconds / 60;
    int seconds = totalSeconds % 60;

    timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
 
	void ShowPanel(GameObject panel, bool show) {
		if (panel != null) panel.SetActive(show);
	}
 
	// --- Button hooks ---
 
	public void Replay() {
		Time.timeScale = 1f;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

    public void Quit() {
		Debug.Log("Quit pressed - exiting game");
		Application.Quit();
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}
 
	public void GoToMainMenu() {
		Time.timeScale = 1f;
		SceneManager.LoadScene("MainMenu");
	}
 
} // class
 