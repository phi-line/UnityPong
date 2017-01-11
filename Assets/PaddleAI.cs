using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PaddleAI : MonoBehaviour {

	public bool useAI = true;
	public GameObject ball;
	public float sightDistance = 5f;
	public int numReflections = 2;
	public float snapMovement = 0.1f;

	Vector3 ballDir;
	Vector3 target;
	
	// Update is called once per frame
	void Update () {
		if (useAI && this.GetComponent<MovePaddle>().haltInput == false){
			this.GetComponent<MovePaddle>().haltInput = true;
		}
		if (useAI){
			//needs movement vector of
			ballDir = ball.GetComponent<BounceBall>().direction.normalized;

			RaycastHit info;
			Ray ray = new Ray(ball.transform.position, ballDir * sightDistance);
			Debug.DrawRay (ray.origin, ballDir * sightDistance, Color.cyan);
			for (int i = 0; i < numReflections; ++i){
				if (i == 0 && Physics.Raycast(ray, out info, sightDistance)){
				}
				else if (Physics.Raycast(ray.origin, ray.direction, out info, sightDistance)){
					ballDir = Vector3.Reflect(ballDir, info.normal);
					ray = new Ray(info.point, ballDir);
					Debug.DrawRay(info.point, info.normal * 3, Color.blue);
					Debug.DrawRay(info.point, ballDir * sightDistance, Color.magenta);

					DetectGoal(info);
				}
			}
		}
	}

	void DetectGoal(RaycastHit info){
		if (info.collider.tag == "Goal" || info.collider.tag == "Wall"){
			target = info.point;
			if (target.y >= transform.position.y + transform.localScale.y/2){
				this.GetComponent<MovePaddle>().MoveUp();
			}
			else if (target.y <= transform.position.y - transform.localScale.y/2)
				this.GetComponent<MovePaddle>().MoveDown();
		}
	}
}