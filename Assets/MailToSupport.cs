using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MailToSupport : MonoBehaviour, IPointerClickHandler {

	public string support_mail = "pau.agusti@gmail.com";
	public string subject = "Sounds Market";
	public InputField input_field;

	public void OnPointerClick (PointerEventData eventData) 
	{
		SendEmail ();
	}


	void SendEmail ()
	{
		string email = support_mail;
		string _subject = MyEscapeURL(subject);
		string body = MyEscapeURL(input_field.text);
		Application.OpenURL("mailto:" + email + "?subject=" + _subject + "&body=" + body);
		input_field.text = "";
	}
	string MyEscapeURL (string url)
	{
		return WWW.EscapeURL(url).Replace("+","%20");
	}
	
	// Use this for initialization
	
}
