using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandScroll : MonoBehaviour {

	public float speed = 0.2f;

	// Use this for initialization
	void Start () {
		// We don't need anything here right now
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.instance.isPaused == false) {
			//Once spawned, scroll the islands at the same speed as the ocean scroll
			this.transform.Translate (Vector2.up * Time.deltaTime * -GameManager.instance.scrollSpeed*20, Camera.main.transform);
		}
	}
}
