using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class chutorial : MonoBehaviour {
	Animator animator;
	public AudioClip SE;
	public GameObject balls1,senario_opening,textimage,textobj,recipebook,Panelobj;
	Renderer bookren;

	//以下画面フェード用変数
	public bool enableFade = true;
	public bool enableFadeIn = true;
	public bool enableFadeOut = true;
	public bool enableFadeOn = true;
	public float speed = 0.01f;
	public Image FadeImage;
	private float count = 1f;
	private bool enableAlphaTop = true;

	//タイマーのかわり
	//int timecounter;

	int fadeoutStarts;
	int flag_get_scenario_end;
	int chutorialFirstStep;

	int pausetimer,pausetimer2,pausetimer3;//一時停止用タイマー
	Vector3 Nballposition;//ボールの現在地取得用

	//ここまで
	Image image;
	int flag_fadeon;


	int flag_senarios;

	void Start()
	{

		enableFade = true;
		enableFadeIn = true;
		setAlpha (FadeImage, count);

		animator = GetComponent<Animator> ();


		Debug.Log("OpenMode");
		flag_fadeon = 1;
		//timecounter = 0;
		fadeoutStarts = 0;
		flag_senarios = 1;
		flag_get_scenario_end = 1;
		pausetimer = 0;
		pausetimer2 = 0;
		pausetimer3 = 0;
		bookren = recipebook.GetComponent<Renderer> ();
		chutorialFirstStep = 1;


	}

	void Update()
	{
		//Wakeupを自動で始めに行う
		//DelayMethod(60);
		if (flag_fadeon == 1) {
			toWakeUp ();
			bookren.enabled = false;
		}
		if (flag_senarios == 1) {
			//シナリオスクリプト　openingのセッティング
			senario_opening.GetComponent<textLoad> ().Readings ("textopeningchutorial",10);
			senario_opening.GetComponent<textLoad> ().SetNextLine ();
			flag_senarios = 0;
		} else if (flag_senarios == 2) {
			senario_opening.GetComponent<textLoad> ().Readings ("textgetrecipe",3);
			senario_opening.GetComponent<textLoad> ().SetNextLine ();
			flag_senarios = 0;
		}
		//シナリオスクリプト　openingのかきこみ
		senario_opening.GetComponent<textLoad> ().WriteLine ();
		//シナリオスクリプト　openingがおわったらチュートリアル開始
		flag_get_scenario_end = senario_opening.GetComponent<textLoad> ().get_scenario_end ();
		Debug.Log ("flag_get_scenario_end" + flag_get_scenario_end);
		if (flag_get_scenario_end == 1 && chutorialFirstStep == 1) {
			pausetimer++;
			if (pausetimer == 30) {
				balls1.GetComponent<ballCamera> ().set_flag_PermitMoving(1);
				Debug.Log ("pausetimer" + pausetimer);
				textobj.GetComponent<CanvasRenderer> ().SetAlpha (0);
				textimage.GetComponent<CanvasRenderer> ().SetAlpha (0);
				senario_opening.GetComponent<textLoad> ().shokika();
				pausetimer = 0;
				chutorialFirstStep = 0;
			}
		}
		//ここまでシナリオ
		//ここからボールの制御
		Nballposition = balls1.GetComponent<ballCamera> ().get_ballPosition();
		Debug.Log ("Nballposition" + Nballposition.x);
		if (Nballposition.z >= -27 && Nballposition.z <= -19 && Nballposition.x <= 5 && Nballposition.x >= -5) {

			balls1.GetComponent<ballCamera> ().set_flag_PermitMoving(0);

			pausetimer2++;
		}
		if (pausetimer2 == 21) {

			Panelobj.GetComponent<CanvasRenderer> ().SetAlpha (0.5f);
			textobj.GetComponent<CanvasRenderer> ().SetAlpha (1);
			textimage.GetComponent<CanvasRenderer> ().SetAlpha (1);
			flag_senarios = 2;
			bookren.enabled = true;
			//レシピを手に入れた　シナリオ
		} 
		//最後の説明が終わったら
		if (flag_get_scenario_end == 1&&pausetimer2 >= 30) {
			pausetimer3++;
			if (pausetimer3 > 30&&pausetimer3 < 130) {
				Panelobj.GetComponent<CanvasRenderer> ().SetAlpha (0);
				textobj.GetComponent<CanvasRenderer> ().SetAlpha (0);
				textimage.GetComponent<CanvasRenderer> ().SetAlpha (0);
				toSleeping ();
				Debug.Log ("S3s"+pausetimer3);
				Debug.Log ("Sflag_get_scenario_ends"+flag_get_scenario_end);
			}
			if (pausetimer3 == 130) {
				SceneManager.LoadScene ("chutorial2");
			}
		}

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