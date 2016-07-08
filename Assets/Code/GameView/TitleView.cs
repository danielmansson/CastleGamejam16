using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class TitleView : MonoBehaviour
{
	private bool firstTime = true;

	void Start ()
	{

	}

	void Update ()
	{
		if(firstTime){
			firstTime = false;
			StartCoroutine(TitleSounds());
		}	
	}

	IEnumerator TitleSounds()
	{
		AudioEvent.Play("Music");
		yield return new WaitForSeconds(1f);
		AudioEvent.Play("VoicePlaceTimeContinuum");
	}
}
