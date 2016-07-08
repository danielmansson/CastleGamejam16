using UnityEngine;
using System.Collections;

public class TextController : MonoBehaviour {

	public GameObject levelUpText;
	public GameObject speedUpText;
	public GameObject gameOverText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowLevelUpText(){
		levelUpText.SetActive();
	}

	public void ShowSpeedUpText(){
		speedUpText.SetActive();
	}

	public void ShowGameOverText(){
		gameOverText.SetActive();
	}
}
