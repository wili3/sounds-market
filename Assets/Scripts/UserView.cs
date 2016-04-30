using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UserView : MonoBehaviour {

	public sidemenu side_menu;
	public float target_position = -259, initial_position, acceleration = 80;
	public RectTransform rec;
	public bool closed = true;
	public int current_index_user = -1;
	public RawImage profile_pic;
	public InputField user_name, user_rates, user_mail;
	public bool my_user = false;
	public RawImage[] stars = new RawImage[5];
	public ProductsManager products_manager;
	public UsersManager users_manager;
	public InfoView info_view;
	// Use this for initialization
	void Start () {
		rec = this.GetComponent<RectTransform> ();
		initial_position = rec.anchoredPosition.y;
		users_manager = GameObject.FindGameObjectWithTag ("ProductsManager").GetComponent<UsersManager> ();
		info_view = GameObject.FindGameObjectWithTag ("InfoView").GetComponent<InfoView> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(closed)
		{
			rec.anchoredPosition = new Vector2(rec.anchoredPosition.x,rec.anchoredPosition.y - acceleration);
			if(rec.anchoredPosition.y < initial_position)
			{
				rec.anchoredPosition = new Vector2(rec.anchoredPosition.x,initial_position);
				this.gameObject.SetActive(false);
			}
		}
		
		if(!closed)
		{
			rec.anchoredPosition = new Vector2(rec.anchoredPosition.x,rec.anchoredPosition.y + acceleration);
			if(rec.anchoredPosition.y > target_position)
			{
				rec.anchoredPosition = new Vector2(rec.anchoredPosition.x,target_position);
			}
		}
	}

	public void CloseIt ()
	{
		my_user = false;
		closed = true;
	}

	public void OpenIt(string index, bool is_my_user)
	{
		closed = false;
		side_menu.closed = true;

		if (!is_my_user) {
			LoadUserInfo ();
		} else {
			LoadMyUserInfo();
		}
	}

	public void LoadUserInfo()
	{
		Debug.Log (info_view.current_user_id);
		users_manager.AskUser (info_view.current_user_id.ToString());
		Debug.Log (users_manager.other_users_info [info_view.current_user_id].Count);
		//  HERE I SHOULD ASSIGN EACH FIELD TO THE VARS OF THIS VIEW CLASS
		Debug.Log (users_manager.other_users_info [info_view.current_user_id]["name"].Count);
		user_name.text = users_manager.other_users_info [info_view.current_user_id] ["name"] [0];
		user_mail.text = users_manager.other_users_info [info_view.current_user_id] ["email"] [0];
	}

	public void LoadMyUserInfo ()
	{
		//  HERE I SHOULD ASSIGN EACH FIELD TO THE VARS OF THIS VIEW CLASS
		user_name.text = users_manager.my_user_info ["name"] [0];
		user_mail.text = users_manager.my_user_info ["email"] [0];
	}
}
