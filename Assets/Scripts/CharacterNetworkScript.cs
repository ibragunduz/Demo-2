using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterNetworkScript : Photon.MonoBehaviour {

	// Use this for initialization
	void Start () {
		//correctPos = new Vector3 (-6,0,0);
	}
	int c;
	// Update is called once per frame
	void Update () {
		if (!photonView.isMine)
		{
		//characterC.moveChar (x);
			Debug.Log ("cccc");
			if (++c > 200) {
				transform.position = correctPos;
				c = 0;
			} else {
				if (Mathf.Abs (correctPos.x) < 400 || Mathf.Abs (correctPos.y) < 100) {
					transform.position = Vector3.Lerp (transform.position, correctPos, Time.deltaTime * 2f);
					GetComponent<Rigidbody2D> ().velocity = veloc; 
				}
			}
		}	
	}



	Vector3 correctPos ;
	Vector2 veloc;

	void OnPhotonSerializeView(PhotonStream Stream,PhotonMessageInfo info){
		
		if (Stream.isWriting) {
			
		
		
				//	transform.position = correctPos;
				Stream.SendNext ((Vector3) transform.position);
				Stream.SendNext ((Vector2) GetComponent<Rigidbody2D> ().velocity);



		//	Stream.SendNext ((int) characterC.x);



		} else {

			correctPos = (Vector3)Stream.ReceiveNext ();	
			veloc = (Vector2)Stream.ReceiveNext ();	

		}
	}
}
