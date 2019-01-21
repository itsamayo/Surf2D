using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Islands : MonoBehaviour {

	//Array of islands to spawn 
	public GameObject[] islands;
	//public GameObject clone;

	//Time it takes to spawn islands
	[Space(3)]
	private float waitingForNextSpawn = 3f;
	private float theCountdown = 2f;

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
		//We don't need anything here right now
	}

	void OnBecameInvisible() {
         Destroy(gameObject);
	}
	// Update is called once per frame
	public void Update()
	{	
		if(GameManager.instance.score <= 100){
			waitingForNextSpawn = 3f;
		} else if (GameManager.instance.score >= 101 && GameManager.instance.score < 400) {
			waitingForNextSpawn = 2f;
		} else if (GameManager.instance.score >= 401 && GameManager.instance.score < 1000) {
			waitingForNextSpawn = 1.5f;
		} else if (GameManager.instance.score >= 1001 && GameManager.instance.score < 2000) {
			waitingForNextSpawn = 1f;
		} else if (GameManager.instance.score >= 2001 && GameManager.instance.score < 3000) {
			waitingForNextSpawn = 0.5f;
		} else if (GameManager.instance.score >= 3001 && GameManager.instance.score < 4000) {
			waitingForNextSpawn = 0.45f;
		} else if (GameManager.instance.score >= 4001 && GameManager.instance.score < 5000) {
			waitingForNextSpawn = 0.4f;
		} else if (GameManager.instance.score >= 5001 && GameManager.instance.score < 6000) {
			waitingForNextSpawn = 0.35f;
		}


		if (GameManager.instance.hasBegun == true && GameManager.instance.isPaused == false) {
			// Timer to spawn the next island 
			theCountdown -= Time.deltaTime;
			if (theCountdown <= 0 && GameManager.instance.spawnsActive == true) {
				SpawnIslands ();
				theCountdown = waitingForNextSpawn;
			}
		}

	}


	void SpawnIslands()
	{
		// Defines the min and max ranges for x and y
		Vector2 pos = new Vector2 (Random.Range (xMin, xMax), Random.Range (yMin, yMax));

		// Chooses a new island to spawn from the array 
		GameObject islandsPrefab = islands [Random.Range (0, islands.Length)];

		// Creates the random island at the random 2D position.
		Instantiate (islandsPrefab, pos, transform.rotation);
		
	}		

}

	
