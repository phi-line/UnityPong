using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour {

	public int score;
	GameManager gm;

	void Awake () {
		gm = GameManager.instance;
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Ball"){
			score++;
			gm.GoalScored(col, score);
		}
	}
}
