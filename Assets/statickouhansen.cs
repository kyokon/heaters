using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statickouhansen : MonoBehaviour {
	static int flag_kouhan;
	int kouhanset;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//static変数 kouhanの呼び出し、セッティング
	public static void set_kouhan(int kouhan){
		flag_kouhan = kouhan;
		Debug.Log ("FLflag_kouhan"+flag_kouhan);
	}
		
	public static int get_kouhan(){
		return flag_kouhan;
	}
}
