using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class EditProductTrigger : MonoBehaviour ,IPointerClickHandler {

	public InfoView info_view;
	public ProductEditor product_editor;
	public OfferCreationViewController offer_creation;

	public void OnPointerClick (PointerEventData eventData) 
	{
		product_editor.LoadCreatedOffer (info_view.current_index_offer);
	}

	// Use this for initialization

}
