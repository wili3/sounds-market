using UnityEngine;
using System.Collections;

public class DropdownChangeNotifier : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnValueChanged ()
	{
		Debug.Log ("value changed");
	}
}
