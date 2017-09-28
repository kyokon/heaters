using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kakeraanimation : MonoBehaviour {

	int Kcolorflag;
	public GameObject gameobjball;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void animationtruefalse(){
		gameobjball.GetComponent<kakerabuttonscript> ().getter_Kcolorflag();
		if (Kcolorflag == 0) {
			Kcolorflag = 1;
		} else if (Kcolorflag == 1) {
			Kcolorflag = 0;
		}

		gameobjball.GetComponent<kakerabuttonscript> ().setter_Kcolorflag(Kcolorflag);
	}
}
