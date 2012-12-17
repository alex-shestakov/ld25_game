using UnityEngine;
using System.Collections;

public class OnLoadingFinishedEnabler : MonoBehaviour {

	void OnLoadingFinished () {
		if (!gameObject.active)
			gameObject.SetActiveRecursively(true);
	}
}
