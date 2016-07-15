using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextController : MonoBehaviour {

	public GameObject levelUpText;
	public GameObject speedUpText;
	public GameObject gameOverText;
	public Text scoreText;

	// Use this for initialization
	void Start ()
	{
		EventManager.Instance.RegisterEvent<EventChangeText>(HandleChangeTextEvent);
		
	}

	// Update is called once per frame
	void Update () {

	}

	// Update is called once per frame
	void OnDestroy () {
		EventManager.Instance.UnregisterEvent<EventChangeText>(HandleChangeTextEvent);
	}

	IEnumerator ShowLevelUpText()
	{
		levelUpText.SetActive(true);
		yield return new WaitForSeconds(1f);
		levelUpText.SetActive(false);
	}

	IEnumerator ShowSpeedUpText()
	{
		speedUpText.SetActive(true);
		yield return new WaitForSeconds(1f);
		speedUpText.SetActive(false);
	}

	IEnumerator ShowGameOverText()
	{
		gameOverText.SetActive(true);
		yield return new WaitForSeconds(5f);
		scoreText.text = "Score: " + Systems.Instance.GameInfo.score*100;
		Debug.Log(scoreText + "FFS OMG");
	}

	void HandleChangeTextEvent(EventChangeText args)
	{
//		Debug.Log("I wanna change the text: " + args.Name);
		if(args.Name.Equals("LevelUp")){
			StartCoroutine(ShowLevelUpText());
		}
		else if(args.Name.Equals("SpeedUp")){
			StartCoroutine(ShowSpeedUpText());
			}
		else if(args.Name.Equals("GameOver")){
			StartCoroutine(ShowGameOverText());
		}
		else
		{
			Debug.LogWarning("Text missing: " + args.Name);
		}
	}
}
