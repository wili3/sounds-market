using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ConnectFacebookButton : MonoBehaviour ,IPointerClickHandler {

	public FacebookInitializer fb_initializer;

	void Start()
	{
		fb_initializer = GameObject.FindGameObjectWithTag ("Player").GetComponent<FacebookInitializer>();
	}

	public void OnPointerClick (PointerEventData eventData) 
	{
		fb_initializer.CallFBLogin ();
		Debug.Log ("hola");
	}

	void SendEmail ()
	{
		string email = "guillempsx2@hotmail.com";
		string subject = MyEscapeURL("My Subject");
		string body = MyEscapeURL("My Body\r\nFull of non-escaped chars");
		Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
	}
	string MyEscapeURL (string url)
	{
		return WWW.EscapeURL(url).Replace("+","%20");
	}
	
	// Use this for initialization
	
}
