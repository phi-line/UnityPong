using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MovePaddle : MonoBehaviour {

	//KeyCodes are enums that point to keyboard buttons (eg: KeyCode.Space)
	public KeyCode upKey = KeyCode.W;
	public KeyCode downKey = KeyCode.S;

	KeyCode currentKey;
	bool upPressed, downPressed;

	//upper and lower bounds of the play area
	public float yBounds = 4f;
	public float speedMult;

	Vector3 unitVector = Vector3.zero;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//checks if either the upKey or the downKey is pressed
		upPressed = Input.GetKey(upKey);
		downPressed = Input.GetKey(downKey);

		//gets the max vertice in world space of the 3d model. 
		//This is dynamic so we can adjust paddle size without affecting our logic
		float minHeight = GetComponent<Renderer>().bounds.min.y;
		float maxHeight = GetComponent<Renderer>().bounds.max.y;

		if (upPressed && !downPressed){
			//we set current key so that if both are pressed we have a fallback
			currentKey = upKey;

			//check to see if paddle is within maximum upper bounds
			if (maxHeight <= yBounds)
				MoveUp();

			unitVector = Vector3.up;
		}

		if (downPressed && !upPressed){
			currentKey = downKey;
			if (minHeight >= -yBounds)
				MoveDown();

			//we set the unit vector here so that on ball reflection later we can get the avg vector
			unitVector = Vector3.down;
		}

		//if both are pressed then we rely on the key that was pressed first
		if (upPressed && downPressed){
			if (currentKey == upKey && maxHeight <= yBounds){
				MoveUp();
			}
			else if (currentKey == downKey && minHeight >= -yBounds){
				MoveDown();
			}
		}
	}

	void MoveUp (){
		//translate in the up direction with our speed var in the fraction of time for this given frame
		Vector3 translation = Vector3.up * speedMult * Time.deltaTime;

		//lastly, Translate our object in world space (x,y,z)
		this.transform.Translate(translation, Space.World);
	}

	void MoveDown (){
		Vector3 translation = Vector3.down * speedMult * Time.deltaTime;
		this.transform.Translate(translation, Space.World);
	}

	public Vector3 getUnitVector(){
		return unitVector;
	}
}
