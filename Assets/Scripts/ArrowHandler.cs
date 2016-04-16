using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class ArrowHandler : MonoBehaviour ,IPointerClickHandler{

	public InfoView info_view;
	public bool is_right;

	void Start()
	{
		info_view = transform.parent.GetComponent<InfoView> ();
	}

	public void OnPointerClick (PointerEventData eventData) 
	{
		if(is_right)
		{
			Debug.Log("Calling method right");
			info_view.MoveRight();
		}
		if(!is_right)
		{
			Debug.Log("Calling method left");
			info_view.MoveLeft();
		}
	}

}
