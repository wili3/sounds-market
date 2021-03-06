﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class EditCreation : MonoBehaviour, IPointerClickHandler {
	
	public OfferCreationViewController ref_creation;
	public bool sold = false;
	// Use this for initialization
	void Start () {
		ref_creation = this.transform.parent.GetComponent<OfferCreationViewController>();
	}
	
	// Update is called once per frame
	public void OnPointerClick (PointerEventData eventData) 
	{
		ref_creation.checkSubmitabbleWithIndex (sold);
		GameObject.FindGameObjectWithTag ("InfoView").GetComponent<InfoView> ().closed = true;
	}
}