using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class MscoreScript : MonoBehaviour {
	Animator animator;
	public AudioClip SE;
	public GameObject balls1,textimage,textobj;//,recipebook,Panelobj;
	Renderer bookren;

	public GameObject gameobjempty;

	//ボタン等UI関連
	public GameObject button_hounou;//ボタン
	private Button Button_hounou;

	//以下画面フェード用変数
	public bool enableFade = true;
	public bool enableFadeIn = true;
	public bool enableFadeOut = true;
	public bool enableFadeOn = true;
	public float speed = 0.1f;
	public Image FadeImage;
	private float count = 1f;
	private bool enableAlphaTop = true;

	//タイマーのかわり
	//int timecounter;

	int fadeoutStarts;
	int flag_get_scenario_end;
	int GoddessFirstScenario,GoddessSecondScenario,GoddessThirdScenario,GoddessForthScenario;

	int pausetimer,pausetimer2,pausetimer3;//一時停止用タイマー
	Vector3 Nballposition;//ボールの現在地取得用
	string[] scenarios;

	//ここまで
	Image image;
	int flag_fadeon;

	int nextscenario_permission;

	int flag_senarios,flag_buttonHono;

	int cflag_count;

	void Start()
	{

		enableFade = true;
		enableFadeIn = true;
		setAlpha (FadeImage, count);

		animator = GetComponent<Animator> ();

		GoddessFirstScenario = 0;
		GoddessSecondScenario = 0;
		GoddessThirdScenario = 0;
		GoddessForthScenario = 0;

		nextscenario_permission = 0;

		Debug.Log("OpenMode");
		flag_fadeon = 1;
		flag_senarios = 0;

		fadeoutStarts = 0;
		textobj.GetComponent<CanvasRenderer> ().SetAlpha (0);
		textimage.GetComponent<CanvasRenderer> ().SetAlpha (0);
		balls1.GetComponent<ballCamera> ().set_flag_PermitMoving(1);

		gameobjempty.GetComponent<textLoad> ().shokika();

		Button_hounou = button_hounou.GetComponent<Button> ();//楽譜奉納ボタン
		Button_hounou.gameObject.SetActive (false);

		flag_buttonHono = 0;
	}

	void Update()
	{
		//Wakeupを自動で始めに行う
		//DelayMethod(60);
		if (flag_fadeon == 1) {
			toWakeUp ();
			//bookren.enabled = false;
		}

		if (flag_senarios == 1) {
			//シナリオスクリプト　チュートリアル、レシピの説明
			gameobjempty.GetComponent<textLoad> ().Readings ("textForEnd", 3);
			gameobjempty.GetComponent<textLoad> ().SetNextLine ();
			flag_senarios = 0;
			GoddessFirstScenario = 1;
		} else if (flag_senarios == 2) {//レシピを開く
			gameobjempty.GetComponent<textLoad> ().Readings ("textgetGodness", 3);
			gameobjempty.GetComponent<textLoad> ().SetNextLine ();
			flag_senarios = 0;
			GoddessSecondScenario = 1;
		} else if (flag_senarios == 3) {//植物に接触
			gameobjempty.GetComponent<textLoad> ().Readings ("textNotgetGodness", 3);
			gameobjempty.GetComponent<textLoad> ().SetNextLine ();
			flag_senarios = 0;
			GoddessThirdScenario = 1;
		}


		gameobjempty.GetComponent<textLoad> ().WriteLine ();
		flag_get_scenario_end = gameobjempty.GetComponent<textLoad> ().get_scenario_end ();
		Debug.Log ("flag_get_scenario_endS" + flag_get_scenario_end);

		balls1.GetComponent<ballCamera> ().set_flag_PermitMoving(1);//常にボールを動かす
		Nballposition = balls1.GetComponent<ballCamera> ().get_ballPosition();

		RootFilter ();

	}



	void RootFilter(){
		//箱の前にボールがきたら
		if (Nballposition.x > -2 && Nballposition.x < 2 && Nballposition.y > 0 && Nballposition.y < 4 && Nballposition.z > 22 && Nballposition.z < 24) {//身体の箱の前を設定
			balls1.GetComponent<ballCamera> ().set_flag_PermitMoving(0);
			int checker = 0;
			if (flag_buttonHono == 0 &&checker == 0) {//奉納ボタン表示
				Button_hounou.gameObject.SetActive (true);
			}else if (flag_buttonHono == 1) {//奉納ボタンがおされたら
				scenarios = gameobjempty.GetComponent<recipeSC> ().Get_RecipeInformation ();
				Button_hounou.gameObject.SetActive (false);

				for (int i = 1; i < 8; i++) {
					if (gameobjempty.GetComponent<recipeSC> ().GetRecipeIFActive (i) == 2) {//レシピのフラグを読む、２なら楽譜がある状態
						checker++;//楽譜があるならchecker をたす
					}
				}
				Debug.Log ("checker"+checker);
				if (checker == 7) {//女神の楽譜以外そろっていたら

					if (scenarios [79] == "2") {//女神の楽譜もそろっていたら
						//黒いやつあらわれる setactive?
						if(GoddessFirstScenario == 0){
							flag_senarios = 1;//シナリオ1をセット
							textobj.GetComponent<CanvasRenderer> ().SetAlpha (1);
							textimage.GetComponent<CanvasRenderer> ().SetAlpha (1);
						}
						if (GoddessFirstScenario == 1 && flag_get_scenario_end == 1) {//シナリオが終わったら
							nextscenario_permission++;
							Debug.Log ("nextscenario_permission"+nextscenario_permission);
							if (nextscenario_permission >= 25 && nextscenario_permission <= 125) {

								textobj.GetComponent<CanvasRenderer> ().SetAlpha (0);
								textimage.GetComponent<CanvasRenderer> ().SetAlpha (0);
								toSleeping();
							}else if(nextscenario_permission > 125 ){

								nextscenario_permission = 0;
								flag_get_scenario_end = 0;

								gameobjempty.GetComponent<textLoad> ().shokika();
								SceneManager.LoadScene ("Ending");//エンドシーンへ移行
								checker = 0;
							}
						}

					} else if (scenarios [79] == "0")  {//女神の楽譜がない場合
						//光をはなつアニメーション
						//女神の楽譜をてにいれる
						gameobjempty.GetComponent<recipeSC> ().SetRecipeToActiveGoddess ();
						if(GoddessSecondScenario == 0){
							flag_senarios = 2;//シナリオ2をセット 女神の楽譜をてにいれた　さいごのしあげだ！的なセリフ
							textobj.GetComponent<CanvasRenderer> ().SetAlpha (1);
							textimage.GetComponent<CanvasRenderer> ().SetAlpha (1);
						}

						if (GoddessSecondScenario == 1 && flag_get_scenario_end == 1) {//シナリオが終わったら
							nextscenario_permission++;
							if (nextscenario_permission >= 25 && nextscenario_permission < 125) {

								textobj.GetComponent<CanvasRenderer> ().SetAlpha (0);
								textimage.GetComponent<CanvasRenderer> ().SetAlpha (0);
								toSleeping();
							}else if(nextscenario_permission == 125 ){

								nextscenario_permission = 0;
								flag_get_scenario_end = 0;
								gameobjempty.GetComponent<textLoad> ().shokika();

								SceneManager.LoadScene ("mainstage1Wood");//メインステージへ移動
							}
						}

					}else if (scenarios [79] == "1"){//女神の楽譜はないが　レシピはすでに持っている場合
						if(GoddessThirdScenario == 0){
							flag_senarios = 3;//シナリオ3をセット シナリオメッセージ　まだまだ～的な
							textobj.GetComponent<CanvasRenderer> ().SetAlpha (1);
							textimage.GetComponent<CanvasRenderer> ().SetAlpha (1);
						}
						Debug.Log ("flag_get_scenario_endM" + flag_get_scenario_end);
						if (GoddessThirdScenario == 1 && flag_get_scenario_end == 1) {//シナリオが終わったら
							nextscenario_permission++;
							//Debug.Log ("nextscenario_permission" + nextscenario_permission);
							if (nextscenario_permission >= 25 && nextscenario_permission <= 125) {

								textobj.GetComponent<CanvasRenderer> ().SetAlpha (0);
								textimage.GetComponent<CanvasRenderer> ().SetAlpha (0);
								toSleeping();
							}else if(nextscenario_permission > 125 ){

								nextscenario_permission = 0;
								flag_get_scenario_end = 0;

								gameobjempty.GetComponent<textLoad> ().shokika();
								SceneManager.LoadScene ("mainstage1Wood");//メインステージへ移動
							}
						}
					}

				} else {
					if(GoddessThirdScenario == 0){
						flag_senarios = 3;//シナリオ3をセット シナリオメッセージ　まだまだ～的な
						textobj.GetComponent<CanvasRenderer> ().SetAlpha (1);
						textimage.GetComponent<CanvasRenderer> ().SetAlpha (1);
					}
					Debug.Log ("flag_get_scenario_endM" + flag_get_scenario_end);
					if (GoddessThirdScenario == 1 && flag_get_scenario_end == 1) {//シナリオが終わったら
						nextscenario_permission++;
						//Debug.Log ("nextscenario_permission" + nextscenario_permission);
						if (nextscenario_permission >= 25 && nextscenario_permission <= 125) {

							textobj.GetComponent<CanvasRenderer> ().SetAlpha (0);
							textimage.GetComponent<CanvasRenderer> ().SetAlpha (0);
							toSleeping();
						}else if(nextscenario_permission > 125 ){

							nextscenario_permission = 0;
							flag_get_scenario_end = 0;

							gameobjempty.GetComponent<textLoad> ().shokika();
							SceneManager.LoadScene ("mainstage1Wood");//メインステージへ移動
						}
					}

				}
			}

		}
	}

	public void ButtonHono_Click(){
		flag_buttonHono = 1;
	}


	private IEnumerator DelayMethod(int delayFrameCount)
	{
		for (var i = 0; i < delayFrameCount; i++)
		{
			yield return null;
		}
	}

	//以下画面フェード用関数

	private void toSleeping(){
		enableFade = true;
		if (enableFadeOut) {
			FadeOut (FadeImage);
		}
	}


	private void toWakeUp(){
		enableFade = true;
		if (enableFadeIn) {
			FadeIn (FadeImage);
		}
	}

	void setAlpha(Image image,float alpha) {
		image.color = new Color (image.color.r, image.color.g, image.color.b, alpha);
	}

	public void FadeOut(Image image) {
		if (enableFade) {
			count += speed;
			setAlpha (image, count);
			if (image.color.a >= 0.98f) {
				fadeoutStarts = 0;
				flag_fadeon = 0;
				enableFade = false;
				if (enableFadeOut) {
					Debug.Log("SleepMode");
				} 
			}
		}
	}

	public void FadeIn(Image image) {
		if (enableFade) {
			count -= speed;
			setAlpha (image, count);
			if (image.color.a <= 0.03f) {
				enableFade = false;
				enableFadeIn = false;
				flag_fadeon = 0;
				Debug.Log("WakeUpMode");
			}
		}
	}

	void FadeInAndOut(Image image) {

		if (enableFade) {
			if (!enableAlphaTop) {
				count -= speed;

				if (image.color.a <= 0.05f) {
					enableFade = false;
					enableFadeIn = false;
					enableFadeOn = false;
					flag_fadeon = 0;

					Debug.Log("flag_fadeon"+flag_fadeon);
				}

				Debug.Log ("FadeonCountS" + count);

			} else {
				count += speed;
				Debug.Log ("FadeonCount" + count);
				if (image.color.a >= 0.97f) {
					enableFade = true;
					enableFadeOn = true;
					enableAlphaTop = false;
				}
			}
			setAlpha (image, count);
			if (image.color.a <= 0.03f) {
				enableAlphaTop = true;
			}
		}
	}


	//ここまでフェード
}