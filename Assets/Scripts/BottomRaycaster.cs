using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomRaycaster : MonoBehaviour {

	public GameObject character;
	// Use this for initialization
	void Start () {
		
	}
	RaycastHit2D hit;

	Quaternion rot;

	public  int smooth=5;



	void FixedUpdate()

	{

		hit = checkRaycast ();
			if (hit)
				{
			if (hit.collider.name == "yol") {
				rot = Quaternion.FromToRotation (transform.up, hit.normal) *
				transform.rotation;
				character.transform.rotation = Quaternion.Lerp (transform.rotation, rot, 
					Time.deltaTime * smooth);
			}
		}

	}


	RaycastHit2D checkRaycast( ){
		Vector2 direction = new Vector2 (0f,-10f);
		Vector2 startingPosition = new Vector2 (transform.position.x,transform.position.y);
		Debug.DrawRay (startingPosition,direction,Color.red);

		return Physics2D.Raycast (transform.position, direction);
	}

}
