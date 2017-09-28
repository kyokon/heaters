using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System;
using System.Text;

public class textLoad1 : MonoBehaviour 
{
	public string[] scenarios;
	int maxlines = 2;

	[SerializeField] Text uiText;

	[SerializeField][Range(0.001f, 0.3f)]
	float intervalForCharacterDisplay = 0.1f;

	public string currentText = string.Empty;
	public float timeUntilDisplay = 0;
	public float timeElapsed = 1;
	public int currentLine = 0;
	public int lastUpdateCharacter = -1;
	int displayCharacterCount;

	public int scenario_times = 3;//行数

	string textreader = "texttest";

	public int flag_scenario_end = 0;//シナリオが終わってるかどうか？0未完、1終了

	public void shokika(){
		currentText = string.Empty;
		timeUntilDisplay = 0;
		timeElapsed = 1;
		currentLine = 0;
		lastUpdateCharacter = -1;
	}

	//テキストの終了をもとにもどす
	public void Set_textendshokika(){
		flag_scenario_end = 0;
	}
	//読み込むテキストファイルの設定
	public string Set_textreader(string textfilename){
		textreader = textfilename;
		return textreader;
	}
	//シナリオの行数設定
	public int Set_scenario_times(int scenariosuu){
		scenario_times = scenariosuu;
		return scenario_times;
	}
	//最後の行を出し終わったら1を送り返す
	public int get_scenario_end(){
		if ((scenario_times == currentLine)&&(IsCompleteDisplayText == true)) {
			flag_scenario_end = 1;
			Debug.Log ("flag_scenario_end" + flag_scenario_end);
			return flag_scenario_end;
		} else {
			flag_scenario_end = 0;
			return flag_scenario_end;
		}
	}

	// 文字の表示が完了しているかどうか
	public bool IsCompleteDisplayText 
	{
		get { return  Time.time > timeElapsed + timeUntilDisplay; }
	}

	void Start()
	{
		//Readings (textreader);
		//SetNextLine();
	}

	void Update () 
	{
		
		//WriteLine ();
	}


	public void WriteLine(){

		// 文字の表示が完了してるならクリック時に次の行を表示する
		if( IsCompleteDisplayText ){
			if((currentLine < scenarios.Length) && ((Input.GetMouseButtonDown(0))||(Input.GetKey(KeyCode.Return)))){
				SetNextLine();
			}
		}else{
			// 完了してないなら文字をすべて表示する
			if((Input.GetMouseButtonDown(0))||(Input.GetKey(KeyCode.Return))){
				timeUntilDisplay = 0;
			}
		}
		//クリックから経過した時間が想定時間の何％か調べ、表示文字数を出す
		displayCharacterCount = (int)(Mathf.Clamp01((Time.time - timeElapsed) / timeUntilDisplay) * currentText.Length);
		//表示文字数が前回の表示文字数と異なる場合、テキストを更新
		if( displayCharacterCount != lastUpdateCharacter ){
			uiText.text = currentText.Substring(0, displayCharacterCount);
			lastUpdateCharacter = displayCharacterCount;
		}
	}

	public void SetNextLine()
	{
		try{
		currentText = scenarios[currentLine];
		Debug.Log ("currentLine"+currentLine);
		}
		catch{
			Debug.Log ("ERRORcurrentLine"+currentLine);
			Debug.Log (scenarios[currentLine]);
		}
		timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
		timeElapsed = Time.time;
		currentLine ++;
		lastUpdateCharacter = -1;
	}

	public void Readings(string textreader, int RSscenario_times){
		TextAsset textfiles = Resources.Load(textreader) as TextAsset;
		string stext = textfiles.text;
		StringReader reader = new StringReader(stext);
		scenario_times = RSscenario_times;

		for(int i = 0; i<RSscenario_times; i++){
				scenarios = stext.Split(',');
		}
		for (int j = 0; j < RSscenario_times; j++) {
				Debug.Log (scenarios [j]);
		}
	}

}	