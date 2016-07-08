using UnityEngine;
using System.Collections;

public class TextController : MonoBehaviour {

	public GameObject levelUpText;
	public GameObject speedUpText;
	public GameObject gameOverText;

	// Use this for initialization
	void Start ()
	{
		EventManager.Instance.RegisterEvent<EventChangeText>(HandleChangeTextEvent);
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ShowLevelUpText(){
		levelUpText.SetActive(true);
	}

	void ShowSpeedUpText(){
		speedUpText.SetActive(true);
	}

	void ShowGameOverText(){
		gameOverText.SetActive(true);
	}

	void HandleChangeTextEvent(EventChangeText args)
	{
		Debug.Log("I wanna change the text: " + args.Name);
		if(args.Name.Equals("LevelUp")){
			ShowLevelUpText();
		}
		else if(args.Name.Equals("SpeedUp")){
			ShowSpeedUpText();
		}
		else if(args.Name.Equals("GameOver")){
			ShowGameOverText();
		}
		else
		{
			Debug.LogWarning("Text missing: " + args.Name);
		}
	}
}
