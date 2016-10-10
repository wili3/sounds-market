using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class CloseOfferCreationView : MonoBehaviour ,IPointerClickHandler {

	public OfferCreationViewController oc_view_controller;
	public InfoView info_view;

	public void OnPointerClick (PointerEventData eventData) 
	{
		oc_view_controller.Close ();
		oc_view_controller.closed = true;
	}
}
