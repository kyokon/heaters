using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class opening2 : MonoBehaviour {
	Animator animator;
	public AudioClip SE;
	public GameObject balls1,senario_opening;

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
	int timecounter;

	int fadeoutStarts;
	int flag_get_scenario_end;


	//ここまで
	Image image;
	int flag_fadeon;


	int flag_senarios;

	private int pausetimer;

	void Start()
	{

		enableFade = true;
		enableFadeIn = true;
		setAlpha (FadeImage, count);

		animator = GetComponent<Animator> ();

		Debug.Log("OpenMode");
		flag_fadeon = 1;
		timecounter = 0;
		fadeoutStarts = 0;
		flag_senarios = 1;
		flag_get_scenario_end = 1;
		balls1.GetComponent<ballCamera> ().set_flag_PermitMoving(0);
		senario_opening.GetComponent<textLoad> ().Set_textendshokika ();
		senario_opening.GetComponent<textLoad> ().shokika ();
		pausetimer = 0;

	}

	void Update()
	{
		//Wakeupを自動で始めに行う
		if (flag_fadeon == 1) {
			toWakeUp ();
		}
		if(flag_senarios == 1){
			//シナリオスクリプト　openingのセッティング
			senario_opening.GetComponent<textLoad> ().Readings ("textopening2",8);
			senario_opening.GetComponent<textLoad> ().SetNextLine();
			flag_senarios = 0;
		}
		//シナリオスクリプト　openingのかきこみ
		senario_opening.GetComponent<textLoad> ().WriteLine ();
		//シナリオスクリプト　openingがおわったかどうかのチェック、もしおわったらフェードアウト
		flag_get_scenario_end = senario_opening.GetComponent<textLoad> ().get_scenario_end ();
		if (flag_get_scenario_end == 1) {

			pausetimer++;
			Debug.Log ("pausetimer" + pausetimer);
			if (pausetimer >= 100) {
				toSleeping ();
				if (pausetimer == 220) {
					SceneManager.LoadScene ("chutorial");
				}
			}
		}
		//ここまで

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