using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class kakerabuttonscript : MonoBehaviour {
	public GameObject button_stroke,button_present,button_hit,button_cry,button_leave;
	public int button_working;
	public GameObject gameobj_target;
	Animator animator, animator_kakeraRed, animator_kakeraBlue, animator_kakeraGreen;
	public GameObject obj_kakeraRed, obj_kakeraBlue, obj_kakeraGreen;
	Animator animator_gakuhu;
	public int count_stroke, count_present, count_hit, count_cry, count_total;	
	private Button Button_stroke, Button_present, Button_hit, Button_cry, Button_leave; 
	public int collisionobject_number;

	Vector3 Nballposition;
	public GameObject balls1,gameobjempty;

	//parameterForanimal
	int animalnumberFK;
	int s_kakeraFK;//動物が持つ玉の色
	int[][] s_wantmoveTimesFK;
	int s_moveFK;//してほしい動きを格納する変数
	int s_timesFK;//動きをしてほしい回数を格納する変数
	int s_move2FK;//してほしい動きを格納する変数
	int s_times2FK;//動きをしてほしい回数を格納する変数
	int sflag_haveKakeraFK;//かけらをもっているか？
	int s_wantKakeraColorFK;//ほしいかけら　プレゼントの場合に使用　ない場合は"0"
	int s_wantKakeraFK;//ほしいかけらの数　プレゼントの場合に使用　ない場合は"0"
	int s_wantParameterFK;//ほしい動きをしてもらえたときのパラメーター下降値
	int s_NotwantParameterFK;//ほしい動きをしてもらえなかったときのパラメーター上昇値
	int s_HPFK;//動物が持つHP これがなくなるとかけらがもらえる
	string animalnames;

	int[,]obj_wantMoveandTimes;
	int[] HPONwantParameter;
	int[,] collisions;
	int[] completeparameter;

	int trycatcher;
	int numberOfwantmove;
	int flagloop;
	int flag_dissapear;
	int Kcolor;
	int Kcolorflag;


	// Use this for initialization
	void Start () {
		Button_stroke = button_stroke.GetComponent<Button> ();
		Button_present = button_present.GetComponent<Button> ();
		Button_hit = button_hit.GetComponent<Button> ();
		Button_cry = button_cry.GetComponent<Button> ();
		Button_leave = button_leave.GetComponent<Button> ();

		Button_stroke.gameObject.SetActive (false);
		Button_present.gameObject.SetActive (false);
		Button_hit.gameObject.SetActive (false);
		Button_cry.gameObject.SetActive (false);
		Button_leave.gameObject.SetActive (false);

		button_working = 0;

		gameobj_target = null;
		count_stroke = 0;
		count_present = 0;
		count_hit = 0;
		count_cry = 0;
		count_total = 0;

		collisionobject_number = 0;

		trycatcher = 0;
		completeparameter = new int[2];


		animator_kakeraRed = obj_kakeraRed.GetComponent<Animator> ();

		animator_kakeraBlue = obj_kakeraBlue.GetComponent<Animator> ();

		animator_kakeraGreen  = obj_kakeraGreen.GetComponent<Animator> ();
		Kcolorflag = 0;

	}

	// Update is called once per frame
	void Update () {
		animationtrue ();
	}

	public void button_appear(GameObject target){

		Debug.Log ("collisionobject_number" + collisionobject_number);
		if (collisionobject_number != 0) {

			gameobj_target = target;
			sflag_haveKakeraFK = gameobj_target.GetComponent<parameteranimal> ().getter_sflag_haveKakera();
			flagloop = gameobj_target.GetComponent<parameteranimal> ().getter_flag_loop();
			Debug.Log ("sflag_haveKakeraFK ==" + sflag_haveKakeraFK);

			if (sflag_haveKakeraFK == 1) {
				Button_stroke.gameObject.SetActive (true);
				Button_present.gameObject.SetActive (true);
				Button_hit.gameObject.SetActive (true);
				Button_cry.gameObject.SetActive (true);
				Button_leave.gameObject.SetActive (true);
				button_working = 1;
				animalnames = target.name;
				animator = gameobj_target.GetComponent<Animator> ();
				if (trycatcher == 0 && flagloop == 1) {
					shokikaCounter ();
					obj_wantMoveandTimes = gameobj_target.GetComponent<parameteranimal> ().getter_s_move ();
					HPONwantParameter = gameobj_target.GetComponent<parameteranimal> ().getter_s_HPONwantParameter ();
					gameobj_target.GetComponent<parameteranimal> ().setter_flag_loop (0);

					s_wantParameterFK = HPONwantParameter [0];
					s_NotwantParameterFK = HPONwantParameter [1];
					s_HPFK = HPONwantParameter [2];


					if (obj_wantMoveandTimes [0, 1] == 0) {
						completeparameter [0] = 1;
					}else if(obj_wantMoveandTimes [1, 1] == 0){
						completeparameter [1] = 1;
					}

					Debug.Log ("SHOKIKA "+trycatcher);
				}
				trycatcher ++;
				if(completeparameter[0]==1&&completeparameter[1]==1){

					Nballposition = balls1.GetComponent<ballCamera1> ().get_ballPosition();

					Kcolor = gameobj_target.GetComponent<parameteranimal> ().getter_s_Kakera ();//かけらの色の情報をゲット
					switch (Kcolor) {
					case 1:
						obj_kakeraRed.transform.position = new Vector3 (Nballposition.x, Nballposition.y, Nballposition.z);
						obj_kakeraRed.gameObject.SetActive (true);
						animator_kakeraRed.Play ("getk");
							break;
					case 2:
						obj_kakeraBlue.transform.position = new Vector3 (Nballposition.x, Nballposition.y, Nballposition.z);
						obj_kakeraBlue.gameObject.SetActive (true);
						animator_kakeraBlue.Play ("getk");
							break;
					case 3:
						obj_kakeraGreen.transform.position = new Vector3 (Nballposition.x, Nballposition.y, Nballposition.z);
						obj_kakeraGreen.gameObject.SetActive (true);
						animator_kakeraGreen.Play ("getk");
							break;
					}

					gameobjempty.GetComponent<recipebuttonscript> ().SetKakeraToRecipe(Kcolor);
					Debug.Log ("SetKakeraToRecipe");

					sflag_haveKakeraFK = 0;
					gameobj_target.GetComponent<parameteranimal> ().setter_sflag_haveKakera(sflag_haveKakeraFK);
					completeparameter [0] = 0;
					completeparameter [1] = 0;
				}
			}
		}
	}

	public void setter_Kcolorflag(int Kcolorflags){
		Kcolorflag = Kcolorflags;
	}
	public int getter_Kcolorflag(){
		return Kcolorflag;
	}

	public void animationtrue(){

		if (Kcolorflag == 1) {
			obj_kakeraRed.gameObject.SetActive (true);
			obj_kakeraBlue.gameObject.SetActive (true);
			obj_kakeraGreen.gameObject.SetActive (true);
		}else if (Kcolorflag == 0) {
			obj_kakeraRed.gameObject.SetActive (false);
			obj_kakeraBlue.gameObject.SetActive (false);
			obj_kakeraGreen.gameObject.SetActive (false);
		}
	}
	public void shokikaCounter(){
		count_stroke = 0;
		count_present = 0;
		count_hit = 0;
		count_cry = 0;
		count_total = 0;
	}

	public void button_disappear(){
		Button_stroke.gameObject.SetActive (false);
		Button_present.gameObject.SetActive (false);
		Button_hit.gameObject.SetActive (false);
		Button_cry.gameObject.SetActive (false);
		Button_leave.gameObject.SetActive (false);

		button_working = 0;
		gameobj_target = null;
		trycatcher = 0;
	}

	public void button_disappearFromPARAMETER(){
		if (collisionobject_number == 0) {
			Button_stroke.gameObject.SetActive (false);
			Button_present.gameObject.SetActive (false);
			Button_hit.gameObject.SetActive (false);
			Button_cry.gameObject.SetActive (false);
			Button_leave.gameObject.SetActive (false);
			button_working = 0;
			gameobj_target = null;
			trycatcher = 0;
		}
	}


	public void button_Stroke_Click(){
		for (int i = 0; i < 2; i++) {
			if (obj_wantMoveandTimes [i, 0] == 1) {
				numberOfwantmove = i;
			}
		}

		if (obj_wantMoveandTimes [0, 0] == 1 || obj_wantMoveandTimes [1, 0] == 1) {
			s_HPFK = s_HPFK - s_wantParameterFK;
			if ((s_HPFK / HPONwantParameter [2]) < 0.5) {
				Debug.Log ("animation_relax" + gameobj_target.name);
				animator.Play ("relax");
			}
			if (s_HPFK < 1 && count_stroke >= obj_wantMoveandTimes [numberOfwantmove,1] ) {
				Debug.Log ("animation_happy" + gameobj_target.name);
				animator.Play ("happy");
				completeparameter[numberOfwantmove] = 1;
			}

			Debug.Log ("count_stroke"+count_stroke);
			Debug.Log ("NowHp"+s_HPFK);
			count_stroke++;
		}else{
			if (s_HPFK < HPONwantParameter [2]) {
				s_HPFK = s_HPFK + s_NotwantParameterFK;
			}
			if ((s_HPFK / HPONwantParameter [2]) >= 0.5&&(s_HPFK / HPONwantParameter [2]) < 1) {
				animator.Play ("sad");
			}
			if (s_HPFK >= HPONwantParameter [2]) {
				animator.Play ("angry");
			}
			count_stroke++;
		}

	}

	public void Notice_animation(){
		animator.Play ("notice");
	}

	public int Getbutton_Stroke_Click(){
		return count_stroke;
	}
	public void button_Present_Click(){
		s_wantKakeraColorFK = gameobj_target.GetComponent<parameteranimal> ().getter_s_wantKakeraColor ();
		for (int i = 0; i < 2; i++) {
			if (obj_wantMoveandTimes [i, 0] == 2) {
				numberOfwantmove = i;
			}
		}

		if (obj_wantMoveandTimes [0, 0] == 2 || obj_wantMoveandTimes [1, 0] == 2) {
			s_HPFK = s_HPFK - s_wantParameterFK;
			if ((s_HPFK / HPONwantParameter [2]) < 0.5) {
				//現在持ってるかけら＜ほしいかけら１こ　の場合 angry 再生して強制的に終わらせる
				int Total_wantkakera = gameobjempty.GetComponent<recipebuttonscript> ().kakeraInformation(s_wantKakeraColorFK);
				if (Total_wantkakera < 1) {
					animator.Play ("angry");
					completeparameter [numberOfwantmove] = 1;
				} else {
					//現在持ってるかけら＞＝ほしいかけら１こ　の場合
					gameobjempty.GetComponent<recipebuttonscript> ().SetMinusKakeraToRecipeForPresent (s_wantKakeraColorFK);
					Debug.Log ("animation_relax" + gameobj_target.name);
					animator.Play ("relax");
				}
			}
			if (s_HPFK < 1 && count_present >= obj_wantMoveandTimes [numberOfwantmove,1] ) {
				//現在持ってるかけら＜ほしいかけら１こ　の場合 angry 再生して強制的に終わらせる
				int Total_wantkakera = gameobjempty.GetComponent<recipebuttonscript> ().kakeraInformation(s_wantKakeraColorFK);
				if (Total_wantkakera < 1) {
					animator.Play ("angry");
					completeparameter [numberOfwantmove] = 1;
				} else {
					//現在持ってるかけら＞＝ほしいかけら１こ　の場合
					gameobjempty.GetComponent<recipebuttonscript> ().SetMinusKakeraToRecipeForPresent (s_wantKakeraColorFK);
					Debug.Log ("animation_happy" + gameobj_target.name);
					animator.Play ("happy");
					completeparameter [numberOfwantmove] = 1;
				}
			}
			Debug.Log ("count_stroke"+count_present);
			Debug.Log ("NowHp"+s_HPFK);
			count_present++;
		}else{
			if (s_HPFK < HPONwantParameter [2]) {
				s_HPFK = s_HPFK + s_NotwantParameterFK;
			}
			if ((s_HPFK / HPONwantParameter [2]) >= 0.5&&(s_HPFK / HPONwantParameter [2]) < 1) {
				animator.Play ("sad");
			}
			if (s_HPFK >= HPONwantParameter [2]) {
				animator.Play ("angry");
			}
			count_present++;
		}

	}
	public int Getbutton_Present_Click(){
		return count_present;
	}
	public void button_Hit_Click(){
		for (int i = 0; i < 2; i++) {
			if (obj_wantMoveandTimes [i, 0] == 1) {
				numberOfwantmove = i;
			}
		}

		if (obj_wantMoveandTimes [0, 0] == 1 || obj_wantMoveandTimes [1, 0] == 1) {
			s_HPFK = s_HPFK - s_wantParameterFK;
			if ((s_HPFK / HPONwantParameter [2]) < 0.5) {
				Debug.Log ("animation_relax" + gameobj_target.name);
				animator.Play ("relax");
			}
			if (s_HPFK < 1 && count_hit >= obj_wantMoveandTimes [numberOfwantmove,1] ) {
				Debug.Log ("animation_happy" + gameobj_target.name);
				animator.Play ("happy");
				completeparameter[numberOfwantmove] = 1;
			}

			Debug.Log ("count_hit"+count_hit);
			Debug.Log ("NowHp"+s_HPFK);
			count_hit++;
		}else{
			if (s_HPFK < HPONwantParameter [2]) {
				s_HPFK = s_HPFK + s_NotwantParameterFK;
			}
			if ((s_HPFK / HPONwantParameter [2]) >= 0.5&&(s_HPFK / HPONwantParameter [2]) < 1) {
				animator.Play ("sad");
			}
			if (s_HPFK >= HPONwantParameter [2]) {
				animator.Play ("angry");
			}
			count_hit++;
		}

	}
	public int Getbutton_Hit_Click(){
		return count_hit;
	}
	public void button_Cry_Click(){
		for (int i = 0; i < 2; i++) {
			if (obj_wantMoveandTimes [i, 0] == 1) {
				numberOfwantmove = i;
			}
		}

		if (obj_wantMoveandTimes [0, 0] == 1 || obj_wantMoveandTimes [1, 0] == 1) {
			s_HPFK = s_HPFK - s_wantParameterFK;
			if ((s_HPFK / HPONwantParameter [2]) < 0.5) {
				Debug.Log ("animation_relax" + gameobj_target.name);
				animator.Play ("relax");
			}
			if (s_HPFK < 1 && count_cry >= obj_wantMoveandTimes [numberOfwantmove,1] ) {
				Debug.Log ("animation_happy" + gameobj_target.name);
				animator.Play ("happy");
				completeparameter[numberOfwantmove] = 1;
			}

			Debug.Log ("count_cry"+count_cry);
			Debug.Log ("NowHp"+s_HPFK);
			count_cry++;
		}else{
			if (s_HPFK < HPONwantParameter [2]) {
				s_HPFK = s_HPFK + s_NotwantParameterFK;
			}
			if ((s_HPFK / HPONwantParameter [2]) >= 0.5&&(s_HPFK / HPONwantParameter [2]) < 1) {
				animator.Play ("sad");
			}
			if (s_HPFK >= HPONwantParameter [2]) {
				animator.Play ("angry");
			}
			count_cry++;
		}
	}
	public int Getbutton_Cry_Click(){
		return count_cry;
	}


	public void Set_collision_number(int collision_number){//衝突したものの番号を得る
		collisionobject_number = collision_number;

		Debug.Log("animal_ollision_number"+collisionobject_number);
	}

	public void setter_animalname(string animalname){
		animalnames = animalname;
	}
	public void setter_animalnumber(int animalnumber){
		animalnumberFK = animalnumber;
	}
	public void setter_s_Kakera(int kakeraCol){//動物が持つかけらの色
		s_kakeraFK = kakeraCol;
	}
	public void setter_s_move(int move){//してほしい動きを格納する変数
		s_moveFK = move;
	}
	public void setter_s_times(int movetimes){//動きをしてほしい回数を格納する変数
		s_timesFK = movetimes;
	}
	public void setter_sflag_haveKakera(int flag_kakera){//かけらをもっているか？
		sflag_haveKakeraFK = flag_kakera;
	}
	public void setter_s_wantKakeraColor(int WantKakeraColor){//ほしいかけらの色　プレゼントの場合に使用　ない場合は"black"
		s_wantKakeraColorFK = WantKakeraColor;
	}
	public void setter_s_wantKakera(int wantKakeraTimes){//ほしいかけらの数　プレゼントの場合に使用　ない場合は"black"
		s_wantKakeraFK = wantKakeraTimes;
	}
	public void setter_s_wantParameter(int value_wantmove){//ほしい動きをしてもらえたときのパラメーター下降値
		s_wantParameterFK = value_wantmove;
	}
	public void setter_s_NotwantParameter(int value_Notwantmove){//ほしい動きをしてもらえなかったときのパラメーター上昇値
		s_NotwantParameterFK = value_Notwantmove;
	}
	//動物が持つHP これがなくなるとかけらがもらえる
	public void setter_s_HP(int HP_animal){
		s_HPFK = HP_animal;
	}

}