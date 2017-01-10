using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Resources;

public class GoalScript : MonoBehaviour {

	public int score;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Ball"){
			score++;
			StartCoroutine(col.gameObject.GetComponent<BounceBall>().Reset());
		}
	}
}
