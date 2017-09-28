using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text;

public class recipeSC : MonoBehaviour {

	//情報保持用変数

	//レシピの情報を格納しているテキストファイル名
	string textreader = "Recipetree";

	//レシピの情報を格納するための読み込み変数等
	TextAsset textfiles;
	string stext;
	StringReader reader;

	public string[] scenarios;

	int writingnow;

	//書き込みのためのテキストファイル等
	string textForwriting;
	string kakerasce, kakeratotal;
	int kakerasuu, kakerasuutotal;

	// Use this for initialization
	void Start () {

		writingnow = 0;
	}
	
	// Update is called once per frame
	void Update () {

	}

	//名前,赤,青,緑,トータル,赤必要数,青必要数,緑必要数,トータル必要数,フラグ,
	public void RecipeReadings(){
		if (writingnow == 0) {
			textfiles = Resources.Load (textreader) as TextAsset;
			stext = textfiles.text;
			reader = new StringReader (stext);

			for (int i = 0; i < 80; i++) {
				scenarios = stext.Split (',');
			}
		}

	}
	//レシピのテキストファイルへの書き込み
	public void RecipeWritings(){
		writingnow = 1;
		textForwriting = string.Join (",", scenarios);
		System.IO.File.WriteAllText ("C:\\Users\\student\\Desktop\\Hearters\\Assets\\Resources\\Recipetree.txt", textForwriting);
		writingnow = 0;
	}

	public void ShokikatextToRecipe(){//レシピ完全初期化 [12349]のみ
		RecipeReadings();
		for (int i = 0; i < 80; i++) {
			if ((i % 10 == 1) || (i % 10 == 2)|| (i % 10 == 3)|| (i % 10 == 4)|| (i % 10 == 9)) {
				scenarios [i] = "0";
			}
		}
		RecipeWritings ();
	}

	public void SetRecipeToActiveGoddess(){//女神のレシピゲット時　そのレシピをActiveにする
		RecipeReadings();
		scenarios [79] = "1";
		RecipeWritings ();
	}


	public int GetRecipeIFActive(int PagenumberForrecipes){//ページ番号を指定、そのページがアクティブかどうかを返す
		int s = int.Parse(scenarios [9+((PagenumberForrecipes-1) * 10)]);
		return s;
	}

	public string[] Get_RecipeInformation(){
		RecipeReadings ();
		return scenarios;
	}
}
