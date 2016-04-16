using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class OpenSupportView : MonoBehaviour ,IPointerClickHandler {
	
	public SupportView support_view;
	
	public void OnPointerClick (PointerEventData eventData) 
	{
		support_view.gameObject.SetActive (true);
		support_view.closed = false;
	}
	
	// Use this for initialization
	
}
