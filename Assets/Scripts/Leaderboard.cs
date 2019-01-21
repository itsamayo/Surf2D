using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{      
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
    
    void Start () {}

    public void OpenLeaderboard(){
        GameManager.instance.leaderboardText.SetActive(true);
        GameManager.instance.startText.SetActive(false);
        StartCoroutine(GetScores());
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
            ScoreInfo info = new ScoreInfo{scores=JsonHelper.FromJson<Scores>(jsonResponse)};
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

public static class JsonHelper
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
