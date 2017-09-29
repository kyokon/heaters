using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ballCameraForblack : MonoBehaviour {

	public GameObject target;
	private NavMeshAgent agent;

	Vector3 Positionball;
	int kouhan;

	Renderer ballblack;
	public ParticleSystem pObject;

	void Start () {

		//static変数 kouhanの呼び出し、1をセット
		if (statickouhansen.get_kouhan () == 0) {

			ballblack = this.GetComponent<Renderer> ();
			pObject.gameObject.SetActive(false);
			ballblack.enabled = false;
		}

		agent = GetComponent<NavMeshAgent> ();
	}

	void Update () {
		Positionball = this.transform.position;
		kouhan = statickouhansen.get_kouhan();
		// ターゲットの位置を目的地に設定する。
		if (kouhan == 1) {
			agent.destination = target.transform.position;
		}
	}

	public Vector3 get_Positionball(){
		return Positionball;
	}
}