using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class CloseInfoView : MonoBehaviour ,IPointerClickHandler {
	
	public InfoView info_view;

	void Start ()
	{
		info_view = this.transform.parent.parent.GetComponent<InfoView> ();
	}
	
	public void OnPointerClick (PointerEventData eventData) 
	{
		info_view.closed = true;
	}
	
	// Use this for initialization
	
}
