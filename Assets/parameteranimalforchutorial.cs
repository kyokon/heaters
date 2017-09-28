using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parameteranimalforchutorial : MonoBehaviour {
	//必要なパラメーターS：　動物が持つ玉の色　なんの動きを何回したらいいか？　プレゼントの場合ほしいたまの色と数 動きにあわせたアニメーション
	public int animalnumber;
	public int s_kakera;//動物が持つ玉の色
	int[,] s_wantmoveTimes;
	public int s_move;//してほしい動きを格納する変数
	public int s_times;//動きをしてほしい回数を格納する変数
	public int s_move2;//してほしい動きを格納する変数2
	public int s_times2;//動きをしてほしい回数を格納する変数2
	public int s_move3;//した動きを格納する変数
	public int s_times3;//動きをした回数を格納する変数
	public int sflag_haveKakera;//かけらをもっているか？
	public int s_wantKakeraColor;//ほしいかけら　プレゼントの場合に使用　ない場合は"0"
	public int s_wantKakera;//ほしいかけらの数　プレゼントの場合に使用　ない場合は"0"
	public int s_wantParameter;//ほしい動きをしてもらえたときのパラメーター下降値
	public int s_NotwantParameter;//ほしい動きをしてもらえなかったときのパラメーター上昇値
	public int s_MAXHP;//動物が持つHPのMAX値 これがなくなるとかけらがもらえる
	public int s_HP;//動物が持つHP これがなくなるとかけらがもらえる
	public float s_x;//動物が持つ位置x
	public float s_y;//動物が持つ位置y
	public float s_z;//動物が持つ位置z
	Vector3 Nballposition;
	public GameObject balls1,GameobjEmpty;

	int flag_loops;

	RaycastHit hit_forward;

	public int flag_near;//動物がボールを感知したかどうか

	int flag_nearball, flag_notice;

	// Use this for initialization
	void Start () {
		s_x = this.transform.position.x;
		s_y = this.transform.position.y;
		s_z = this.transform.position.z;
		s_wantmoveTimes = new int[,]{{s_move,s_times},{s_move2,s_times2}};
		flag_loops = 1;

		flag_nearball = 0;
	}
	
	// Update is called once per frame
	void Update () {

		nearball ();
	}

	void nearball(){
		Nballposition = balls1.GetComponent<ballCamera> ().get_ballPosition();

		if ((s_x + 6) > Nballposition.x && (s_x - 6) < Nballposition.x && (s_z - 6) < Nballposition.z && (s_z + 6) > Nballposition.z&& (s_y - 8) < Nballposition.y && (s_y + 8) > Nballposition.y) {
			flag_notice++;
			Debug.Log ("Hit" + animalnumber);
			GameobjEmpty.GetComponent<kakerabuttonscript> ().Set_collision_number(animalnumber);
			GameobjEmpty.GetComponent<kakerabuttonscript> ().button_appear(this.gameObject);
			if (flag_notice == 3) {
				GameobjEmpty.GetComponent<kakerabuttonscript> ().Notice_animation ();
			}

			flag_nearball++;
			if (sflag_haveKakera == 0) {
				GameobjEmpty.GetComponent<kakerabuttonscript> ().Set_collision_number (0);
				GameobjEmpty.GetComponent<kakerabuttonscript> ().button_disappearFromPARAMETER ();
				Debug.Log ("OBJECTEND");
				flag_notice = 0;
			}
		} else {
			if (flag_nearball >= 1) {
				GameobjEmpty.GetComponent<kakerabuttonscript> ().Set_collision_number (0);
				GameobjEmpty.GetComponent<kakerabuttonscript> ().button_disappear();
				flag_loops = 1;
				//Debug.Log ("NGflag_nearball" + flag_nearball);
				flag_nearball = 0;
				flag_notice = 0;
			}
		}
	}
	public string getter_animalname(){
		return this.gameObject.name;
	}
	public int getter_animalnumber(){
		return animalnumber;
	}
	public int getter_s_Kakera(){
		return s_kakera;
	}
	public int[,] getter_s_move(){
		return s_wantmoveTimes;
	}
	public int getter_sflag_haveKakera(){
		return sflag_haveKakera;
	}

	public void setter_sflag_haveKakera(int haveKakera){
		sflag_haveKakera = haveKakera;
	}


	public int getter_s_wantKakeraColor(){
		return s_wantKakeraColor;
	}
	public int getter_s_wantKakera(){
		return s_wantKakera;
	}

	public int[] getter_s_HPONwantParameter (){
		int[] HPONwantParameter = new int[]{ s_wantParameter, s_NotwantParameter, s_MAXHP};
		return HPONwantParameter;
	}
	public int getter_s_MAXHP(){
		return s_MAXHP;
	}
	public void setter_s_HP(int nowHP){
		s_HP = nowHP;
	}
	public void setter_flag_loop(int flag_loop){
		flag_loops = flag_loop;
	}
	public int getter_flag_loop(){
		return flag_loops;
	}
}
