using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigDiamonds : MonoBehaviour {

	//Array of big diamonds to spawn 
	public GameObject[] bigdiamonds;
	//public GameObject clone;

	//Time it takes to spawn big diamonds
	[Space(3)]
	private float waitingForNextSpawn = 10;
	private float theCountdown = 10;

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
				SpawnBigDiamonds ();
				theCountdown = waitingForNextSpawn;
			}
		}

	}


	void SpawnBigDiamonds()
	{
		// Defines the min and max ranges for x and y
		Vector2 pos = new Vector2 (Random.Range (xMin, xMax), Random.Range (yMin, yMax));

		// Chooses a new BigDiamonds to spawn from the array 
		GameObject bigdiamondsPrefab = bigdiamonds [Random.Range (0, bigdiamonds.Length)];

		// Creates the random BigDiamonds at the random 2D position.
		Instantiate (bigdiamondsPrefab, pos, transform.rotation);

	}	

	// Collision with rock destroys diamond
	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Obstacle") {
			Destroy (coll.gameObject);
		} 
	}

}


