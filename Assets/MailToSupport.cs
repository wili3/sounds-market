using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MailToSupport : MonoBehaviour ,IPointerClickHandler {

	public string support_mail = "pau.agusti@gmail.com";

	public void OnPointerClick (PointerEventData eventData) 
	{
		SendEmail (support_mail);
	}
	
	void SendEmail (string _email)
	{
		string email = _email;
		string subject = MyEscapeURL("");
		string body = MyEscapeURL("");
		Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
	}
	string MyEscapeURL (string url)
	{
		return WWW.EscapeURL(url).Replace("+","%20");
	}
	
	// Use this for initialization
	
}
