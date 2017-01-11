using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;

public class PaddleAI : MonoBehaviour {

	public bool useAI = true;
	public GameObject ball;
	public Text playerTag;
	public float sightDistance = 5f;
	public int numReflections = 2;
	public float snapMovement = 0.1f;
	public Color rayColorA, rayColorB;

	Vector3 ballDir, target;
	String AI = "AI";
	String player = "HMN";

	void Start () {
		if (useAI)
			playerTag.text = AI;
		else playerTag.text = player;
	}

	void Update () {
		if (useAI && this.GetComponent<MovePaddle>().haltInput == false){
			this.GetComponent<MovePaddle>().haltInput = true;
		}
		if (useAI){
			//needs movement vector of
			ballDir = ball.GetComponent<BounceBall>().direction.normalized;

			RaycastHit info;
			Ray ray = new Ray(ball.transform.position, ballDir * sightDistance);
			Debug.DrawRay (ray.origin, ballDir * sightDistance, rayColorA);
			for (int i = 0; i < numReflections; ++i){
				if (i == 0 && Physics.Raycast(ray, out info, sightDistance)){
				}
				else if (Physics.Raycast(ray.origin, ray.direction, out info, sightDistance)){
					ballDir = Vector3.Reflect(ballDir, info.normal);
					ray = new Ray(info.point, ballDir);
					Debug.DrawRay(info.point, info.normal * 3, Color.grey);
					Debug.DrawRay(info.point, ballDir * sightDistance, rayColorB);

					DetectGoal(info);
				}
			}
		}
	}

	void DetectGoal(RaycastHit info){
		if (info.collider.tag == "Goal" || info.collider.tag == "Wall"){
			target = info.point;
			if (target.y >= transform.position.y + snapMovement){
				this.GetComponent<MovePaddle>().MoveUp();
			}
			else if (target.y <= transform.position.y - snapMovement)
				this.GetComponent<MovePaddle>().MoveDown();
		}
	}
}