using UnityEngine;
using System.Collections;

public class SamePosition : MonoBehaviour {

	public Transform ref_transform;

	// Use this for initialization

	
	// Update is called once per frame
	void Update () {
		transform.position = ref_transform.position;
	}
}
