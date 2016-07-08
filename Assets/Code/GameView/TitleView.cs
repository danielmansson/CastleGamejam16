using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class TitleView : MonoBehaviour
{
	private static bool firstTime = true;
	private bool firstIteration = true;

	void Start ()
	{

	}

	void Update ()
	{
		if(firstIteration){
			firstIteration = false;
			StartCoroutine(TitleSounds());
		}	
	}

	IEnumerator TitleSounds()
	{
		if(firstTime) {
			firstTime = false;
			AudioEvent.Play("Music");
		} else {
			AudioEvent.ChangeParameter("Music", "LVL", 0);
		}
		AudioEvent.ChangeParameter("Music", "GameOver", 0);
		yield return new WaitForSeconds(1f);
		AudioEvent.Play("VoicePlaceTimeContinuum");
	}
}
