using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class SetImageScript : MonoBehaviour ,IPointerClickHandler {
	public int index;
	public OfferCreationViewController oc_view_controller;
	// Use this for initialization
	void Start () {
		oc_view_controller = GameObject.FindGameObjectWithTag ("OCViewController").GetComponent<OfferCreationViewController> ();
		for(int i = 0; i < transform.parent.childCount; i++)
		{
			if(transform == transform.parent.GetChild(i))
			{
				index = i;
			}
		}
	}
	
	// Update is called once per frame
	public void OnPointerClick (PointerEventData eventData) 
	{
		oc_view_controller.SetImage (index);
		Debug.Log ("hola");
	}
}
