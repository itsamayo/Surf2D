using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour {

	//Array of islands to spawn 
	public GameObject[] coins;
	//public GameObject clone;

	//Time it takes to spawn coins
	[Space(3)]
	private float waitingForNextSpawn = 1;
	private float theCountdown = 1;

	// the range of X - Found to work best when set from -4 to 4
	[Header ("X Spawn Range")]
	public float xMin;
	public float xMax;

	// the range of y - Used to create a slight variation in spawn distance 
	[Header ("Y Spawn Range")]
	public float yMin;
	public float yMax;

	// Use this for initialization
	void Start()
	{
		// collect = GetComponent<AudioSource> ();
	}

	void OnBecameInvisible() {
         Destroy(gameObject);
	}
	// Update is called once per frame
	public void Update()
	{
		if (GameManager.instance.hasBegun == true && GameManager.instance.isPaused == false) {
			// Timer to spawn the next coin 
			theCountdown -= Time.deltaTime;
			if (theCountdown <= 0) {
				// Spawn a coin
				SpawnCoins ();
				theCountdown = waitingForNextSpawn;
			}
		}

	}


	void SpawnCoins()
	{
		// Defines the min and max ranges for x and y
		Vector2 pos = new Vector2 (Random.Range (xMin, xMax), Random.Range (yMin, yMax));

		// Chooses a new island to spawn from the array 
		GameObject coinsPrefab = coins [Random.Range (0, coins.Length)];

		// Creates the random island at the random 2D position.
		Instantiate (coinsPrefab, pos, transform.rotation);

	}	

}


