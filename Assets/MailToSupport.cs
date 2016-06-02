using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MailToSupport : MonoBehaviour, IPointerClickHandler {

	public string support_mail = "pau.agusti@gmail.com";
	public string subject = "Sounds Market";

	public void OnPointerClick (PointerEventData eventData) 
	{
		SendEmail ();
	}


	void SendEmail ()
	{
		string email = support_mail;
		string _subject = MyEscapeURL(subject);
		string body = MyEscapeURL("");
		Application.OpenURL("mailto:" + email + "?subject=" + _subject + "&body=" + body);
	}
	string MyEscapeURL (string url)
	{
		return WWW.EscapeURL(url).Replace("+","%20");
	}
	
	// Use this for initialization
	
}
