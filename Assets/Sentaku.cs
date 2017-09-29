using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Sentaku : MonoBehaviour {
	public AudioClip SEstart;
	GameObject balls1, gameobjempy;//sphere,gameobjEmpty
	// Use this for initialization
	void Start () {
		GetComponent<AudioSource> ().PlayOneShot (SEstart);

	}

	// Update is called once per frame
	void Update () {
		
	}

	public void SceneLoad1(){
		SceneManager.LoadScene ("opening");
	}
	public void SceneLoad3(){

		SceneManager.LoadScene ("mainstage1Wood");
	}

	public void SceneLoad10(){
		SceneManager.LoadScene ("starters");
	}

	public void InitializationOFparameter(){
		//gameobjempy.GetComponent<kakerabuttonscript>().ShokikatextToRecipe;
	}
}