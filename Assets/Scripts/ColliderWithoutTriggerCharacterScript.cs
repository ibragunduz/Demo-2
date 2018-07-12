using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderWithoutTriggerCharacterScript : MonoBehaviour {

	characterController character;
	// Use this for initialization
	void Start () {
		character = gameObject.transform.parent.transform.parent.gameObject.GetComponent<characterController> ();


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collider){
		

		switch (collider.tag) {
		case  "mana": 
			character.collectOneMana (10);
			Destroy (collider.gameObject);
			break;

		case "rock":
			character.GetComponent<Animator> ().Play ("Trip",0,0);
				character.GetComponent<Rigidbody2D> ().velocity = new Vector2 (character.GetComponent<Rigidbody2D> ().velocity.x * .85f, character.GetComponent<Rigidbody2D> ().velocity.y);
				Destroy (collider.gameObject);
				StartCoroutine (hitTheDoor ());
		
			break;
		case "door":
			character.GetComponent<Animator> ().Play ("Giddy",0,0);

				character.GetComponent<Rigidbody2D> ().velocity = new Vector2 (character.GetComponent<Rigidbody2D> ().velocity.x * .35f, character.GetComponent<Rigidbody2D> ().velocity.y);
				Destroy (collider.gameObject);
				StartCoroutine (hitTheDoor ());

				break;
		}
	}

	IEnumerator hitTheDoor(){
		float maxspeed = character.maxSpeed;
		character.maxSpeed = 0;
		yield return new WaitForSeconds (.2f);
		character.maxSpeed = maxspeed/4;
		yield return new WaitForSeconds (.4f);
		character.maxSpeed = maxspeed/2;
		yield return new WaitForSeconds (.6f);
		character.maxSpeed = maxspeed;

	}

	IEnumerator hitTheRock(){


	

		float maxspeed = character.maxSpeed;
		character.maxSpeed = 0;
		yield return new WaitForSeconds (.5f);
		character.maxSpeed = maxspeed;


	}

}
