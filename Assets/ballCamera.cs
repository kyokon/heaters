using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballCamera : MonoBehaviour {
	public Vector3 SpeedForBall = new Vector3 (1.0f, 1.0f, 1.0f);
	public float speed = 1;
	public Vector3 Positions;
	public int flag_PermitMoving;//動いていいか許可する0だめ1おけ
	public Vector3 beforePosition;
	public float FirstPositionx;

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

			beforePosition = Positions;

			if (Input.GetKey (KeyCode.UpArrow)) {
				Positions += transform.TransformDirection (Vector3.forward) * speed;

				Debug.Log ("Forward");
			}
			if (Input.GetKey (KeyCode.DownArrow)) {
				Positions += transform.TransformDirection (Vector3.back) * speed;

				Debug.Log ("Back");
			}
			if (Input.GetKey (KeyCode.RightArrow)) {
				Positions += transform.TransformDirection (Vector3.right) * speed;
			}
			if (Input.GetKey (KeyCode.LeftArrow)) {
				Positions += transform.TransformDirection (Vector3.left) * speed;
			}
			if (Input.GetKey (KeyCode.E)) {
				Positions += transform.TransformDirection (Vector3.up) * speed;
			}
			if (Input.GetKey(KeyCode.X)) {
				Positions += transform.TransformDirection (Vector3.down) * speed;
			}

			if (Input.GetKey (KeyCode.A)) {
				transform.Rotate (new Vector3 (0, -1, 0));
			}
			if (Input.GetKey (KeyCode.D)) {
				transform.Rotate (new Vector3 (0, 1, 0));
			}
			if (Input.GetKey (KeyCode.W)) {
				transform.Rotate (new Vector3 (-1, 0, 0));
			}
			if (Input.GetKey (KeyCode.S)) {
				transform.Rotate (new Vector3 (1, 0, 0));
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
