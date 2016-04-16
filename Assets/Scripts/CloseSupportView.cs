using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class CloseSupportView : MonoBehaviour ,IPointerClickHandler {
	
	public SupportView support_view;
	
	public void OnPointerClick (PointerEventData eventData) 
	{
		support_view.closed = true;
	}
	
	// Use this for initialization
	
}
