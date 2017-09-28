using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballCamera : MonoBehaviour {
	public Vector3 SpeedForBall = new Vector3 (1.0f, 1.0f, 1.0f);
	public float speed = 1;
	public Vector3 Positions,CPositions;
	public int flag_PermitMoving;//動いていいか許可する0だめ1おけ
	public Vector3 beforePosition;
	public float FirstPositionx;

	public GameObject cameraobjct;
	public Transform cameratrans;
	Vector3 relativePos;
	public Transform myTransform;

	// Use this for initialization
	void Start () {
		FirstPositionx = transform.position.x;
		flag_PermitMoving = 0;
	}
	
	// Update is called once per frame
	void Update () {
		BallMove ();
	}


	void BallMove(){
		if (flag_PermitMoving == 1) {
			Positions = transform.position;

			CPositions = cameraobjct.transform.position;

			relativePos = Positions-CPositions;

			beforePosition = Positions;

			if (Input.GetKey (KeyCode.UpArrow)) {
				Positions += cameratrans.transform.TransformDirection (Vector3.forward) * speed;

				Debug.Log ("Forward");
			}
			if (Input.GetKey (KeyCode.DownArrow)) {
				Positions += cameratrans.transform.TransformDirection (Vector3.back) * speed;

				Debug.Log ("Back");
			}
			if (Input.GetKey (KeyCode.RightArrow)) {
				Positions += cameratrans.transform.TransformDirection (Vector3.right) * speed;
			}
			if (Input.GetKey (KeyCode.LeftArrow)) {
				Positions += cameratrans.transform.TransformDirection (Vector3.left) * speed;
			}
			if (Input.GetKey (KeyCode.E)) {
				Positions += cameratrans.transform.TransformDirection (Vector3.up) * speed;
			}
			if (Input.GetKey(KeyCode.X)) {
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
		set_flag_PermitMoving (0);
		Positions = beforePosition;
		//Positions +=transform.TransformDirection (Vector3.forward) * 0;
		//Destroy (collision.gameObject);
		set_flag_PermitMoving (1);
		Debug.Log ("Collision");
	}


}
