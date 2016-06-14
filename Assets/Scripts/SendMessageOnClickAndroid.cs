using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class SendMessageOnClickAndroid : MonoBehaviour ,IPointerClickHandler {

	public GameObject Reciver;
	public string MethodName;
	// Use this for initialization
	void Start () {
		Reciver = GameObject.FindGameObjectWithTag ("OCViewController");
	}

	public void OnPointerClick (PointerEventData eventData) 
	{
		Reciver.SendMessage(MethodName, SendMessageOptions.DontRequireReceiver);
	}
}
