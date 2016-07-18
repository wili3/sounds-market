using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class GoToMyProducts : MonoBehaviour, IPointerClickHandler {
	
	public ProductsManager products_manager;
	public inputhandler input_handler;
	public ImageManager image_manager;
	
	// Update is called once per frame
	public void OnPointerClick (PointerEventData eventData) 
	{
		input_handler.ref_sidemenu.closed = true;
		image_manager.RequestMyProducts ();
	}

	public void go_to()
	{
		input_handler.ref_sidemenu.closed = true;
		image_manager.RequestMyProducts ();
	}
}
