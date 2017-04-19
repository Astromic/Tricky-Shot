using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Map : MonoBehaviour {
	void OnEnable () {
		transform.position = new Vector3 (transform.position.x, -10 * transform.GetSiblingIndex (), transform.position.z);
	}
}
