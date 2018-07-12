using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour {

	public Transform playerTransform;
	float ExtraX,ExtraY;
	void Start () {
		calculateExtras ();
	
	}




	void FixedUpdate ()
	{
		if (playerTransform == null)
			return;
		Vector3 desiredPosition = new Vector3 (transform.position.x, (playerTransform.position.y+ExtraY),transform.position.z);

		//new Vector3 ((playerTransform.position.x+offset),(playerTransform.position.y+offset),playerTransform.position.z);

		//desiredPosition.y += ExtraY;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime*5);
		transform.position = smoothedPosition;

		//transform.LookAt(playerTransform);
	}

	public float x;
	void LateUpdate () {

		if (playerTransform == null)
			return;
		
		transform.position = new Vector3 (playerTransform.position.x + ExtraX, transform.position.y, transform.position.z);
		//GetComponent<Transform> ().position = new Vector3 (playerTransform.position.x+ExtraX,playerTransform.position.y+ExtraY,GetComponent<Transform> ().position.z);

	}


	void calculateExtras(){
		ExtraX = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, 0)).x - Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0)).x;
		ExtraX = ExtraX * (8.5f / 32f);

		ExtraY = Camera.main.ViewportToWorldPoint (new Vector3 (0 ,1, 0)).y - Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0)).y;
		Debug.Log (ExtraY+	"");

		ExtraY = ExtraY * (1.5f / 18f);
	}
}
