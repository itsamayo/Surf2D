using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour {

	public float speed = 2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.instance.hasBegun == true && GameManager.instance.isPaused == false) {
			Vector2 offset = new Vector2 (0, Time.time * speed);

			GetComponent<Renderer> ().material.mainTextureOffset = offset;﻿
		}
	}
}
