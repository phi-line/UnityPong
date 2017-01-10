using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour {

	public Text leftText, rightText;
	private int lScore, rScore;
	public GoalScript leftGoal, rightGoal;
	public GameObject leftPaddle, rightPaddle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (leftGoal.score > lScore){
			lScore = leftGoal.score;
		}
		if (rightGoal.score < rScore){
			rScore = rightGoal.score;
		}
	}
}
