using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MailTo : MonoBehaviour ,IPointerClickHandler {

	public InfoView info_view;
	public string subject = "Sounds Market";

	public void OnPointerClick (PointerEventData eventData) 
	{
		if (User.is_guest_user ()) {
			GameObject.FindGameObjectWithTag ("ProductsManager").GetComponent<ShowPopUpLoginView> ().Show ();
			return;
		}
		SendEmail (info_view.current_email);
	}
	
	void SendEmail (string _email)
	{
		string email = _email;
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

