using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class OpenSelfUserView : MonoBehaviour ,IPointerClickHandler {
	
	public UserView user_view;
	public InfoView info_view;
	
	public void OnPointerClick (PointerEventData eventData) 
	{
		if (User.is_guest_user ()) {
			GameObject.FindGameObjectWithTag("ProductsManager").GetComponent<ShowPopUpLoginView>().Show();
			return;
		}
		user_view.gameObject.SetActive (true);
		user_view.OpenIt ("null",true);
	}
}
