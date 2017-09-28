using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class mahoujinScript : MonoBehaviour {
	Vector3 Nballposition;//ボールの現在地取得用
	public GameObject balls1, gameobjEmpty, book;
	float Ms_x;//魔法陣の中心位置x
	float Ms_y;//魔法陣の中心位置y
	float Ms_z;//魔法陣の中心位置z
	public int Mahoujin_whattodo;


	public int flag_uponMahoujin;

	//以下フェード用変数
	public bool enableFade = true;
	public bool enableFadeIn = true;
	public bool enableFadeOut = true;
	public bool enableFadeOn = true;
	public float speed = 0.01f;
	public Image FadeImage;
	private float count = 0;
	private bool enableAlphaTop = true;

	int flag_fadeon;
	//ここまで

	Renderer bookren;
	int[] mahoujinSC;
	//0:シーン1 1:シーン召喚場所 2:楽譜シーン 3:本2 4:本3 5:本4 6:本5 7:本6 8:本7 9:本8 10:楽譜召喚


	public Transform cameratrans;
	string scenename;
	public GameObject textimage,textobj,Panelobj;

	// Use this for initialization
	void Start () {

		Panelobj.GetComponent<CanvasRenderer> ().SetAlpha (0);
		textobj.GetComponent<CanvasRenderer> ().SetAlpha (0);
		textimage.GetComponent<CanvasRenderer> ().SetAlpha (0);

		Ms_x = this.transform.position.x;
		Ms_y = this.transform.position.y;
		Ms_z = this.transform.position.z;
		mahoujinSC = new int[8];
		mahoujinSC [Mahoujin_whattodo - 1] = 1;
		bookren = book.GetComponent<Renderer> ();

		bookren.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		Nballposition = balls1.GetComponent<ballCamera1> ().get_ballPosition();
		mahoujin_allow ();
	}

	public void mahoujin_allow(){
		if ((Ms_x + 6) > Nballposition.x && (Ms_x - 6) < Nballposition.x && (Ms_z - 6) < Nballposition.z && (Ms_z + 6) > Nballposition.z && (Ms_y - 6) < Nballposition.y && (Ms_y + 6) > Nballposition.y) {
			Debug.Log ("Upon Mahouijn");
			balls1.GetComponent<ballCamera1> ().set_flag_PermitMoving(0);
			if (flag_uponMahoujin == 5) {
				switch (Mahoujin_whattodo) {
				case 1:
					if (mahoujinSC [Mahoujin_whattodo - 1] == 1) {
						scenename = "mainstage1Wood";

						flag_fadeon = 2;
					}
					break;
				case 2:
					if (mahoujinSC [Mahoujin_whattodo - 1] == 1) {
						scenename = "mainstageshoukan";

						flag_fadeon = 2;
					}
					break;
				case 3:
					if (mahoujinSC [Mahoujin_whattodo - 1] == 1) {
						scenename = "stageMScore";

						flag_fadeon = 2;
					}
					break;
				case 4:
					if (mahoujinSC [Mahoujin_whattodo - 1] == 1) {
						gameobjEmpty.GetComponent<recipebuttonscript> ().SetRecipeToActive (2);
						Debug.Log("mahoujinSet4");
						Panelobj.GetComponent<CanvasRenderer> ().SetAlpha (1);
						textobj.GetComponent<CanvasRenderer> ().SetAlpha (1);
						textimage.GetComponent<CanvasRenderer> ().SetAlpha (1);
						book.transform.position = new Vector3 (Nballposition.x, Nballposition.y, Nballposition.z);
						Vector3 bookposition = book.transform.position;
						bookposition += cameratrans.transform.TransformDirection (Vector3.forward) * 3;
						book.transform.position = bookposition;
						Debug.Log("BookPosition"+book.transform.position);
						mahoujinSC [Mahoujin_whattodo - 1] = 0;

						textobj.GetComponent<Text>().text = "レシピ2 をてにいれたよ";
						bookren.enabled = true;
					}
					break;
				case 5:
					if (mahoujinSC [Mahoujin_whattodo - 1] == 1) {
						gameobjEmpty.GetComponent<recipebuttonscript> ().SetRecipeToActive (3);
						Debug.Log("mahoujinSet5");

						Panelobj.GetComponent<CanvasRenderer> ().SetAlpha (1);
						textobj.GetComponent<CanvasRenderer> ().SetAlpha (1);
						textimage.GetComponent<CanvasRenderer> ().SetAlpha (1);

						book.transform.position = new Vector3 (Nballposition.x, Nballposition.y, Nballposition.z);
						Vector3 bookposition = book.transform.position;
						bookposition += cameratrans.transform.TransformDirection (Vector3.forward) * 3;
						book.transform.position = bookposition;
						mahoujinSC [Mahoujin_whattodo - 1] = 0;
						textobj.GetComponent<Text>().text = "レシピ3 をてにいれたよ";

						bookren.enabled = true;
					}
					break;
				case 6:
					if (mahoujinSC [Mahoujin_whattodo - 1] == 1) {
						Debug.Log("mahoujinSet6");
						Panelobj.GetComponent<CanvasRenderer> ().SetAlpha (1);
						textobj.GetComponent<CanvasRenderer> ().SetAlpha (1);
						textimage.GetComponent<CanvasRenderer> ().SetAlpha (1);
						gameobjEmpty.GetComponent<recipebuttonscript> ().SetRecipeToActive (4);
						book.transform.position = new Vector3 (Nballposition.x, Nballposition.y, Nballposition.z);
						Vector3 bookposition = book.transform.position;
						bookposition += cameratrans.transform.TransformDirection (Vector3.forward) * 3;
						book.transform.position = bookposition;
						mahoujinSC [Mahoujin_whattodo - 1] = 0;

						textobj.GetComponent<Text>().text  = "レシピ4 をてにいれたよ";
						bookren.enabled = true;
					}
					break;
				case 7:
					if (mahoujinSC [Mahoujin_whattodo - 1] == 1) {
						Debug.Log("mahoujinSet7");
						Panelobj.GetComponent<CanvasRenderer> ().SetAlpha (1);
						textobj.GetComponent<CanvasRenderer> ().SetAlpha (1);
						textimage.GetComponent<CanvasRenderer> ().SetAlpha (1);
						gameobjEmpty.GetComponent<recipebuttonscript> ().SetRecipeToActive (5);
						book.transform.position = new Vector3 (Nballposition.x, Nballposition.y, Nballposition.z);
						Vector3 bookposition = book.transform.position;
						bookposition += cameratrans.transform.TransformDirection (Vector3.forward) * 3;
						book.transform.position = bookposition;
						mahoujinSC [Mahoujin_whattodo - 1] = 0;

						textobj.GetComponent<Text>().text = "レシピ5 をてにいれたよ";
						bookren.enabled = true;
					}
					break;
				case 8:
					if (mahoujinSC [Mahoujin_whattodo - 1] == 1) {
						Debug.Log("mahoujinSet8");
						Panelobj.GetComponent<CanvasRenderer> ().SetAlpha (1);
						textobj.GetComponent<CanvasRenderer> ().SetAlpha (1);
						textimage.GetComponent<CanvasRenderer> ().SetAlpha (1);
						gameobjEmpty.GetComponent<recipebuttonscript> ().SetRecipeToActive (6);
						book.transform.position = new Vector3 (Nballposition.x, Nballposition.y, Nballposition.z);
						Vector3 bookposition = book.transform.position;
						bookposition += cameratrans.transform.TransformDirection (Vector3.forward) * 3;
						book.transform.position = bookposition;
						mahoujinSC [Mahoujin_whattodo - 1] = 0;

						textobj.GetComponent<Text>().text = "レシピ6 をてにいれたよ";
						bookren.enabled = true;
					}
					break;
				case 9:
					if (mahoujinSC [Mahoujin_whattodo - 1] == 1) {
						Debug.Log("mahoujinSet9");
						Panelobj.GetComponent<CanvasRenderer> ().SetAlpha (1);
						textobj.GetComponent<CanvasRenderer> ().SetAlpha (1);
						textimage.GetComponent<CanvasRenderer> ().SetAlpha (1);
						gameobjEmpty.GetComponent<recipebuttonscript> ().SetRecipeToActive (7);
						book.transform.position = new Vector3 (Nballposition.x, Nballposition.y, Nballposition.z);
						Vector3 bookposition = book.transform.position;
						bookposition += cameratrans.transform.TransformDirection (Vector3.forward) * 3;
						book.transform.position = bookposition;
						mahoujinSC [Mahoujin_whattodo - 1] = 0;

						textobj.GetComponent<Text>().text = "レシピ7 をてにいれたよ";
						bookren.enabled = true;
					}
					break;
				case 10:
					if (mahoujinSC [Mahoujin_whattodo - 1] == 1) {
						Debug.Log("mahoujinSet10");
						Panelobj.GetComponent<CanvasRenderer> ().SetAlpha (1);
						textobj.GetComponent<CanvasRenderer> ().SetAlpha (1);
						textimage.GetComponent<CanvasRenderer> ().SetAlpha (1);
						gameobjEmpty.GetComponent<recipebuttonscript> ().SetRecipeToActive (8);
						book.transform.position = new Vector3 (Nballposition.x, Nballposition.y, Nballposition.z);
						Vector3 bookposition = book.transform.position;
						bookposition += cameratrans.transform.TransformDirection (Vector3.forward) * 3;
						book.transform.position = bookposition;
						mahoujinSC [Mahoujin_whattodo - 1] = 0;

						textobj.GetComponent<Text>().text = "レシピ8 をてにいれたよ";
						bookren.enabled = true;
					}
					break;
				case 11:
					if (mahoujinSC [Mahoujin_whattodo - 1] == 1) {
						Panelobj.GetComponent<CanvasRenderer> ().SetAlpha (1);
						textobj.GetComponent<CanvasRenderer> ().SetAlpha (1);
						textimage.GetComponent<CanvasRenderer> ().SetAlpha (1);
						gameobjEmpty.GetComponent<recipebuttonscript> ().SetRecipeToActive (1);
						book.transform.position = new Vector3 (Nballposition.x, Nballposition.y, Nballposition.z);
						Vector3 bookposition = book.transform.position;
						bookposition += cameratrans.transform.TransformDirection (Vector3.forward) * 3;
						book.transform.position = bookposition;
						mahoujinSC [Mahoujin_whattodo - 1] = 0;

						textobj.GetComponent<Text>().text = "楽譜 をてにいれたよ";
						bookren.enabled = true;
					}
					break;
				}
				//シーン移動ならおやすみなさい
			}
			if (flag_uponMahoujin > 5 && flag_uponMahoujin <= 75) {
				if (flag_fadeon == 2) {
					toSleeping ();
				} else {


				}
			}
			if (flag_uponMahoujin > 75 && flag_uponMahoujin < 135) {
				if (flag_fadeon == 2) {
					toSleeping ();
				} else {

					Panelobj.GetComponent<CanvasRenderer> ().SetAlpha (0);
					textobj.GetComponent<CanvasRenderer> ().SetAlpha (0);
					textimage.GetComponent<CanvasRenderer> ().SetAlpha (0);
					balls1.GetComponent<ballCamera1> ().set_flag_PermitMoving(1);
					bookren.enabled = false;
				}
			}
			if (flag_uponMahoujin == 125 && flag_fadeon == 2) {//シーン移動
				Debug.Log("scene change");
				if(scenename != null){
				SceneManager.LoadScene (scenename);
				}
				scenename = null;
				flag_fadeon = 0;
				balls1.GetComponent<ballCamera1> ().set_flag_PermitMoving(1);
			}

			flag_uponMahoujin++;
			Debug.Log ("flag_uponMahoujin"+flag_uponMahoujin);
		} else {
			flag_uponMahoujin = 0;
		}
	}

	public void set_MahoujinOnOff(int mahoujin_number, int workflag){//魔法陣の起動終了を制御　１なら起動
		if (Mahoujin_whattodo == mahoujin_number) {
			mahoujinSC [mahoujin_number - 1] = workflag;
		}
	}
	public int get_MahoujinOnOff(int mahoujin_number){
		int flag_working = mahoujinSC [mahoujin_number - 1];
		return flag_working;
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
}
