using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class SubmitTags : MonoBehaviour ,IPointerClickHandler {

	public TagViewController tag_view_controller;

	public void OnPointerClick (PointerEventData eventData) 
	{
		tag_view_controller.Submit ();
	}
	
	// Use this for initialization
	
}