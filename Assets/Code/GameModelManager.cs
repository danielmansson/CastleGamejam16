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

	void OnDrawGizmos()
	{
		if (Model == null)
			return;

		int frame = Model.TotalFrame;

		if (frame % 8 == 0)
		{
			Gizmos.DrawSphere(Vector3.zero, 1f);
		}
		/*if (frame % 4 == 0)
		{
			Gizmos.DrawSphere(Vector3.right*2, 0.75f);
		}
		if (frame % 2 == 0)
		{
			Gizmos.DrawSphere(Vector3.right * 4, 0.4f);
		}*/
	}
}
