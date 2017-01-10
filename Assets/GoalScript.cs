using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour {

	GameManager gm;

	void Start () {
		gm = GameManager.instance;
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Ball"){
			if (gm != null)
				gm.GoalScored(col);
		}
	}
}
