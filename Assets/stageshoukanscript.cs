using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class stageshoukanscript : MonoBehaviour {
	Animator animator;
	public AudioClip SE;
	public GameObject balls1,textimage,textobj;//,recipebook,Panelobj;
	Renderer bookren;
	public GameObject mahoujin, gameobjempty;


	//以下画面フェード用変数
	public bool enableFade = true;
	public bool enableFadeIn = true;
	public bool enableFadeOut = true;
	public bool enableFadeOn = true;
	public float speed = 0.1f;
	public Image FadeImage;
	private float count = 1f;
	private bool enableAlphaTop = true;


	public GameObject MusicScore;

	float Ms_x;//魔法陣の中心位置x
	float Ms_y;//魔法陣の中心位置y
	float Ms_z;//魔法陣の中心位置z

	//ボタン等UI関連
	public GameObject button_shoukan, button_shoukan2, button_shoukan3, button_shoukan4, button_shoukan5, button_shoukan6, button_shoukan7, button_shoukan8;//ボタン
	private Button Button_shoukan,Button_shoukan2,Button_shoukan3,Button_shoukan4,Button_shoukan5,Button_shoukan6,Button_shoukan7,Button_shoukan8;

	Animator animator_MusicScore;

	int fadeoutStarts;
	int flag_get_scenario_end;
	int chutorialFirstStep;

	int pausetimer,pausetimer2,pausetimer3;//一時停止用タイマー
	Vector3 Nballposition;//ボールの現在地取得用
	string[] scenarios;

	//ここまで
	Image image;
	int flag_fadeon;

	int flag_buttonshoukan, checker;

	int flag_senarios;
	int ShoukanFirstScenario, ShoukanSecondScenario, ShoukanThirdScenario;
	int exist_recipeTG;
	int counter_scenariogetScore, counter_scenarioNotgetScore;

	void Start()
	{

		enableFade = true;
		enableFadeIn = true;
		setAlpha (FadeImage, count);

		animator_MusicScore = MusicScore.GetComponent<Animator> ();

		Ms_x = mahoujin.transform.position.x;
		Ms_y = mahoujin.transform.position.y;
		Ms_z = mahoujin.transform.position.z;

		Debug.Log("OpenMode");
		flag_fadeon = 1;
		//timecounter = 0;
		fadeoutStarts = 0;

		textobj.GetComponent<CanvasRenderer> ().SetAlpha (0);
		textimage.GetComponent<CanvasRenderer> ().SetAlpha (0);
		balls1.GetComponent<ballCamera1> ().set_flag_PermitMoving(1);

		gameobjempty.GetComponent<textLoad1> ().shokika();

		Button_shoukan = button_shoukan.GetComponent<Button> ();//楽譜奉納ボタン
		Button_shoukan2 = button_shoukan2.GetComponent<Button> ();//楽譜奉納ボタン
		Button_shoukan3 = button_shoukan3.GetComponent<Button> ();//楽譜奉納ボタン
		Button_shoukan4 = button_shoukan4.GetComponent<Button> ();//楽譜奉納ボタン
		Button_shoukan5 = button_shoukan5.GetComponent<Button> ();//楽譜奉納ボタン
		Button_shoukan6 = button_shoukan6.GetComponent<Button> ();//楽譜奉納ボタン
		Button_shoukan7 = button_shoukan7.GetComponent<Button> ();//楽譜奉納ボタン
		Button_shoukan8 = button_shoukan8.GetComponent<Button> ();//楽譜奉納ボタン

		Button_shoukan.gameObject.SetActive (false);
		Button_shoukan2.gameObject.SetActive (false);
		Button_shoukan3.gameObject.SetActive (false);
		Button_shoukan4.gameObject.SetActive (false);
		Button_shoukan5.gameObject.SetActive (false);
		Button_shoukan6.gameObject.SetActive (false);
		Button_shoukan7.gameObject.SetActive (false);
		Button_shoukan8.gameObject.SetActive (false);

		MusicScore.gameObject.SetActive (false);
		//bookren = recipebook.GetComponent<Renderer> ();
		//balls1.GetComponent<ballCamera1> ().set_flag_PermitMoving(1);

		checker = 0;

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
			gameobjempty.GetComponent<textLoad1> ().Readings ("textForgetgakuhu", 3);
			gameobjempty.GetComponent<textLoad1> ().SetNextLine ();
			flag_senarios = 0;
			ShoukanFirstScenario = 1;
		} else if (flag_senarios == 2) {//レシピを開く
			gameobjempty.GetComponent<textLoad1> ().Readings ("textFornotgetgakuhu", 3);
			gameobjempty.GetComponent<textLoad1> ().SetNextLine ();
			flag_senarios = 0;
			ShoukanSecondScenario = 1;
		} else if (flag_senarios == 3) {//レシピを開く
			gameobjempty.GetComponent<textLoad1> ().Readings ("textenoughgakuhu", 3);
			gameobjempty.GetComponent<textLoad1> ().SetNextLine ();
			flag_senarios = 0;
			ShoukanThirdScenario = 1;
		} 

		gameobjempty.GetComponent<textLoad1> ().WriteLine ();
		flag_get_scenario_end = gameobjempty.GetComponent<textLoad1> ().get_scenario_end ();

		Nballposition = balls1.GetComponent<ballCamera1> ().get_ballPosition();
		balls1.GetComponent<ballCamera1> ().set_flag_PermitMoving(1);//常にボールを動かす

		//魔法陣の前に来たら
		if (435 > Nballposition.x && 420 < Nballposition.x && 198 < Nballposition.z && 215 > Nballposition.z && 10 < Nballposition.y && 33 > Nballposition.y) {
			

			
			if (flag_buttonshoukan == 0 &&checker == 0) {//楽譜召喚ボタンを表示
				Button_shoukan.gameObject.SetActive (true);
				Button_shoukan2.gameObject.SetActive (true);
				Button_shoukan3.gameObject.SetActive (true);
				Button_shoukan4.gameObject.SetActive (true);
				Button_shoukan5.gameObject.SetActive (true);
				Button_shoukan6.gameObject.SetActive (true);
				Button_shoukan7.gameObject.SetActive (true);
				Button_shoukan8.gameObject.SetActive (true);


			}
			if (flag_buttonshoukan == 1) {//楽譜召喚ボタンがおされたら

				//楽譜召喚ボタンをけす
				Button_shoukan.gameObject.SetActive (false);
				Button_shoukan2.gameObject.SetActive (false);
				Button_shoukan3.gameObject.SetActive (false);
				Button_shoukan4.gameObject.SetActive (false);
				Button_shoukan5.gameObject.SetActive (false);
				Button_shoukan6.gameObject.SetActive (false);
				Button_shoukan7.gameObject.SetActive (false);
				Button_shoukan8.gameObject.SetActive (false);

				animator_MusicScore.Play ("getMS");
				textobj.GetComponent<CanvasRenderer> ().SetAlpha (1);
				textimage.GetComponent<CanvasRenderer> ().SetAlpha (1);

				if (flag_get_scenario_end == 1) {//シナリオがおわったら

					counter_scenariogetScore++;//カウンター発動
					if (counter_scenariogetScore > 30) {

						Button_shoukan.gameObject.SetActive (true);
						Button_shoukan2.gameObject.SetActive (true);
						Button_shoukan3.gameObject.SetActive (true);
						Button_shoukan4.gameObject.SetActive (true);
						Button_shoukan5.gameObject.SetActive (true);
						Button_shoukan6.gameObject.SetActive (true);
						Button_shoukan7.gameObject.SetActive (true);
						Button_shoukan8.gameObject.SetActive (true);

						checker = 0;
						flag_buttonshoukan = 0;

						textobj.GetComponent<CanvasRenderer> ().SetAlpha (0);
						textimage.GetComponent<CanvasRenderer> ().SetAlpha (0);
						MusicScore.gameObject.SetActive (false);//楽譜をけす
						flag_get_scenario_end = 0;
						gameobjempty.GetComponent<textLoad1> ().shokika ();
					}
				}

				//シナリオセッティング


				//scenarios = gameobjempty.GetComponent<recipebuttonscript> ().Get_RecipeInformation ();//レシピの内容をゲット
				//Button_shoukan.gameObject.SetActive (false);//楽譜召喚ボタンをけす
				//checker = 0;

				/*
				if (exist_recipeTG >= 1) {//集まっている場合
					MusicScore.gameObject.SetActive (true);
					animator_MusicScore.Play ("getMS");
					textobj.GetComponent<CanvasRenderer> ().SetAlpha (1);
					textimage.GetComponent<CanvasRenderer> ().SetAlpha (1);
					flag_senarios = 1;
					//テキストを出す
					if (ShoukanFirstScenario == 1 && flag_get_scenario_end == 1) {
						counter_scenariogetScore++;
						if (counter_scenariogetScore == 15) {
							//アニメーションをけす
							MusicScore.gameObject.SetActive (false);
							//テキストをけす

							textobj.GetComponent<CanvasRenderer> ().SetAlpha (0);
							textimage.GetComponent<CanvasRenderer> ().SetAlpha (0);
							flag_get_scenario_end = 0;
							gameobjempty.GetComponent<textLoad1> ().shokika ();
							//checker を1 にする
							checker = 1;
						}

					}
				} else {//集まっていない場合

					textobj.GetComponent<CanvasRenderer> ().SetAlpha (1);
					textimage.GetComponent<CanvasRenderer> ().SetAlpha (1);
					flag_senarios = 2;

					if (ShoukanSecondScenario == 1&& flag_get_scenario_end == 1) {
						counter_scenarioNotgetScore++;
						if (counter_scenarioNotgetScore == 15) {
							textobj.GetComponent<CanvasRenderer> ().SetAlpha (0);
							textimage.GetComponent<CanvasRenderer> ().SetAlpha (0);
							flag_get_scenario_end = 0;
							gameobjempty.GetComponent<textLoad1> ().shokika ();

							checker = 1;
						}
					}

				}*/
			}
		}
		exist_recipeTG = 0;
	}

	public void buttonShoukan_Click(){
		flag_buttonshoukan = 1;

		checker = 1;
			if (gameobjempty.GetComponent<recipebuttonscript> ().GetRecipeIFActive (1) == 1) {//レシピのフラグ（1レシピだけがある　２楽譜がある状態）
				Debug.Log("RecipeOK"+1);
				//レシピがある&楽譜がない場合
				int CKF = gameobjempty.GetComponent<recipebuttonscript> ().GetRecipeCheckFull (1);//レシピに必要なかけらが集まっているかをチェック（１OK０NG)
				Debug.Log("RecipeOK"+1+"CKF"+CKF);
				if (CKF == 1) {//集まっている場合
					gameobjempty.GetComponent<recipebuttonscript> ().SetGakuhuToRecipe (1);//レシピから必要数をぬき、フラグを２（楽譜もち）にする
					exist_recipeTG += 1;
					Debug.Log("exist_recipe"+1);
					MusicScore.gameObject.SetActive (true);//アニメーション　楽譜を出す

				gameobjempty.GetComponent<textLoad1> ().shokika ();
				flag_senarios = 1;
			}else{
				//レシピかざいりょうがたりないよ

				gameobjempty.GetComponent<textLoad1> ().shokika ();
				flag_senarios = 2;
			}
		}else if(gameobjempty.GetComponent<recipebuttonscript> ().GetRecipeIFActive (1) == 2){
			//がくふがすでにあるよ

			gameobjempty.GetComponent<textLoad1> ().shokika ();
			flag_senarios = 3;
		}else{
			//レシピかざいりょうがたりないよ

			gameobjempty.GetComponent<textLoad1> ().shokika ();
			flag_senarios = 2;
		}
	}
	public void buttonShoukan_Click2(){
		flag_buttonshoukan = 1;
		checker = 1;

		if (gameobjempty.GetComponent<recipebuttonscript> ().GetRecipeIFActive (2) == 1) {//レシピのフラグ（1レシピだけがある　２楽譜がある状態）
			Debug.Log("RecipeOK"+2);
			//レシピがある&楽譜がない場合
			int CKF = gameobjempty.GetComponent<recipebuttonscript> ().GetRecipeCheckFull (2);//レシピに必要なかけらが集まっているかをチェック（１OK０NG)
			Debug.Log("RecipeOK"+2+"CKF"+CKF);
			if (CKF == 1) {//集まっている場合
				gameobjempty.GetComponent<recipebuttonscript> ().SetGakuhuToRecipe (2);//レシピから必要数をぬき、フラグを２（楽譜もち）にする
				exist_recipeTG += 1;
				Debug.Log("exist_recipe"+2);
				MusicScore.gameObject.SetActive (true);//アニメーション　楽譜を出す

				gameobjempty.GetComponent<textLoad1> ().shokika ();
				flag_senarios = 1;
			}else{
				//レシピかざいりょうがたりないよ

				gameobjempty.GetComponent<textLoad1> ().shokika ();
				flag_senarios = 2;
			}
		}else if(gameobjempty.GetComponent<recipebuttonscript> ().GetRecipeIFActive (1) == 2){
			//がくふがすでにあるよ

			gameobjempty.GetComponent<textLoad1> ().shokika ();
			flag_senarios = 3;
		}else{
			//レシピかざいりょうがたりないよ

			gameobjempty.GetComponent<textLoad1> ().shokika ();
			flag_senarios = 2;
		}
	}public void buttonShoukan_Click3(){

		gameobjempty.GetComponent<textLoad1> ().shokika ();
		flag_buttonshoukan = 1;

		checker = 1;
		if (gameobjempty.GetComponent<recipebuttonscript> ().GetRecipeIFActive (3) == 1) {//レシピのフラグ（1レシピだけがある　２楽譜がある状態）
			Debug.Log("RecipeOK"+3);
			//レシピがある&楽譜がない場合
			int CKF = gameobjempty.GetComponent<recipebuttonscript> ().GetRecipeCheckFull (3);//レシピに必要なかけらが集まっているかをチェック（１OK０NG)
			Debug.Log("RecipeOK"+3+"CKF"+CKF);
			if (CKF == 1) {//集まっている場合
				gameobjempty.GetComponent<recipebuttonscript> ().SetGakuhuToRecipe (3);//レシピから必要数をぬき、フラグを２（楽譜もち）にする
				exist_recipeTG += 1;
				Debug.Log("exist_recipe"+3);
				MusicScore.gameObject.SetActive (true);//アニメーション　楽譜を出す
				flag_senarios = 1;
			}else{
				//レシピかざいりょうがたりないよ
				flag_senarios = 2;
			}
		}else if(gameobjempty.GetComponent<recipebuttonscript> ().GetRecipeIFActive (1) == 2){
			//がくふがすでにあるよ
			flag_senarios = 3;
		}else{
			//レシピかざいりょうがたりないよ
			flag_senarios = 2;
		}
	}public void buttonShoukan_Click4(){
		flag_buttonshoukan = 1;
		checker = 1;
		gameobjempty.GetComponent<textLoad1> ().shokika ();

		flag_buttonshoukan = 1;
		if (gameobjempty.GetComponent<recipebuttonscript> ().GetRecipeIFActive (4) == 1) {//レシピのフラグ（1レシピだけがある　２楽譜がある状態）
			Debug.Log("RecipeOK"+4);
			//レシピがある&楽譜がない場合
			int CKF = gameobjempty.GetComponent<recipebuttonscript> ().GetRecipeCheckFull (4);//レシピに必要なかけらが集まっているかをチェック（１OK０NG)
			Debug.Log("RecipeOK"+4+"CKF"+CKF);
			if (CKF == 1) {//集まっている場合
				gameobjempty.GetComponent<recipebuttonscript> ().SetGakuhuToRecipe (4);//レシピから必要数をぬき、フラグを２（楽譜もち）にする
				exist_recipeTG += 1;
				Debug.Log("exist_recipe"+4);
				MusicScore.gameObject.SetActive (true);//アニメーション　楽譜を出す
				flag_senarios = 1;
				flag_buttonshoukan = 1;
			}else{
				//レシピかざいりょうがたりないよ
				flag_senarios = 2;
				flag_buttonshoukan = 1;
			}
		}else if(gameobjempty.GetComponent<recipebuttonscript> ().GetRecipeIFActive (1) == 2){
			//がくふがすでにあるよ
			flag_senarios = 3;
			flag_buttonshoukan = 1;
		}else{
			//レシピかざいりょうがたりないよ
			flag_senarios = 2;
			flag_buttonshoukan = 1;
		}
	}public void buttonShoukan_Click5(){
		flag_buttonshoukan = 5;
		checker = 1;
		gameobjempty.GetComponent<textLoad1> ().shokika ();

		flag_buttonshoukan = 1;
		if (gameobjempty.GetComponent<recipebuttonscript> ().GetRecipeIFActive (5) == 1) {//レシピのフラグ（1レシピだけがある　２楽譜がある状態）
			Debug.Log("RecipeOK"+5);
			//レシピがある&楽譜がない場合
			int CKF = gameobjempty.GetComponent<recipebuttonscript> ().GetRecipeCheckFull (5);//レシピに必要なかけらが集まっているかをチェック（１OK０NG)
			Debug.Log("RecipeOK"+5+"CKF"+CKF);
			if (CKF == 1) {//集まっている場合
				gameobjempty.GetComponent<recipebuttonscript> ().SetGakuhuToRecipe (5);//レシピから必要数をぬき、フラグを２（楽譜もち）にする
				exist_recipeTG += 1;
				Debug.Log("exist_recipe"+5);
				MusicScore.gameObject.SetActive (true);//アニメーション　楽譜を出す
				flag_senarios = 1;
			}else{
				//レシピかざいりょうがたりないよ
				flag_senarios = 2;
			}
		}else if(gameobjempty.GetComponent<recipebuttonscript> ().GetRecipeIFActive (1) == 2){
			//がくふがすでにあるよ
			flag_senarios = 3;
		}else{
			//レシピかざいりょうがたりないよ
			flag_senarios = 2;
		}
	}public void buttonShoukan_Click6(){
		flag_buttonshoukan = 1;
		checker = 1;

		gameobjempty.GetComponent<textLoad1> ().shokika ();
		if (gameobjempty.GetComponent<recipebuttonscript> ().GetRecipeIFActive (6) == 1) {//レシピのフラグ（1レシピだけがある　２楽譜がある状態）
			Debug.Log("RecipeOK"+6);
			//レシピがある&楽譜がない場合
			int CKF = gameobjempty.GetComponent<recipebuttonscript> ().GetRecipeCheckFull (6);//レシピに必要なかけらが集まっているかをチェック（１OK０NG)
			Debug.Log("RecipeOK"+6+"CKF"+CKF);
			if (CKF == 1) {//集まっている場合
				gameobjempty.GetComponent<recipebuttonscript> ().SetGakuhuToRecipe (6);//レシピから必要数をぬき、フラグを２（楽譜もち）にする
				exist_recipeTG += 1;
				Debug.Log("exist_recipe"+6);
				MusicScore.gameObject.SetActive (true);//アニメーション　楽譜を出す
				flag_senarios = 1;
			}else{
				//レシピかざいりょうがたりないよ
				flag_senarios = 2;
			}
		}else if(gameobjempty.GetComponent<recipebuttonscript> ().GetRecipeIFActive (1) == 2){
			//がくふがすでにあるよ
			flag_senarios = 3;
		}else{
			//レシピかざいりょうがたりないよ
			flag_senarios = 2;
		}
	}public void buttonShoukan_Click7(){
		flag_buttonshoukan = 1;
		checker = 1;
		gameobjempty.GetComponent<textLoad1> ().shokika ();

		if (gameobjempty.GetComponent<recipebuttonscript> ().GetRecipeIFActive (7) == 1) {//レシピのフラグ（1レシピだけがある　２楽譜がある状態）
			Debug.Log("RecipeOK"+7);
			//レシピがある&楽譜がない場合
			int CKF = gameobjempty.GetComponent<recipebuttonscript> ().GetRecipeCheckFull (7);//レシピに必要なかけらが集まっているかをチェック（１OK０NG)
			Debug.Log("RecipeOK"+7+"CKF"+CKF);
			if (CKF == 1) {//集まっている場合
				gameobjempty.GetComponent<recipebuttonscript> ().SetGakuhuToRecipe (7);//レシピから必要数をぬき、フラグを２（楽譜もち）にする
				exist_recipeTG += 1;
				Debug.Log("exist_recipe"+7);
				MusicScore.gameObject.SetActive (true);//アニメーション　楽譜を出す
				flag_senarios = 1;
			}else{
				//レシピかざいりょうがたりないよ
				flag_senarios = 2;
			}
		}else if(gameobjempty.GetComponent<recipebuttonscript> ().GetRecipeIFActive (1) == 2){
			//がくふがすでにあるよ
			flag_senarios = 3;
		}else{
			//レシピかざいりょうがたりないよ
			flag_senarios = 2;
		}
	}public void buttonShoukan_Click8(){
		flag_buttonshoukan = 1;
		checker = 1;

		gameobjempty.GetComponent<textLoad1> ().shokika ();
		if (gameobjempty.GetComponent<recipebuttonscript> ().GetRecipeIFActive (8) == 1) {//レシピのフラグ（1レシピだけがある　２楽譜がある状態）
			Debug.Log("RecipeOK"+8);
			//レシピがある&楽譜がない場合
			int CKF = gameobjempty.GetComponent<recipebuttonscript> ().GetRecipeCheckFull (8);//レシピに必要なかけらが集まっているかをチェック（１OK０NG)
			Debug.Log("RecipeOK"+8+"CKF"+CKF);
			if (CKF == 1) {//集まっている場合
				gameobjempty.GetComponent<recipebuttonscript> ().SetGakuhuToRecipe (8);//レシピから必要数をぬき、フラグを２（楽譜もち）にする
				exist_recipeTG += 1;
				Debug.Log("exist_recipe"+8);
				MusicScore.gameObject.SetActive (true);//アニメーション　楽譜を出す
				flag_senarios = 1;
			}else{
				//レシピかざいりょうがたりないよ
				flag_senarios = 2;
			}
		}else if(gameobjempty.GetComponent<recipebuttonscript> ().GetRecipeIFActive (1) == 2){
			//がくふがすでにあるよ
			flag_senarios = 3;
		}else{
			//レシピかざいりょうがたりないよ
			flag_senarios = 2;
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