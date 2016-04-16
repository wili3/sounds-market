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
	public UsersManager user_manager;
	public RawImage[] stars = new RawImage[5];
	public ProductsManager products_manager;
	// Use this for initialization
	void Start () {
		rec = this.GetComponent<RectTransform> ();
		initial_position = rec.anchoredPosition.y;
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
	}

	public void LoadUserInfo(string user_id_string)
	{
		// Here has to search for the user id by the string given in products manager, then check if the user_id given exists locally and then retrieve the info and set it to the screen output
	}
}
