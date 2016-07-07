using UnityEngine;
using System.Collections;

/// <summary>
/// Should listen to dangerView.OnDestroy and call dangerView.DestroyView eventually after the callback.
/// </summary>
public abstract class PlayerVisualController : MonoBehaviour
{
	public abstract void Init(PlayerView dangerView);
}
