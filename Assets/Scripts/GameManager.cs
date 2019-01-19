using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Net;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public GameObject startText;
	public GameObject gameOverText;
	public GameObject leaderboardText;
	public GameObject pauseText;
	public GameObject pauseButton;
	public GameObject leftButton;
	public GameObject rightButton;
	public GameObject muteButton;
	public GameObject unmuteButton;
	public GameObject networkError;
	public GameObject BoatSelect;
	public GameObject newHighScore;
	public GameObject loadingCircle;
	public Text playersName;
	public Text scoreText;
	public Text distanceText;
	public Text bestText;
	public Text highscoreText;
	public Text levelUp;
	public Text speedBoost;
	public Text scoreIncrease;
	public Text currentScoreText;
	public Text name1;
	public Text score1;
	public Text name2;
	public Text score2;
	public Text name3;
	public Text score3;
	public Text name4;
	public Text score4;
	public Text name5;
	public Text score5;
	public bool gameOver = false;
	public bool hasBegun = false;
	public bool isPaused = false;
	public AudioSource levelup;
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
			// AudioListener.volume = 0.0f;
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
		scoreIncrease.text = "+20";
		StartCoroutine(ShowScoreIncreaseText());
		if(distance >=100 && score <=110){
			levelUp.text = "Nice!";
			levelup.Play();
			StartCoroutine(ShowLevelUpText());
		}
		if(distance >=400 && score <=410){
			levelUp.text = "Keep going!";
			levelup.Play();
			StartCoroutine(ShowLevelUpText());
		}
		if(distance >=1000 && score <=1010){
			levelUp.text = "Killing it!";
			levelup.Play();
			StartCoroutine(ShowLevelUpText());
		}
		if(distance >=2000 && score <= 2010){
			levelUp.text = "Unbelievable!";
			levelup.Play();
			StartCoroutine(ShowLevelUpText());
		}
		if(distance >=3000 && score <= 3010){
			levelUp.text = "Still going?!";
			levelup.Play();
			StartCoroutine(ShowLevelUpText());
		}
		if(distance >=4000 && score <= 4010){
			levelUp.text = "You're insane!";
			levelup.Play();
			StartCoroutine(ShowLevelUpText());
		}
		if(distance >=5000 && score <= 5010){
			levelUp.text = "This is crazy!";
			levelup.Play();
			StartCoroutine(ShowLevelUpText());
		}
	}

	public void CollectBigDiamonds(){
		distance += 50;
		scoreIncrease.text = "+50";
		StartCoroutine(ShowScoreIncreaseText());
		if(distance >=100 && score <=150){
			levelUp.text = "Nice!";
			levelup.Play();
			StartCoroutine(ShowLevelUpText());
		}
		if(distance >=400 && score <=450){
			levelUp.text = "Keep going!";
			levelup.Play();
			StartCoroutine(ShowLevelUpText());
		}
		if(distance >=1000 && score <=1050){
			levelUp.text = "Killing it!";
			levelup.Play();
			StartCoroutine(ShowLevelUpText());
		}
		if(distance >=2000 && score <= 2050){
			levelUp.text = "Unbelievable!";
			levelup.Play();
			StartCoroutine(ShowLevelUpText());
		}
		if(distance >=3000 && score <= 3050){
			levelUp.text = "Still going?!";
			levelup.Play();
			StartCoroutine(ShowLevelUpText());
		}
		if(distance >=4000 && score <= 4050){
			levelUp.text = "You're insane!";
			levelup.Play();
			StartCoroutine(ShowLevelUpText());
		}
		if(distance >=5000 && score <= 5050){
			levelUp.text = "This is crazy!";
			levelup.Play();
			StartCoroutine(ShowLevelUpText());
		}
	}

	public void CollectSpeedboost(){
		speedBoost.text = "Turbo turning active!";
		StartCoroutine(ShowSpeedBoostText());
		StartCoroutine(ActivateSpeedBoost());
	}

	public void SelectBoat(){
		startText.SetActive (false);
		BoatSelect.SetActive (true);
	}

	public void CloseBoatSelection(){
		startText.SetActive (true);
		BoatSelect.SetActive (false);
	}

	public void CloseNetworkError(){
		networkError.SetActive(false);
		gameOverText.SetActive(true);
	}	

	public void SubmitNewHighScore(){
		newHighScore.SetActive(false);
		StartCoroutine(setNewHighScore());
	}

	IEnumerator ActivateSpeedBoost() {
		speed = 500;
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

	public IEnumerator setNewHighScore(){
		int scoreInt = (int) score;		
		WWWForm form = new WWWForm();
        form.AddField("name", playersName.text);
		form.AddField("score", scoreInt);		
        UnityWebRequest www = UnityWebRequest.Post("https://waila.ml/api/dodgyrocks/createScore", form);
        yield return www.SendWebRequest();
 
        if(www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }
        else {
            Debug.Log("Form upload complete!");
        }
	}

	// When the player dies
	public void SurferDied(){		
		int scoreInt = (int) score;
		if(score > PlayerPrefs.GetInt ("best")){
			// int scoreInt = (int) score;
			PlayerPrefs.SetInt ("best", scoreInt);
			if(Application.internetReachability == NetworkReachability.NotReachable){
				Debug.Log("Error. Check internet connection!");
				// networkError.SetActive(true);
			} else {				
				newHighScore.SetActive(true);				
			}
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
