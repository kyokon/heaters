using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetfollow : MonoBehaviour {
	public Transform targetball;
	private Vector3 offset;
	// Use this for initialization
	void Start () {
		offset = GetComponent<Transform> ().position - targetball.position;
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Transform>().position = targetball.position + offset;
	}
}
