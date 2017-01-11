using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class TranslateCube : MonoBehaviour {

	RaycastHit hit;
	public float speedMult = 1f;
	private float threshold = 0.1f;
	private float distance = 0f;

	// Use this for initialization
	void Start () {
		hit = new RaycastHit();
	}
	
	// Update is called once per frame
	void Update () {
		//casts a 'ray' in the Z direction along the XY position of the mouse cursor
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		//if the ray collides with a collider return a hit object
		//the hit object contains various info about the object collided, the position of collision etc
		if (Physics.Raycast(ray, out hit, 1000.0f)){
			//update variable with hit coordinates
			Vector3 newPos = new Vector3(hit.point.x, hit.point.y, transform.position.z);

			//calculate the direction of movement as a normalized vector
			//we do this so that we can multiply this value directly with speed multipliers and 
			Vector3 dir = (newPos - this.transform.position).normalized;

			//Distance() is the same as (a-b).magnitude
			distance = Vector3.Distance(newPos, this.transform.position);

			//prevents mouse glitches by snapping to the mouse pos when within a threshold
			if (distance >= threshold) {
				//our formula for calculating the direction and speed to move within this frame
				//Time.deltaTime is defined as the time in seconds needed to conplete the last frame
				//We use this because that way our game will run the same acrosss all machines with varying framerates
				Vector3 translation = dir * speedMult * distance * Time.deltaTime;

				//lastly, Translate our object in world space (x,y,z) the amt needed this frame
				this.transform.Translate(translation, Space.World);
			}
		}
	}
}
