using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionScript : MonoBehaviour {
	
	void Start () {

		PhotonNetwork.ConnectUsingSettings ("0.1");
		PhotonNetwork.autoJoinLobby = true;
	}



	void OnJoinedLobby(){
		CreateRoom ("AppsBt");

	}

	void CreateRoom(string roomId){
		RoomOptions rr = new RoomOptions ();
		rr.MaxPlayers = 3;

		PhotonNetwork.JoinOrCreateRoom (roomId,rr,TypedLobby.Default);
	}
	void OnPhotonJoinRoomFailed(){	
		Debug.Log ("error");
			}
	// Update is called once per frame
	void Update () {
		Debug.Log (PhotonNetwork.room);
	}

}
