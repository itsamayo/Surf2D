using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public GameObject startText;
	public GameObject gameOverText;
	public GameObject pauseText;
	public GameObject pauseButton;
	public GameObject leftButton;
	public GameObject rightButton;
	public GameObject muteButton;
	public GameObject unmuteButton;
	public Text scoreText;
	public Text distanceText;
	public Text bestText;
	public Text highscoreText;
	public Text levelUp;
	public Text speedBoost;
	public Text scoreIncrease;
	public Text currentScoreText;
	public bool gameOver = false;
	public bool hasBegun = false;
	public bool isPaused = false;

	public int speed = 300;
	

	public int muted = 0;
	public float volume = 0.3f;

	private float distance = 0f;
	public float score = 0f;
	private float best = 0f;

	// Use this for initialization
	void Awake () {
		// Just to make sure there's only ever one instance of the GameManager
		if (instance == null) {
			instance = this;
		} 
		// If there is one, then destroy this one
		else if (instance != this) {
			Destroy (gameObject);
		}

		//First screen seen on entry
		startText.SetActive (true);
		
		if (!PlayerPrefs.HasKey ("best")) { 
			PlayerPrefs.SetInt ("best", 0);
			best = 0;
		} else {
			best = PlayerPrefs.GetInt("best");
		}

		if (!PlayerPrefs.HasKey ("muted")) { 
			PlayerPrefs.SetInt ("muted", 0);
			muted = 0;
			muteButton.SetActive(false);
			unmuteButton.SetActive(true);
		} else {
			muted = PlayerPrefs.GetInt("muted");
			if(muted == 0){
				muteButton.SetActive(false);
				unmuteButton.SetActive(true);
			} else {
				muteButton.SetActive(true);
				unmuteButton.SetActive(false);
			}		
		}
		// muteButton.SetActive(false);
		// unmuteButton.SetActive(true);		

		scoreText.enabled = false;
	}

	// Update is called once per frame
	void Update () {
		// If gameOver is true then on tap will restart the game - NB this is temporary
		//if (gameOver == true && Input.GetMouseButtonDown(0)) {
		//	SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		//} 
		if (isPaused == false && gameOver == false && hasBegun == true) {
			distance += Time.deltaTime;
			score = Mathf.Round (distance);
			scoreText.enabled = true;
		}			

		scoreText.text = "SCORE: " + score.ToString ();
		currentScoreText.text = score.ToString () + "";
		bestText.text = best.ToString () + "";
		highscoreText.text = best.ToString ();		

		if (muteButton.activeSelf == true) {
			AudioListener.volume = 0.0f;
			PlayerPrefs.SetInt("muted", 1);
		} else {
			AudioListener.volume = volume;
			PlayerPrefs.SetInt("muted", 0);
		}
		
	}

	//Collect coins
	public void CollectCoins(){
		distance += 20;
		StartCoroutine(ShowScoreIncreaseText());
		if(distance >=100 && score <=110){
			levelUp.text = "Nice!";
			StartCoroutine(ShowLevelUpText());
		}
		if(distance >=400 && score <=410){
			levelUp.text = "Keep going!";
			StartCoroutine(ShowLevelUpText());
		}
		if(distance >=1000 && score <=1010){
			levelUp.text = "Killing it!";
			StartCoroutine(ShowLevelUpText());
		}
		if(distance >=2000 && score <= 2010){
			levelUp.text = "Unbelievable!";
			StartCoroutine(ShowLevelUpText());
		}
		if(distance >=3000 && score <= 3010){
			levelUp.text = "Still going?!";
			StartCoroutine(ShowLevelUpText());
		}
		if(distance >=4000 && score <= 4010){
			levelUp.text = "You're insane!";
			StartCoroutine(ShowLevelUpText());
		}
		if(distance >=5000 && score <= 5010){
			levelUp.text = "This is crazy!";
			StartCoroutine(ShowLevelUpText());
		}
	}

	public void CollectSpeedboost(){
		speedBoost.text = "Turbo turning active!";
		StartCoroutine(ShowSpeedBoostText());
		StartCoroutine(ActivateSpeedBoost());
	}	

	IEnumerator ActivateSpeedBoost() {
		speed = 1300;
		yield return new WaitForSeconds(6);
		speed = 300;
	}
	IEnumerator ShowSpeedBoostText() {
		StartCoroutine(FadeTextToFullAlpha(1f, speedBoost));
		yield return new WaitForSeconds(6);
		StartCoroutine(FadeTextToZeroAlpha(1f, speedBoost));
	}
	IEnumerator ShowLevelUpText() {
		StartCoroutine(FadeTextToFullAlpha(1f, levelUp));
		yield return new WaitForSeconds(2);
		StartCoroutine(FadeTextToZeroAlpha(1f, levelUp));
	}

	IEnumerator ShowScoreIncreaseText() {
		StartCoroutine(FadeTextToFullAlpha(1f, scoreIncrease));
		yield return new WaitForSeconds(1);
		StartCoroutine(FadeTextToZeroAlpha(1f, scoreIncrease));
	}

	public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }
 
    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }

	// When the player dies
	public void SurferDied(){		
		if(score > PlayerPrefs.GetInt ("best")){
			int scoreInt = (int) score;
			PlayerPrefs.SetInt ("best", scoreInt);
		}

		PlayerPrefs.SetInt ("score", 0);

		// Sets the game over overlay to visible
		gameOverText.SetActive (true);
		pauseButton.SetActive (false);
		leftButton.SetActive (false);
		rightButton.SetActive (false);
		scoreText.enabled = false;
		// Sets gameOver to true so that the player can't move anymore
		gameOver = true;		
	}
}
