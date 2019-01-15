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
    
    void Start () {
        GetScores();      
    }      

    public void GetScores () {
        if(Application.internetReachability == NetworkReachability.NotReachable){
            Debug.Log("Error. Check internet connection!");
            GameManager.instance.networkError.SetActive(true);
            GameManager.instance.gameOverText.SetActive(false);
        } else {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("https://waila.ml/api/dodgyrocks/getScores"));
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string jsonResponse = reader.ReadToEnd();
            ScoreInfo info = new ScoreInfo{scores=JsonHelper.FromJson<Scores>(jsonResponse)};
            GameManager.instance.name1.text = "1. " + info.scores[0].name;
            GameManager.instance.name2.text = "2. " + info.scores[1].name;
            GameManager.instance.name3.text = "3. " + info.scores[2].name;
            GameManager.instance.name4.text = "4. " + info.scores[3].name;
            GameManager.instance.name5.text = "5. " + info.scores[4].name;
            GameManager.instance.score1.text = info.scores[0].score.ToString();
            GameManager.instance.score2.text = info.scores[1].score.ToString();
            GameManager.instance.score3.text = info.scores[2].score.ToString();
            GameManager.instance.score4.text = info.scores[3].score.ToString();
            GameManager.instance.score5.text = info.scores[4].score.ToString();
            GameManager.instance.leaderboardText.SetActive(true);
		    GameManager.instance.gameOverText.SetActive(false);
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
