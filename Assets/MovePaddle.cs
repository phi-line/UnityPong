using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MovePaddle : MonoBehaviour {

	public bool haltInput;

	//KeyCodes are enums that point to keyboard buttons (eg: KeyCode.Space)
	public KeyCode upKey = KeyCode.W;
	public KeyCode downKey = KeyCode.S;

	KeyCode currentKey;
	bool upPressed, downPressed;
	float minHeight, maxHeight;

	//upper and lower bounds of the play area
	public float yBounds = 4f;
	public float speedMult;

	Vector3 unitVector = Vector3.zero;
	
	// Update is called once per frame
	void Update () {
		//checks if either the upKey or the downKey is pressed
		upPressed = Input.GetKey(upKey);
		downPressed = Input.GetKey(downKey);

		//gets the max vertice in world space of the 3d model. 
		//This is dynamic so we can adjust paddle size without affecting our logic
		minHeight = GetComponent<Renderer>().bounds.min.y;
		maxHeight = GetComponent<Renderer>().bounds.max.y;

		if (upPressed && !downPressed && !haltInput){
			//we set current key so that if both are pressed we have a fallback
			currentKey = upKey;
			MoveUp();
			unitVector = Vector3.up;
		}

		if (downPressed && !upPressed && !haltInput){
			currentKey = downKey;
			MoveDown();

			//we set the unit vector here so that on ball reflection later we can get the avg vector
			unitVector = Vector3.down;
		}

		//if both are pressed then we rely on the key that was pressed first
		if (upPressed && downPressed && !haltInput){
			if (currentKey == upKey && maxHeight <= yBounds){
				MoveUp();
			}
			else if (currentKey == downKey && minHeight >= -yBounds){
				MoveDown();
			}
		}
	}

	public void MoveUp (){
		//check to see if paddle is within maximum upper bounds
		if (maxHeight <= yBounds) {
			//translate in the up direction with our speed var in the fraction of time for this given frame
			Vector3 translation = Vector3.up * speedMult * Time.deltaTime;

			//lastly, Translate our object in world space (x,y,z)
			this.transform.Translate(translation, Space.World);
		}
	}

	public void MoveDown (){
		if (minHeight >= -yBounds) {
			Vector3 translation = Vector3.down * speedMult * Time.deltaTime;
			this.transform.Translate(translation, Space.World);
		}
	}

	public Vector3 getUnitVector(){
		return unitVector;
	}
}
