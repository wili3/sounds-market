using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ScrollUpQuickly : MonoBehaviour ,IPointerClickHandler {
	
	public contentmanager content;
	
	public void OnPointerClick (PointerEventData eventData) 
	{
		content.ref_scroll.scroll.velocity = new Vector2(0,-100000);
	}
	// Use this for initialization
	
}
