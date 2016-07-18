using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class CloseLoginVIew : MonoBehaviour ,IPointerClickHandler {
	
	public PopUpLoginView login_view;
	
	public void OnPointerClick (PointerEventData eventData) 
	{
		login_view.closed = true;
	}
	
	// Use this for initialization
	
}
