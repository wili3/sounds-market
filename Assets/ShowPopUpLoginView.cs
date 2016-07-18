using UnityEngine;
using System.Collections;

public class ShowPopUpLoginView : MonoBehaviour {

	public PopUpLoginView login_view;

	public void Show()
	{
		login_view.gameObject.SetActive (true);
		login_view.closed = false;
	}
}
