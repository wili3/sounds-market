using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class GoToMyProducts : MonoBehaviour, IPointerClickHandler {
	
	public ProductsManager products_manager;
	public inputhandler input_handler;

	
	// Update is called once per frame
	public void OnPointerClick (PointerEventData eventData) 
	{
		products_manager.ChangeContextToMyOffers ();
		input_handler.ref_sidemenu.closed = true;
	}
}
