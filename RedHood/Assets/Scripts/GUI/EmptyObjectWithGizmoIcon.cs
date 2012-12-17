using UnityEngine;
using System.Collections;

public class EmptyObjectWithGizmoIcon : MonoBehaviour {
	
	public string iconName;
	public Vector3 iconOffset = new Vector3(0f, 0.5f, 0f);
	public bool drawSphere = true;
	public float sphereRadius = 0.2f;
	
	void OnDrawGizmos() {
		Gizmos.DrawIcon(transform.position + iconOffset, iconName);
		if (drawSphere)
			Gizmos.DrawSphere (transform.position, sphereRadius);
	}	
}
