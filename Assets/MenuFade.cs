using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuFade : MonoBehaviour {

	public Text winText;
	public Text instructionText;
	public float timeToChange = 5f;
	public int timeSteps = 100;

	MeshRenderer blur;
	public float maxDistortion = 5f;
	public float maxSize = 3f;

	public Color fade = Color.white;

	public IEnumerator Blur(string top, string bottom){
		//Debug.Log("blur started");
		ResetDefaults();
		winText.text = top;
		instructionText.text = bottom;
		float maxD = maxDistortion;
		float maxS = maxSize;
		for (int i = 0; i < timeSteps; ++i){
			yield return new WaitForSeconds(timeToChange/timeSteps);
			maxD += maxDistortion/timeSteps;
			blur.material.SetFloat("_BumpAmt", maxD);
			maxS += maxSize/timeSteps;
			blur.material.SetFloat("_Size", maxS);
			fade.a += 255/timeSteps;
			winText.color = fade;
			instructionText.color = fade;
		}
	}

	public void ResetDefaults(){
		blur = this.GetComponent<MeshRenderer>();
		blur.material.SetFloat("_BumpAmt", 0f);
		blur.material.SetFloat("_Size", 0f);
		fade.a = 0;
		winText.color = fade;
		instructionText.color = fade;
	}
}
