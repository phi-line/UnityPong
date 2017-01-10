using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public static GameManager instance = null;

	public Text leftText, rightText;
	public GameObject leftPaddle, rightPaddle;

	public int lScore, rScore;

	void Awake() {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}

	// Update is called once per frame
	void Update () {

	}

	public void GoalScored(Collision col, int score){
		if (col.transform.position.x > 0){
			Debug.Log("Left goal scored upon!");
			lScore = score;
			leftText.text = lScore.ToString();
			StartCoroutine(col.gameObject.GetComponent<BounceBall>().Reset(Vector3.left));
		}
		else {
			Debug.Log("Right goal scored upon!");
			rScore = score;
			rightText.text = rScore.ToString();
			StartCoroutine(col.gameObject.GetComponent<BounceBall>().Reset(Vector3.right));
		}
	}
}
