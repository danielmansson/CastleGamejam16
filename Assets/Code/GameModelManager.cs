using UnityEngine;
using System.Collections;


public class GameModelManager : MonoBehaviour
{
	public event System.Action OnModelInitialized;

	public GameModel Model { get; private set; }

	void Start ()
	{
		//Fetch whatever data needed from System.Instance.GameInfo and init the Model
		Model = new GameModel(new GameModelConfig()
		{
			secondsPerTick = 0.5f * 0.25f * 0.5f
		});

		if (OnModelInitialized != null)
		{
			OnModelInitialized();
		}
	}
	
	void Update ()
	{
		Model.Update(Time.deltaTime);
	}
}
