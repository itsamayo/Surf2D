using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	//variables
	public float moveSpeed = 300;
	public GameObject character;

	private Rigidbody2D characterBody;
	private float ScreenWidth;
    private float widthRel;

	// Use this for initialization
	void Start () {
		ScreenWidth = Screen.width;
        widthRel = character.transform.localScale.x / ScreenWidth;
		characterBody = character.GetComponent<Rigidbody2D>();
        // characterBody.freezeRotation = false;
	}
	
	// Update is called once per frame
	void Update () {
		int i = 0;
		//loop over every touch found
		while (i < Input.touchCount && GameManager.instance.hasBegun == true && GameManager.instance.isPaused == false && GameManager.instance.gameOver == false) {
			if (Input.GetTouch (i).position.x > ScreenWidth / 2) {
				//move right
				RunCharacter (1.0f);
                characterBody.rotation = -15f;
			}
			if (Input.GetTouch (i).position.x < ScreenWidth / 2) {
				//move left
				RunCharacter (-1.0f);
                characterBody.rotation = 15f;
			}             
			++i;
		}

        if (Input.touchCount == 0) {
            characterBody.rotation = 0f;
        }

        if(GameManager.instance.gameOver == true){
            characterBody.GetComponent<Renderer>().enabled = false;
        }		

	}
	void FixedUpdate(){
		#if UNITY_EDITOR
		RunCharacter(Input.GetAxis("Horizontal"));
		#endif

        Vector3 viewPos = Camera.main.WorldToViewportPoint (character.transform.position);
        viewPos.x = Mathf.Clamp(viewPos.x, widthRel, 1 - widthRel);
        character.transform.position = Camera.main.ViewportToWorldPoint (viewPos);
	}

	private void RunCharacter(float horizontalInput){
		//move player
		//characterBody.AddForce(new Vector2(horizontalInput * moveSpeed * Time.deltaTime, 0));
		characterBody.velocity = new Vector2(horizontalInput * moveSpeed * Time.deltaTime,0f);
	}
}