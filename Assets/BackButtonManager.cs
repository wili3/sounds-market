using UnityEngine;
using System.Collections;

public class BackButtonManager : MonoBehaviour {

	public sidemenu side_menu;
	public InfoView info_view;
	public OfferCreationViewController ocview_controller;
	public UserView user_view;
	public TagView tag_view;
	public SupportView support_view;
	public PopUpLoginView login_view;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			if(!login_view.closed){
				login_view.closed = true;
				return;
			}
			if(!support_view.closed){
				support_view.closed = true;
				return;
			}
			if(!user_view.closed){
				user_view.closed = true;
				return;
			}
			if(!tag_view.closed){
				tag_view.closed = true;
				return;
			}
			if(!info_view.closed){
				info_view.closed = true;
				return;
			}
			if(!ocview_controller.closed){
				ocview_controller.closed = true;
				return;
			}
			if(!side_menu.closed){
				side_menu.closed = true;
				return;
			}

			Application.Quit();
		}
	}
}
