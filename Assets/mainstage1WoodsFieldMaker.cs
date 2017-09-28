using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class mainstage1WoodsFieldMaker : MonoBehaviour {
	Animator animator;


	public Terrain Terrain1wood;
	public GameObject treeprefab;

	public int[,] ColliderObject;

	void Start()
	{
		animator = GetComponent<Animator> ();
		ColliderObject = new int[3,2];
		animalmaker (3);

	}

	void Update()
	{
		

	}

	void setAlpha(Image image,float alpha) {
		
		image.color = new Color (image.color.r, image.color.g, image.color.b, alpha);
	}

	//必要なパラメーターS：　動物が持つ玉の色　なんの動きを何回したらいいか？　プレゼントの場合ほしいたまの色と数 動きにあわせたアニメーション
	//オブジェクトが持つべきパラメーター　：動物の番号、Sの番号、玉をもってるかもってないか（フラグ）
	public void animalmaker(int animalnumberTOTAL){
		for (int i = 0; i < animalnumberTOTAL; i++) {
			for (int j = 0; j < 2; j++) {
				if (j == 0) {
					int randoms = UnityEngine.Random.Range (0, 5);//パラメーターの種類が６こ
					ColliderObject [i, j] = randoms;
				}
				if (j == 1) {
					ColliderObject [i, j] = 1;
				}
			}
		}
	}

	//ここまでフェード
}