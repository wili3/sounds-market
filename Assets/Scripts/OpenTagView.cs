using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class OpenTagView : MonoBehaviour ,IPointerClickHandler {

	public TagView tag_view;
	public bool is_search = false; 

	public void OnPointerClick (PointerEventData eventData) 
	{
		tag_view.gameObject.SetActive (true);
		tag_view.closed = false;
		tag_view.tag_view_controller.Initialize (is_search);
		tag_view.side_menu.closed = true;
	}
	
	// Use this for initialization
	
}
