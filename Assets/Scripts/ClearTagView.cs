using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ClearTagView : MonoBehaviour ,IPointerClickHandler {
	
	public TagView tag_view;
	
	public void OnPointerClick (PointerEventData eventData) 
	{
		tag_view.gameObject.SetActive (true);
		tag_view.tag_view_controller.ResetTags ();
	}
	// Use this for initialization
	
}
