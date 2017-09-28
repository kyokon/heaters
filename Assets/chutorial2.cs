using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class chutorial2 : MonoBehaviour {
	public Animator animator1, animator2;
	public Animator animator, animator_kakeraRed, animator_kakeraBlue, animator_kakeraGreen;
	public GameObject obj_kakeraRed, obj_kakeraBlue, obj_kakeraGreen;
	public AudioClip SE;
	public GameObject balls1,GameobjForScript,textimage,textobj,Panelobj,grass,rabbit;


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

	int pausetimer,pausetimer2,pausetimer3,pausetimer_permission;//一時停止用タイマー
	Vector3 Nballposition;//ボールの現在地取得用

	//ここまで
	Image image;
	int flag_fadeon;

	int flag_recipe_working;

	int flag_senarios;
	int chutorialFirstStep,chutorialSecondStep,chutorialThirdStep,chutorialForthStep,chutorialFifthStep;
	int chutorialSixStep,chutorialFinalStep;
	int Firstball;
	int Forcontinue;
	int grass_anime;
	int rabbit_anime;

	int Kcolorflag;

	public Button Button_stroke, Button_present, Button_hit, Button_cry, Button_leave, Button_Recipe;
	int clicke_present,clicke_stroke;
	int Button_recipeallow;

	void Awake(){
		GameobjForScript.GetComponent<recipebuttonscript> ().ShokikatextToRecipe();
		GameobjForScript.GetComponent<recipebuttonscript> ().SetRecipeToActive(1);
		Debug.Log ("awake");
	}

	void Start()
	{
		Button_recipeallow = 1;

		enableFade = true;
		enableFadeIn = true;
		setAlpha (FadeImage, count);

		animator1 = grass.GetComponent<Animator> ();
		animator2 = rabbit.GetComponent<Animator> ();

		animator_kakeraRed = obj_kakeraRed.GetComponent<Animator> ();
		animator_kakeraBlue = obj_kakeraBlue.GetComponent<Animator> ();
		animator_kakeraGreen  = obj_kakeraGreen.GetComponent<Animator> ();

		Debug.Log("OpenMode");
		flag_fadeon = 1;
		//timecounter = 0;
		fadeoutStarts = 0;
		flag_senarios = 1;
		flag_get_scenario_end = 1;
		pausetimer = 0;
		pausetimer2 = 0;
		pausetimer3 = 0;

		chutorialFirstStep = 0;
		chutorialSecondStep = 0;
		chutorialThirdStep = 0;
		chutorialForthStep = 0;
		chutorialFifthStep = 0;
		pausetimer_permission = 1;
		Firstball = 1;
		Forcontinue = 0;
		clicke_present = 2;
		clicke_stroke = 2;

		Kcolorflag = 0;

		obj_kakeraRed.gameObject.SetActive (false);
		obj_kakeraBlue.gameObject.SetActive (false);
		obj_kakeraGreen.gameObject.SetActive (false);


		Button_stroke.gameObject.SetActive (false);
		Button_present.gameObject.SetActive (false);
		Button_hit.gameObject.SetActive (false);
		Button_cry.gameObject.SetActive (false);
		Button_leave.gameObject.SetActive (false);

	}

	void Update()
	{

		//Wakeupを自動で始めに行う
		if (flag_fadeon == 1) {
			toWakeUp ();

		}
		if (Button_recipeallow == 1) {
			Button_Recipe.gameObject.SetActive (true);
		} else {
			Button_Recipe.gameObject.SetActive (false);
		}

		if (flag_senarios == 1) {
			//シナリオスクリプト　チュートリアル、レシピの説明
			GameobjForScript.GetComponent<textLoad> ().Readings ("textchutorial2",5);
			GameobjForScript.GetComponent<textLoad> ().SetNextLine ();
			flag_senarios = 0;
			chutorialFirstStep = 1;
			GameobjForScript.GetComponent<recipebuttonscript> ().set_recipe_Buttonworking ();
		} else if (flag_senarios == 2) {//レシピを開く
			GameobjForScript.GetComponent<textLoad> ().Readings ("textchutorialOpenrecipe",5);
			GameobjForScript.GetComponent<textLoad> ().SetNextLine ();
			flag_senarios = 0;
			chutorialSecondStep = 1;
		} else if (flag_senarios == 3) {//植物に接触
			GameobjForScript.GetComponent<textLoad> ().Readings ("textgetshokubutu",3);
			GameobjForScript.GetComponent<textLoad> ().SetNextLine ();
			flag_senarios = 0;
			chutorialThirdStep = 1;
		} else if (flag_senarios == 4) {//植物に接触　動作後
			GameobjForScript.GetComponent<textLoad> ().Readings ("textgetshokubutu2",3);
			GameobjForScript.GetComponent<textLoad> ().SetNextLine ();
			flag_senarios = 0;
			chutorialForthStep = 1;
			Debug.Log ("CFSS" + chutorialForthStep);
		} else if (flag_senarios == 5) {//動物に接触
			GameobjForScript.GetComponent<textLoad> ().Readings ("textgetanimal",3);
			GameobjForScript.GetComponent<textLoad> ().SetNextLine ();
			flag_senarios = 0;
			chutorialFifthStep = 1;
		} else if (flag_senarios == 6) {//動物に接触　動作後
			GameobjForScript.GetComponent<textLoad> ().Readings ("textgetanimal2",8);
			GameobjForScript.GetComponent<textLoad> ().SetNextLine ();
			flag_senarios = 0;
			chutorialSixStep = 1;
		} else if (flag_senarios == 7) {//シナリオ、チュートリアルのさいご
			GameobjForScript.GetComponent<textLoad> ().Readings ("textchutorialEnd",3);
			GameobjForScript.GetComponent<textLoad> ().SetNextLine ();
			flag_senarios = 0;
			chutorialFinalStep = 1;
		}
		//シナリオスクリプト　テキストのかきこみ
		GameobjForScript.GetComponent<textLoad> ().WriteLine ();
		//シナリオスクリプト　シナリオがおわったかを判定
		flag_get_scenario_end = GameobjForScript.GetComponent<textLoad> ().get_scenario_end ();
		Debug.Log ("SFLAGscenario_end" + flag_get_scenario_end);


		//レシピをひらいているかどうか　1開　2閉
		flag_recipe_working = GameobjForScript.GetComponent<recipebuttonscript> ().get_recipe_working();
		//シナリオ１終わり　＆レシピが開かれている場合
		if (flag_get_scenario_end == 1 && chutorialFirstStep == 1 && flag_recipe_working == 1) {

			Button_recipeallow = 0;
			if (pausetimer_permission == 1) {
				pausetimer++;
				if (pausetimer == 15) {
					GameobjForScript.GetComponent<textLoad> ().shokika ();//テキストの初期化
					pausetimer = 0;
					flag_senarios = 2;//シナリオ２をセット
					pausetimer_permission = 0;
				}
			}
		}


		if (flag_recipe_working == 1 && chutorialSecondStep == 1 && flag_get_scenario_end == 1) {
			Button_recipeallow = 1;
		}


		//シナリオ　レシピの説明2がおわったら
		if (flag_recipe_working == 0&&chutorialSecondStep == 1&&flag_get_scenario_end == 1) {

			GameobjForScript.GetComponent<textLoad> ().shokika();//テキストを初期化
			balls1.GetComponent<ballCamera> ().set_flag_PermitMoving(1);//ボール動作許可

				//テキストの部分をみえなくする
				textobj.GetComponent<CanvasRenderer> ().SetAlpha (0);
				textimage.GetComponent<CanvasRenderer> ().SetAlpha (0);
				GameobjForScript.GetComponent<textLoad> ().shokika();
				pausetimer_permission = 1;
				chutorialSecondStep = 0;//チュートリアル第二ステップ終了の合図

		}


		//ボールの位置をとる
		Nballposition = balls1.GetComponent<ballCamera> ().get_ballPosition();
		//ボールが指定の位置に来た場合
		if (Nballposition.z >= 14 && Nballposition.z <= 16 && Nballposition.x <= 4 && Nballposition.x >= 2 && Firstball == 1) {
			Button_recipeallow = 0;
			balls1.GetComponent<ballCamera> ().set_flag_PermitMoving (0);//ボールをとめる
			if (pausetimer_permission == 1 && chutorialThirdStep == 0) {
				pausetimer++;

				if (pausetimer == 15) {
					textobj.GetComponent<CanvasRenderer> ().SetAlpha (1);
					textimage.GetComponent<CanvasRenderer> ().SetAlpha (1);
					GameobjForScript.GetComponent<textLoad> ().shokika ();

					pausetimer = 0;
					flag_senarios = 3;//シナリオ３をセット
					pausetimer_permission = 0;
					animator1.Play ("chutorialgrass");//15コマすぎたら草を動かす

					Button_stroke.gameObject.SetActive (true);//ボタンを表示、使用可能にする
					Button_present.gameObject.SetActive (true);
					Button_hit.gameObject.SetActive (true);
					Button_cry.gameObject.SetActive (true);
					Button_leave.gameObject.SetActive (true);
					clicke_stroke = 0;//ボタンを押したときにレシピへの登録とアニメーションを再生可能にする


				}
			}
			//ボタンがおされるとgrass_animeが 1になる　以下実行可能になる
			if (pausetimer_permission == 0 && chutorialThirdStep == 1 && grass_anime == 1) {
				pausetimer++;
				if (pausetimer == 5) {
					Kcolorflag = 1;

					obj_kakeraGreen.gameObject.SetActive (true);
					pausetimer = 0;
					animator1.Play ("happy");//5コマすぎたら草を動かす
					animator_kakeraGreen.Play ("getk");
					GameobjForScript.GetComponent<textLoad> ().shokika ();
					flag_senarios = 4;//４番目のシナリオ開放
					pausetimer_permission = 1;
					Button_stroke.gameObject.SetActive (false);
					Button_present.gameObject.SetActive (false);
					Button_hit.gameObject.SetActive (false);
					Button_cry.gameObject.SetActive (false);
					Button_leave.gameObject.SetActive (false);
				}
			}
			Debug.Log ("flag_get_scenario_end" + flag_get_scenario_end);
			if(flag_get_scenario_end == 1&&chutorialForthStep == 1&&pausetimer_permission == 1){
				pausetimer++;
				if (pausetimer == 30) {
					Kcolorflag = 0;

					obj_kakeraGreen.gameObject.SetActive (false);
					textobj.GetComponent<CanvasRenderer> ().SetAlpha (0);
					textimage.GetComponent<CanvasRenderer> ().SetAlpha (0);
					balls1.GetComponent<ballCamera> ().set_flag_PermitMoving(1);
					Firstball = 2;
					pausetimer = 0;
				}
			}

		}

		if (Nballposition.z >=-9 && Nballposition.z <= -1 && Nballposition.x <= -18 && Nballposition.x >= -21 && Firstball == 2) {
			Button_recipeallow = 0;
			balls1.GetComponent<ballCamera> ().set_flag_PermitMoving(0);
			Debug.Log ("pausetimer_permission" + pausetimer_permission);
			Debug.Log ("chutorialFifthStep" + chutorialFifthStep);

			if (pausetimer_permission == 1&&chutorialFifthStep == 0) {
				pausetimer++;

				Debug.Log ("BALL2pausetimer" + pausetimer);
				if (pausetimer == 15) {
					textobj.GetComponent<CanvasRenderer> ().SetAlpha (1);
					textimage.GetComponent<CanvasRenderer> ().SetAlpha (1);
					GameobjForScript.GetComponent<textLoad> ().shokika ();
					pausetimer = 0;
					flag_senarios = 5;
					pausetimer_permission = 0;
					animator2.Play("chutorialrabbit");
					Button_stroke.gameObject.SetActive (true);
					Button_present.gameObject.SetActive (true);
					Button_hit.gameObject.SetActive (true);
					Button_cry.gameObject.SetActive (true);
					Button_leave.gameObject.SetActive (true);
					clicke_present = 0;//プレゼントボタンを開放、実行可能にする
				}
			}

			if (pausetimer_permission == 0 && chutorialFifthStep == 1 && rabbit_anime == 1) {
				pausetimer++;
				Debug.Log ("Setpausetimer" + pausetimer);
				if (pausetimer == 5) {

					obj_kakeraRed.gameObject.SetActive (true);
					pausetimer = 0;
					animator2.Play ("happy");//5コマすぎたら草を動かす
					animator_kakeraRed.Play ("getk");
					GameobjForScript.GetComponent<textLoad> ().shokika ();
					flag_senarios = 6;
					pausetimer_permission = 1;
					Button_stroke.gameObject.SetActive (false);
					Button_present.gameObject.SetActive (false);
					Button_hit.gameObject.SetActive (false);
					Button_cry.gameObject.SetActive (false);
					Button_leave.gameObject.SetActive (false);
				}
			}
			Debug.Log ("flag_get_scenario_end" + flag_get_scenario_end);
			if(flag_get_scenario_end == 1&&chutorialSixStep == 1&&pausetimer_permission == 1){
				pausetimer++;
				if (pausetimer == 30) {

					obj_kakeraRed.gameObject.SetActive (false);
					textobj.GetComponent<CanvasRenderer> ().SetAlpha (0);
					textimage.GetComponent<CanvasRenderer> ().SetAlpha (0);
					balls1.GetComponent<ballCamera> ().set_flag_PermitMoving(1);
					Firstball = 3;
					pausetimer = 0;
				}
			}

		}

		if (Nballposition.z >= -54 && Nballposition.z <= -45 && Nballposition.x <= 6 && Nballposition.x >= -6 && Firstball == 3) {

			balls1.GetComponent<ballCamera> ().set_flag_PermitMoving(0);
			if (pausetimer_permission == 1&&chutorialFinalStep == 0) {
				pausetimer++;
				Debug.Log ("BALL2pausetimer" + pausetimer);
				if (pausetimer == 15) {
					textobj.GetComponent<CanvasRenderer> ().SetAlpha (1);
					textimage.GetComponent<CanvasRenderer> ().SetAlpha (1);
					GameobjForScript.GetComponent<textLoad> ().shokika ();
					pausetimer = 0;
					flag_senarios = 7;
					pausetimer_permission = 0;
				}
			}
			if(flag_get_scenario_end == 1&&chutorialFinalStep == 1&&pausetimer_permission == 0){
				pausetimer++;
				Debug.Log ("BALL3pausetimer" + pausetimer);
				if (pausetimer >= 15 && pausetimer < 135) {
					textobj.GetComponent<CanvasRenderer> ().SetAlpha (0);
					textimage.GetComponent<CanvasRenderer> ().SetAlpha (0);
					flag_senarios = 0;
					toSleeping ();
					GameobjForScript.GetComponent<recipebuttonscript> ().set_recipe_NotButtonworking ();
				} else if (pausetimer==135) {
					SceneManager.LoadScene ("mainstage1Wood");
					Debug.Log ("TogoFirststage");
					pausetimer = 0;
					Firstball = 0;
				}
			}
		}

	}

	public void Strokebutton_Click(){
		if (clicke_stroke == 0) {
			GameobjForScript.GetComponent<recipebuttonscript> ().SetKakeraToRecipe (3);
			grass_anime = 1;
			clicke_stroke = 1;
			obj_kakeraGreen.transform.position = new Vector3 (Nballposition.x, Nballposition.y, Nballposition.z);

		}
	}

	public void Presentbutton_Click(){
		if (clicke_present == 0) {
			GameobjForScript.GetComponent<recipebuttonscript> ().SetMinusKakeraToRecipeForPresentForchutorial (3);
			rabbit_anime = 1;
			GameobjForScript.GetComponent<recipebuttonscript> ().SetKakeraToRecipe (1);
			obj_kakeraRed.transform.position = new Vector3 (Nballposition.x, Nballposition.y, Nballposition.z);

		}
	}

	public void setter_Kcolorflag(int Kcolorflags){
		Kcolorflag = Kcolorflags;
	}
	public int getter_Kcolorflag(){
		return Kcolorflag;
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