using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ConnectFacebookButton : MonoBehaviour ,IPointerClickHandler {

	public FacebookInitializer fb_initializer;

	public void OnPointerClick (PointerEventData eventData) 
	{
		fb_initializer.CallFBLogin ();
		Debug.Log ("hola");
	}
	
	// Use this for initialization
	
}
