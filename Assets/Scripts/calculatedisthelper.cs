using UnityEngine;
using System.Collections;

public class calculatedisthelper : MonoBehaviour {

	public float dist;
	public RectTransform rec_1;
	public RectTransform rec_2;
	// Use this for initialization
	void Start () 
	{
		dist = Vector3.Distance (rec_1.transform.position, rec_2.transform.position);
	}
}
