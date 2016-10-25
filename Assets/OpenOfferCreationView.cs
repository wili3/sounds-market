using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class OpenOfferCreationView : MonoBehaviour  ,IPointerClickHandler {

	public OfferCreationViewController oc_view_controller;

	public void OnPointerClick (PointerEventData eventData) 
	{
		if (User.is_guest_user ()) {
			GameObject.FindGameObjectWithTag ("ProductsManager").GetComponent<ShowPopUpLoginView> ().Show ();
			return;
		}
		oc_view_controller.closed = false;

		
		oc_view_controller.clear_button.gameObject.SetActive(true);
		oc_view_controller.sold_button.gameObject.SetActive (false);
		oc_view_controller.edit_button.gameObject.SetActive(false);
		oc_view_controller.submit_button.gameObject.SetActive (true);

		
		Debug.Log("HOLA PASO POR AQUI");
		
		oc_view_controller.gameObject.SetActive(true);
	}
}
