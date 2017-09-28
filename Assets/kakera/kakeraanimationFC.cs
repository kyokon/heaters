using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kakeraanimationFC : MonoBehaviour {

	int Kcolorflag;
	public GameObject gameobjball;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		animationtruefalse ();
	}


	public void animationtruefalse(){
		gameobjball.GetComponent<chutorial2> ().getter_Kcolorflag();
		if (Kcolorflag == 0) {
			Kcolorflag = 1;
		} else if (Kcolorflag == 1) {
			Kcolorflag = 0;
		}
		Debug.Log ("KCOlorflagS" + Kcolorflag);
		gameobjball.GetComponent<chutorial2> ().setter_Kcolorflag(Kcolorflag);
	}
}
