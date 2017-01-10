using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class GameManager : MonoBehaviour {
	public static GameManager instance = null;

	public Text leftText, rightText;
	public GameObject leftPaddle, rightPaddle;
	public GameOverFade gameOverFade;
	public GameObject ps;
	private GameObject ips;

	public int maxScore = 7;
	private int lScore, rScore;
	private string winMessage = " side wins!";

	private bool gameOver;

	void Awake() {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}

	// Update is called once per frame
	void Update () {
		if (gameOver && Input.GetKeyDown(KeyCode.Space)){
			gameOver = false;
			gameOverFade.ResetDefaults();
			gameOverFade.gameObject.SetActive(false);
			ResetGame();
		}
		if (ips) {
			if(!ips.GetComponent<ParticleSystem>().IsAlive())
	         {
	             Destroy(ips);
	         }
	    }
	}

	public void GoalScored(Collision col){
		if (col.transform.position.x > 0){
			ips = (GameObject) Instantiate(ps, col.transform.position, Quaternion.Euler(Vector3.up));
			lScore++;
			leftText.text = lScore.ToString();
			if (lScore >= maxScore){
				Debug.Log("Left win!"); 
				TriggerWin("Left");
				StartCoroutine(col.gameObject.GetComponent<BounceBall>().ResetBall(Vector3.zero));
				return;
			}
			StartCoroutine(col.gameObject.GetComponent<BounceBall>().ResetBall(Vector3.left));
		}
		else {
			ips = (GameObject) Instantiate(ps, col.transform.position, Quaternion.Euler(Vector3.up));
			rScore++;
			rightText.text = rScore.ToString();
			if (rScore >= maxScore){
				Debug.Log("Right win!"); 
				TriggerWin("Right");
				StartCoroutine(col.gameObject.GetComponent<BounceBall>().ResetBall(Vector3.zero));
				return;
			}
			StartCoroutine(col.gameObject.GetComponent<BounceBall>().ResetBall(Vector3.right));
		}
	}

	private void TriggerWin(string winner){
		winner += winMessage;
		gameOver = true;
		gameOverFade.gameObject.SetActive(true);
		StartCoroutine(gameOverFade.Blur(winner));
	}

	private void ResetGame(){
		lScore = rScore = 0;
		leftText.text = rightText.text = 0.ToString();
		if (UnityEngine.Random.insideUnitCircle.x < 0)
			StartCoroutine(FindObjectOfType<BounceBall>().ResetBall(Vector3.left));
		else
			StartCoroutine(FindObjectOfType<BounceBall>().ResetBall(Vector3.right));
	}
}
