using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlomoBoost : MonoBehaviour
{
    //Array of islands to spawn 
	public GameObject[] slomoboosts;
	//public GameObject clone;

	//Time it takes to spawn coins
	[Space(3)]
	private float waitingForNextSpawn = 5f;
	private float theCountdown = 1f;

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
		if (GameManager.instance.hasBegun == true && GameManager.instance.isPaused == false && GameManager.instance.score > 2000) {
			// Timer to spawn the next speedboost 
			theCountdown -= Time.deltaTime;
			if (theCountdown <= 0 && GameManager.instance.spawnsActive == true) {
				// Spawn a speedboost
				SpawnSlomoboosts ();
				theCountdown = waitingForNextSpawn;
			}
		}

	}


	void SpawnSlomoboosts()
	{
		// Defines the min and max ranges for x and y
		Vector2 pos = new Vector2 (Random.Range (xMin, xMax), Random.Range (yMin, yMax));

		// Chooses a new island to spawn from the array 
		GameObject slomoPrefab = slomoboosts [Random.Range (0, slomoboosts.Length)];

		// Creates the random island at the random 2D position.
		Instantiate (slomoPrefab, pos, transform.rotation);

	}	

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Obstacle") {
			Destroy (coll.gameObject);
		} 
	}
}
