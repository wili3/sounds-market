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
		user_view.gameObject.SetActive (true);
		user_view.OpenIt ("null",true);
	}
	
	// Use this for initialization
	
}
