using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class ButtonTextScript : MonoBehaviour , IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler{
	public Text childText;
	public Color childTextInitialColor;
	// Use this for initialization
	void Start () {
		childText = transform.GetComponentInChildren<Text>();
		childTextInitialColor = childText.color;
	}
	
	// Update is called once per frame
	public void OnPointerDown (PointerEventData eventData) {
		childText.color = Color.white;
	}


	public void OnPointerUp (PointerEventData eventData) {
		childText.color = childTextInitialColor;
	}

	public void OnPointerExit(PointerEventData eventData) {
		childText.color = childTextInitialColor;
	}

	public void OnPointerEnter (PointerEventData eventData) {
		childText.color = Color.white;
	}
}
