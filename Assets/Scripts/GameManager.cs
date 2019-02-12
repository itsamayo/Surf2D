using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Net;
using System.IO;
using System;
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
	public Text slomoText;
	public Text scoreIncrease;
	public Text currentScoreText;
	public Text playersPos;
	public Text playersScore;
	public Text playersPos2;
	public Text playersScore2;
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
	public Text name1a;
	public Text score1a;
	public Text name2a;
	public Text score2a;
	public Text name3a;
	public Text score3a;
	public Text name4a;
	public Text score4a;
	public Text name5a;
	public Text score5a;
	public bool gameOver = false;
	public bool hasBegun = false;
	public bool isPaused = false;
	public AudioSource levelup;
	public int speed = 300;
	public float scrollSpeed = 0.15f;
	public bool spawnsActive = true;
	public int muted = 0;
	public float volume = 0.3f;
	private float distance = 0f;
	public float score = 0f;
	public float best = 0f;

	// Use this for initialization
	void Awake () {
		Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
        Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;

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

	public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    {
        Debug.Log("Received Registration Token: " + token.Token);
		StartCoroutine(SubmitNewToken(token.Token));
		
    }

	public IEnumerator SubmitNewToken(string token){
		WWWForm form = new WWWForm();
        form.AddField("token", token);
        UnityWebRequest www = UnityWebRequest.Post("https://waila.ml/api/dodgyrocks/updateFirebaseToken", form);
        yield return www.SendWebRequest(); 
        if(www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }
        else {
            Debug.Log("Form upload complete: " + www.downloadHandler.data);
        }
	}

    public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    {
        Debug.Log("Received a new message from: " + e.Message.From);		
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
			scrollSpeed = 0.17f;
		}
		if(distance >=400 && score <=410){
			levelUp.text = "Keep going!";
			levelup.Play();
			StartCoroutine(ShowLevelUpText());
			scrollSpeed = 0.18f;
		}
		if(distance >=1000 && score <=1010){
			levelUp.text = "Killing it!";
			levelup.Play();
			StartCoroutine(ShowLevelUpText());
			scrollSpeed = 0.20f;
		}
		if(distance >=2000 && score <= 2010){
			levelUp.text = "Unbelievable!";
			levelup.Play();
			StartCoroutine(ShowLevelUpText());
			scrollSpeed = 0.22f;
		}
		if(distance >=3000 && score <= 3010){
			levelUp.text = "Still going?!";
			levelup.Play();
			StartCoroutine(ShowLevelUpText());
			scrollSpeed = 0.24f;
		}
		if(distance >=4000 && score <= 4010){
			levelUp.text = "You're insane!";
			levelup.Play();
			StartCoroutine(ShowLevelUpText());
			scrollSpeed = 0.26f;
		}
		if(distance >=5000 && score <= 5010){
			levelUp.text = "This is crazy!";
			levelup.Play();
			StartCoroutine(ShowLevelUpText());
			scrollSpeed = 0.28f;
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
			scrollSpeed = 0.17f;
		}
		if(distance >=400 && score <=450){
			levelUp.text = "Keep going!";
			levelup.Play();
			StartCoroutine(ShowLevelUpText());
			scrollSpeed = 0.18f;
		}
		if(distance >=1000 && score <=1050){
			levelUp.text = "Killing it!";
			levelup.Play();
			StartCoroutine(ShowLevelUpText());
			scrollSpeed = 0.20f;
		}
		if(distance >=2000 && score <= 2050){
			levelUp.text = "Unbelievable!";
			levelup.Play();
			StartCoroutine(ShowLevelUpText());
			scrollSpeed = 0.22f;
		}
		if(distance >=3000 && score <= 3050){
			levelUp.text = "Still going?!";
			levelup.Play();
			StartCoroutine(ShowLevelUpText());
			scrollSpeed = 0.24f;
		}
		if(distance >=4000 && score <= 4050){
			levelUp.text = "You're insane!";
			levelup.Play();
			StartCoroutine(ShowLevelUpText());
			scrollSpeed = 0.26f;
		}
		if(distance >=5000 && score <= 5050){
			levelUp.text = "This is crazy!";
			levelup.Play();
			StartCoroutine(ShowLevelUpText());
			scrollSpeed = 0.28f;
		}
	}

	public void CollectSpeedboost(){
		speedBoost.text = "Turbo turning active!";
		StartCoroutine(ShowSpeedBoostText());
		StartCoroutine(ActivateSpeedBoost());
	}

	public void CollectSlomo(){
		slomoText.text = "Slow motion active!";
		StartCoroutine(ShowSlomoText());
		StartCoroutine(ActivateSlomo());
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

	IEnumerator ActivateSlomo() {
		float currentSpeed = scrollSpeed;
		scrollSpeed = 0.08f;
		spawnsActive = false;
		yield return new WaitForSeconds(6);
		scrollSpeed = currentSpeed;
		spawnsActive = true;
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

	IEnumerator ShowSlomoText() {
		StartCoroutine(FadeTextToFullAlpha(1f, slomoText));
		yield return new WaitForSeconds(6);
		StartCoroutine(FadeTextToZeroAlpha(1f, slomoText));
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

	IEnumerator ShowNewHighScorePopup() {		
		yield return new WaitForSeconds(1);
		newHighScore.SetActive(true);
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
            Debug.Log("Form upload complete: " + www.downloadHandler.data);
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
				StartCoroutine(ShowNewHighScorePopup());
			}
		}		
		PlayerPrefs.SetInt ("score", 0);
		// Sets the game over overlay to visible
		StartCoroutine(GetScores());
		gameOverText.SetActive (true);
		pauseButton.SetActive (false);
		leftButton.SetActive (false);
		rightButton.SetActive (false);
		scoreText.enabled = false;
		// Sets gameOver to true so that the player can't move anymore
		gameOver = true;		
	}

	[Serializable]
      public class Scores
      {
          public string name;
          public string score;
      }
      [Serializable]
      public class ScoreInfo
      {
          public Scores[] scores;
          
      }        
    
    IEnumerator GetScores () {
        yield return new WaitForSeconds(1);        
        if(Application.internetReachability == NetworkReachability.NotReachable){
            Debug.Log("Error. Check internet connection!");
            GameManager.instance.leaderboardText.SetActive(false);
            GameManager.instance.networkError.SetActive(true);
            GameManager.instance.startText.SetActive(false);
        } else {                     
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("https://waila.ml/api/dodgyrocks/getScores?score="+PlayerPrefs.GetInt("best")));
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string jsonResponse = reader.ReadToEnd();
            ScoreInfo info = new ScoreInfo{scores=JsonHelper2.FromJson<Scores>(jsonResponse)};
            // For the pop-able view from the start
            GameManager.instance.name1.text = "1. " + info.scores[1].name;
            GameManager.instance.name2.text = "2. " + info.scores[2].name;
            GameManager.instance.name3.text = "3. " + info.scores[3].name;
            GameManager.instance.name4.text = "4. " + info.scores[4].name;
            GameManager.instance.name5.text = "5. " + info.scores[5].name;
            GameManager.instance.score1.text = info.scores[1].score.ToString();
            GameManager.instance.score2.text = info.scores[2].score.ToString();
            GameManager.instance.score3.text = info.scores[3].score.ToString();
            GameManager.instance.score4.text = info.scores[4].score.ToString();
            GameManager.instance.score5.text = info.scores[5].score.ToString();
            // For the Game Over view
            GameManager.instance.name1a.text = "1. " + info.scores[1].name;
            GameManager.instance.name2a.text = "2. " + info.scores[2].name;
            GameManager.instance.name3a.text = "3. " + info.scores[3].name;
            GameManager.instance.name4a.text = "4. " + info.scores[4].name;
            GameManager.instance.name5a.text = "5. " + info.scores[5].name;
            GameManager.instance.score1a.text = info.scores[1].score.ToString();
            GameManager.instance.score2a.text = info.scores[2].score.ToString();
            GameManager.instance.score3a.text = info.scores[3].score.ToString();
            GameManager.instance.score4a.text = info.scores[4].score.ToString();
            GameManager.instance.score5a.text = info.scores[5].score.ToString();
            if(info.scores[0].score.ToString() == "-1"){
                GameManager.instance.playersPos.text = "";
                GameManager.instance.playersScore.text = "";
                GameManager.instance.playersPos2.text = "";
                GameManager.instance.playersScore2.text = "";
            } else {
                GameManager.instance.playersPos.text = info.scores[0].score.ToString() + ". You";
                GameManager.instance.playersScore.text = PlayerPrefs.GetInt("best").ToString();
                GameManager.instance.playersPos2.text = info.scores[0].score.ToString() + ". You";
                GameManager.instance.playersScore2.text = PlayerPrefs.GetInt("best").ToString();
            }
            if(PlayerPrefs.GetInt("best").ToString()==info.scores[1].score.ToString()){
                GameManager.instance.name1.color = new Color(43.0f/255.0f, 43.0f/255.0f, 43.0f/255.0f);
                GameManager.instance.score1.color = new Color(43.0f/255.0f, 43.0f/255.0f, 43.0f/255.0f);
                GameManager.instance.name1a.color = new Color(43.0f/255.0f, 43.0f/255.0f, 43.0f/255.0f);
                GameManager.instance.score1a.color = new Color(43.0f/255.0f, 43.0f/255.0f, 43.0f/255.0f);
            }
            if(PlayerPrefs.GetInt("best").ToString()==info.scores[2].score.ToString()){
                GameManager.instance.name2.color = new Color(43.0f/255.0f, 43.0f/255.0f, 43.0f/255.0f);
                GameManager.instance.score2.color = new Color(43.0f/255.0f, 43.0f/255.0f, 43.0f/255.0f);
                GameManager.instance.name2a.color = new Color(43.0f/255.0f, 43.0f/255.0f, 43.0f/255.0f);
                GameManager.instance.score2a.color = new Color(43.0f/255.0f, 43.0f/255.0f, 43.0f/255.0f);
            }
            if(PlayerPrefs.GetInt("best").ToString()==info.scores[3].score.ToString()){
                GameManager.instance.name3.color = new Color(43.0f/255.0f, 43.0f/255.0f, 43.0f/255.0f);
                GameManager.instance.score3.color = new Color(43.0f/255.0f, 43.0f/255.0f, 43.0f/255.0f);
                GameManager.instance.name3a.color = new Color(43.0f/255.0f, 43.0f/255.0f, 43.0f/255.0f);
                GameManager.instance.score3a.color = new Color(43.0f/255.0f, 43.0f/255.0f, 43.0f/255.0f);
            }
            if(PlayerPrefs.GetInt("best").ToString()==info.scores[4].score.ToString()){
                GameManager.instance.name4.color = new Color(43.0f/255.0f, 43.0f/255.0f, 43.0f/255.0f);
                GameManager.instance.score4.color = new Color(43.0f/255.0f, 43.0f/255.0f, 43.0f/255.0f);
                GameManager.instance.name4a.color = new Color(43.0f/255.0f, 43.0f/255.0f, 43.0f/255.0f);
                GameManager.instance.score4a.color = new Color(43.0f/255.0f, 43.0f/255.0f, 43.0f/255.0f);
            }
            if(PlayerPrefs.GetInt("best").ToString()==info.scores[5].score.ToString()){
                GameManager.instance.name5.color = new Color(43.0f/255.0f, 43.0f/255.0f, 43.0f/255.0f);
                GameManager.instance.score5.color = new Color(43.0f/255.0f, 43.0f/255.0f, 43.0f/255.0f);
                GameManager.instance.name5a.color = new Color(43.0f/255.0f, 43.0f/255.0f, 43.0f/255.0f);
                GameManager.instance.score5a.color = new Color(43.0f/255.0f, 43.0f/255.0f, 43.0f/255.0f);
            }
        }        
    }    
}

public static class JsonHelper2
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}