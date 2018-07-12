using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class characterController : Photon.MonoBehaviour {


	//Publics
	public float normalSpeed,nosSpeed,maxSpeed,jumpPower
	,floatingDrag,acceleration,defaultDrag,manaUsageSpeed
	,manaStuffingSpeedOnFloating,oneManaValue,maxManaCount,currentMana;
	public bool isGameCanPlaying = false,isWillReset=false;
	[HideInInspector]
	public bool isMine;




	//privates
	Vector2 startPos;

	int c = 0;

	PhotonView characterPhotonView;
	public float extraSpeedOnNos;

	float extraSpeedToRotation=0;
	bool onKeyDownNos,onKeyDownJump;
	public float maxNosCount;
	public float nosCount;

	bool jumpUpped=true;

	public float speed;

	public bool isCanFloating = false,isNosUsingStarted = false,isTouchingAnyGround;//isTouchingGrpund && velocity.y<-1

	void Start () {
		
		if (maxSpeed == 0)
			maxSpeed = 10;
		if (jumpPower == 0)
			jumpPower = 10;
		if (maxSpeed == 0)
			maxSpeed = 10;
		if (floatingDrag == 0)
			floatingDrag = 5;
		if (acceleration == 0)
			acceleration = 5 ;
		if(maxNosCount==0)
			maxNosCount = 100;
		if (manaStuffingSpeedOnFloating == 0)
			manaStuffingSpeedOnFloating = 4;

		nosCount = maxNosCount;
		characterPhotonView = GetComponent<PhotonView> ();
		startPos = transform.position;
		defaultDrag = GetComponent<Rigidbody2D> ().drag;
		isMine = characterPhotonView.isMine;


		if (isMine) { 
			transform.GetChild(2).gameObject.GetComponent<SpriteRenderer> ().color = Color.green;
			GameObject.Find ("Main Camera").GetComponent<CameraTracking> ().playerTransform = gameObject.transform;
		}
		Debug.Log ("<-->" + PhotonNetwork.playerList[0].ID);


	}


	void Update () {

		if(!isGameCanPlaying)
		isGameCanPlaying = PhotonNetwork.otherPlayers.Length > 0;

		if (isGameCanPlaying) {
			

			if (Input.GetKeyDown (KeyCode.Space)) {
				onKeyDownJump = true;
				jumpUpped = true;
			}
			if (Input.GetKeyUp (KeyCode.Space)) {
				onKeyDownJump = false;
			}
			if (Input.GetKeyDown (KeyCode.Z)) {
				onKeyDownNos = true;
			}
			if (Input.GetKeyUp (KeyCode.Z)) {
				onKeyDownNos = false;
			}




			if (onKeyDownJump) {
				if (!isTouchingAnyGround && GetComponent<Rigidbody2D> ().velocity.y < -.3f) {
					isCanFloating = true;

				} else {
					isCanFloating = false;
				}

			} else {
				isCanFloating = false;
			}


			if (onKeyDownNos&&(int)nosCount>0)
				isNosUsingStarted = true;
			else
				isNosUsingStarted = false;
			if (Input.GetKeyDown (KeyCode.R))
				isWillReset = true;

		}
	}

	bool lastOnKeyDownJump=false,lastIsNosUsingStarted,lastIscanFloating = false;
	void FixedUpdate(){

		if (isGameCanPlaying) {
			go ();

			if (isWillReset) {
				this.photonView.RPC ("resetGame", PhotonTargets.All, null);
				isWillReset = false;
			}

			if (!isMine)
				return;
			if (lastOnKeyDownJump != onKeyDownJump) {
				if (onKeyDownJump) {
					jump();
				} else {
					normalizeAll ();
				}

			lastOnKeyDownJump = onKeyDownJump;
			}
			if (lastIsNosUsingStarted != isNosUsingStarted) {
				if (isNosUsingStarted)
					startNos ();
				else 
					stopNos ();


				lastIsNosUsingStarted = isNosUsingStarted;
			}
		
			if (lastIscanFloating != isCanFloating) {
				if (isCanFloating) {
					startFloating ();
					} else {
					GetComponent<Animator> ().Play ("ParachuteClose",0,0);

					normalizeAll ();
				}
				lastIscanFloating = isCanFloating;
			}
			if(isCanFloating)collectOneMana (Time.deltaTime * manaStuffingSpeedOnFloating);
		
			if (isTouchingAnyGround&&transform.rotation.z > -70 && transform.rotation.z < 0)
				extraSpeedToRotation = Mathf.Abs(transform.rotation.z) ;




		}
	
	
	}







	void startNos(){
		if (nosCount > 1) {
			extraSpeedOnNos = acceleration;
			if(isNosUsingStarted&&extraSpeedOnNos>0)
		GetComponent<Animator> ().Play ("ForceUp",0,0);
		}
	}

	void stopNos(){
		extraSpeedOnNos = 0;
		ss.stopExplosion ();
		GetComponent<Animator> ().Play ("ForceDown",0,0);
		Debug.Log ("nos started "+ c++);

	}



	void jump(){
		

		if (isTouchingAnyGround&&jumpUpped) {
			normalizeAll ();
		if (GetComponent<Rigidbody2D> ().velocity.y < maxSpeed) {
				//transform.position = position;
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, 5);
				GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 100 * jumpPower);
				jumpUpped = false;
			}
		}
	}
	public ExplosionScript ss;
	void go(){


		if (Mathf.Abs (GetComponent<Rigidbody2D> ().velocity.x) < maxSpeed+extraSpeedToRotation/5+extraSpeedOnNos*(maxSpeed/acceleration)) {
			GetComponent<Rigidbody2D> ().AddForce (new Vector2(100*(extraSpeedOnNos+extraSpeedToRotation+acceleration),0));

		}
		if (extraSpeedOnNos >0&&nosCount>0) {
			useMana ();
			ss.startExplosion ();
		}
		if (nosCount < 1&&isNosUsingStarted) {
			
			stopNos ();
		}
		speed = GetComponent<Rigidbody2D> ().velocity.x;
	}




	void OnCollisionEnter2D(Collision2D collision){
		isTouchingAnyGround = true;
		}

	void OnCollisionExit2D(Collision2D collision){
		isTouchingAnyGround = false;
	}


[PunRPC]
void resetGame(){
		gameObject.transform.position = startPos;
		gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0,0);

	}
	void normalizeAll(){
		GetComponent<Rigidbody2D> ().drag = defaultDrag;
	}

	void startFloating(){
		GetComponent<Animator> ().Play ("ParachuteOpen",0,0);
		if(GetComponent<Rigidbody2D> ().velocity.x<maxSpeed&&isCanFloating)
			GetComponent<Rigidbody2D> ().AddForce (new Vector2(100*5,0));
		GetComponent<Rigidbody2D> ().drag = 10;

	}
	public void collectOneMana(float count){
		if (nosCount < 100-count)
			nosCount += count;
		else
			nosCount = 100;
	}

	void useMana(){
		if (nosCount < 1)
			nosCount = 0;
		else
			nosCount -= 1;
	}




}
