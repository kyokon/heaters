using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballCamera1 : MonoBehaviour {
	public Vector3 SpeedForBall = new Vector3 (1.0f, 1.0f, 1.0f);
	public float speed = 1;
	public Vector3 Positions, CPositions;
	public int flag_PermitMoving;//動いていいか許可する0だめ1おけ
	public Vector3 beforePosition;
	public Vector3 relativePos;
	public float FirstPositionx;
	public GameObject cameraobjct;
	public Transform myTransform;

	public Terrain Terrain1wood;

	RaycastHit hit_forward, hit_right, hit_left, hit_back, hit_down;
	bool flag_hit_forward, flag_hit_right, flag_hit_left, flag_hit_back, flag_hit_down, flag_hit_up, flag_hit_totalup;
	float ballsheight, ballTotalhight;
	public int layermask;
	public Transform cameratrans;


	// Use this for initialization
	void Start () {
		FirstPositionx = transform.position.x;
		flag_PermitMoving = 0;
		flag_hit_forward = false;
		flag_hit_right = false;
		flag_hit_left = false;
		flag_hit_back = false;
		flag_hit_down = false;
		flag_hit_up = false;
		layermask = 1 << 8;
	}
	
	// Update is called once per frame
	void Update () {

		BallMove ();
	}

	void RayHitALL(){
		if ((transform.position.z > 660 && transform.position.z < 120 )|| (Physics.Raycast (transform.position, Vector3.forward, out hit_forward,3))||(Physics.Raycast (cameraobjct.transform.position, Vector3.forward, out hit_forward,3))) {
			flag_hit_forward = true;
			Debug.Log("forwardTrue");
		}else{
			flag_hit_forward = false;
		}

		if ((transform.position.x > 700 && transform.position.x < 44)||(Physics.Raycast (transform.position, Vector3.right, out hit_right,3))||(Physics.Raycast (cameraobjct.transform.position, Vector3.right, out hit_right,3))) {
			flag_hit_right = true;
			Debug.Log("RightTrue");
		} else {
			flag_hit_right = false;
		}
		if ((transform.position.x > 700 && transform.position.x < 44) ||(Physics.Raycast (transform.position, Vector3.left, out hit_left,3))||(Physics.Raycast (cameraobjct.transform.position, Vector3.left, out hit_left,3))) {
			flag_hit_left = true;
			Debug.Log("LeftTrue");
		} else {
			flag_hit_left = false;
		}
		if ((transform.position.z > 660 && transform.position.z < 120 ) ||(Physics.Raycast (transform.position, Vector3.back, out hit_back,3))||(Physics.Raycast (cameraobjct.transform.position, Vector3.back, out hit_back,3))) {
			flag_hit_back = true;
			Debug.Log("BackTrue");
		} else {
			flag_hit_back = false;
		}
		if ((Physics.Raycast (transform.position, Vector3.down, out hit_down,3))||(Physics.Raycast (cameraobjct.transform.position, Vector3.down, out hit_down,3))) {
			flag_hit_down = true;
			Debug.Log("DownTrue");
		} else {
			flag_hit_down = false;
		}
		if ((Physics.Raycast (transform.position, Vector3.down, out hit_down))||(Physics.Raycast (cameraobjct.transform.position, Vector3.down, out hit_down,3))) {
			ballsheight = hit_down.distance;
			if (ballsheight > 24.0f) {
				flag_hit_up = true;
				Debug.Log("UpTrue");
			} else {
				flag_hit_up = false;
			}
		}
		if ((Physics.Raycast (transform.position, Vector3.down, out hit_down, Mathf.Infinity, layermask))||(Physics.Raycast (cameraobjct.transform.position, Vector3.down, out hit_down, Mathf.Infinity, layermask))) {
			ballTotalhight = hit_down.distance;
			if (ballTotalhight > 75.0f) {
				flag_hit_totalup = true;
				Debug.Log("TUpTrue");
			} else {
				flag_hit_totalup = false;
			}
		}
	}


	void BallMove(){
		RayHitALL();

		if (flag_PermitMoving == 1) {
			Positions = transform.position;
			CPositions = cameraobjct.transform.position;

			Vector3 relativePos = Positions-CPositions;

			beforePosition = Positions;

			if ((Input.GetKey (KeyCode.UpArrow))&& flag_hit_forward != true && flag_hit_totalup != true) {
				Positions += cameratrans.transform.TransformDirection (Vector3.forward) * speed;

			}
			if ((Input.GetKey (KeyCode.DownArrow))&& flag_hit_back != true && flag_hit_totalup != true) {
				Positions += cameratrans.transform.TransformDirection (Vector3.back) * speed;
			}
			if ((Input.GetKey (KeyCode.RightArrow))&& flag_hit_right != true && flag_hit_totalup != true) {
				Positions += cameratrans.transform.TransformDirection (Vector3.right) * speed;
			}
			if ((Input.GetKey (KeyCode.LeftArrow))&& flag_hit_left != true && flag_hit_totalup != true) {
				Positions += cameratrans.transform.TransformDirection (Vector3.left) * speed;
			}
			if ((Input.GetKey (KeyCode.E))&& flag_hit_up != true && flag_hit_totalup != true) {
				Positions += cameratrans.transform.TransformDirection (Vector3.up) * speed;
			}
			if ((Input.GetKey(KeyCode.X))&& flag_hit_down != true) {
				Positions += cameratrans.transform.TransformDirection (Vector3.down) * speed;
			}
			if (Input.GetKey (KeyCode.W)) {

				Quaternion rotationU = Quaternion.LookRotation (relativePos);
				myTransform.rotation = rotationU;
				myTransform.RotateAround (Positions, Vector3.left, 1.0f*speed);
			}
			if (Input.GetKey (KeyCode.S)) {
				Quaternion rotationD = Quaternion.LookRotation (relativePos);
				myTransform.rotation = rotationD;
				myTransform.RotateAround (Positions, Vector3.right, 1.0f*speed);
			}
			if (Input.GetKey (KeyCode.D)) {
				relativePos = Positions-CPositions;
				Quaternion rotationR = Quaternion.LookRotation (relativePos);
				myTransform.rotation = rotationR;
				myTransform.RotateAround (Positions, Vector3.up, 1.0f*speed);
			}
			if (Input.GetKey (KeyCode.A)) {
				Quaternion rotationL = Quaternion.LookRotation (relativePos);
				myTransform.rotation = rotationL;
				myTransform.RotateAround (Positions, Vector3.down, 1.0f*speed);
			}
			if (Positions.y <= 0) {
				Positions.y = 0;
			}

			if ((this.transform.eulerAngles.x >= 28)&&(this.transform.eulerAngles.x <= 180)) {
				float yeuler = this.transform.eulerAngles.y;
				float zeuler = this.transform.eulerAngles.z;
				this.transform.rotation = Quaternion.Euler (28.0f, yeuler, zeuler);
			}else if ((Mathf.RoundToInt(this.transform.eulerAngles.x) >= 180)&&(Mathf.RoundToInt(this.transform.eulerAngles.x) <= 332)){
				float yeuler = this.transform.eulerAngles.y;
				float zeuler = this.transform.eulerAngles.z;
				this.transform.rotation = Quaternion.Euler (332.0f, yeuler, zeuler);
			}

			transform.position = Positions;
		}

	}
	//現在の位置情報を得る
	public Vector3 get_ballPosition(){
		return Positions;
	}
	//動かしていいか
	public void set_flag_PermitMoving(int flag){
		flag_PermitMoving = flag;
	}

	//ぶつかったもののゲームオブジェクトを判定
	void OnCollisionEnter(Collision collision){
		Debug.Log ("Collision");
	}


}
