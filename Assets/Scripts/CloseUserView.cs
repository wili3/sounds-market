using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class CloseUserView : MonoBehaviour ,IPointerClickHandler {

	public UserView user_view;

	void Start()
	{
		user_view = this.transform.parent.parent.GetComponent<UserView> ();
	}

	public void OnPointerClick (PointerEventData eventData) 
	{
		user_view.CloseIt ();
	}
	
	// Use this for initialization
	
}
