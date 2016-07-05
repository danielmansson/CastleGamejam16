using UnityEngine;
using System.Collections;

public class GameModelDummy
{

}

public class GameModelManager : MonoBehaviour
{
	public event System.Action OnModelInitialized;

	public GameModelDummy Model { get; private set; }

	void Start ()
	{
		//Fetch whatever data needed from System.Instance.GameInfo and init the Model
		Model = new GameModelDummy();

		if (OnModelInitialized != null)
		{
			OnModelInitialized();
		}
	}
	
	void Update ()
	{
	
	}
}
