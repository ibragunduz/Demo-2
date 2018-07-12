using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour {

	CircleCollider2D col;
	// Use this for initialization
	void Start () {
		col = GetComponent<CircleCollider2D> ();

	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D collider){
		Debug.Log ("-->"+collider.name);
		if(collider.gameObject.tag=="door"||collider.gameObject.tag=="rock")
		throwObject (collider.gameObject);
		if (collider.gameObject.tag == "Player") {
		//	blockOutToPlayer (collider.gameObject);
		}
	}

	IEnumerator blockOutToPlayer(GameObject obj){
		characterController character = obj.GetComponent<characterController> ();
		float maxspeed = character.maxSpeed;
		character.maxSpeed = 0;
		yield return new WaitForSeconds (.3f);
		character.maxSpeed = maxspeed/4;
		yield return new WaitForSeconds (.5f);
		character.maxSpeed = maxspeed/2;
		yield return new WaitForSeconds (.9f);
		character.maxSpeed = maxspeed;
	}
	void throwObject(GameObject obj){
		Destroy (obj);
	}

	public void startExplosion(){
		
		col.radius =3;
	}
	public void stopExplosion(){
			col.radius = 0f;
	}
}
