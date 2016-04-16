using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ClearCreation : MonoBehaviour, IPointerClickHandler {

	public OfferCreationViewController ref_creation;

	// Use this for initialization
	void Start () {
		ref_creation = this.transform.parent.GetComponent<OfferCreationViewController>();
	}
	
	// Update is called once per frame
	public void OnPointerClick (PointerEventData eventData) 
	{
		ref_creation.clearCreation ();
	}
}
