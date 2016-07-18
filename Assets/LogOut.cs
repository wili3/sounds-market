using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class LogOut : MonoBehaviour ,IPointerClickHandler {


	
	public void OnPointerClick (PointerEventData eventData) 
	{
		LogOutUser ();
	}

	public void LogOutUser()
	{
		PlayerPrefs.DeleteAll ();
		Application.LoadLevel (0);
	}
	
	// Use this for initialization
	
}
