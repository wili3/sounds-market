using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TagViewController : MonoBehaviour {

	TagView tag_view;
	string parent_tag, mid_tag, low_tag;
	public bool is_search, ready_to_deliver;
	public Dropdown[] dropdown;
	public Image image_parent, image_mid, image_low;
	public Text text_parent, text_mid, text_low;
	public GameObject button_search, button_add_tags;
	public bool last_type, able_to_submit;
	public string default_option = "Choose tag";
	public List<string> dic;
	public OfferCreationViewController oc_view_controller;
	// Use this for initialization
	void Start ()
	{
		tag_view = this.GetComponent<TagView> ();
		//Dropdown.OptionData new_option = new Dropdown.OptionData (); //build an option for the dropdown,
		//new_option.text = "bla bla";
		//dropdown [1].options.Add (new_option);
		Reset ();
		for(int i = 0 ; i < dropdown.Length; i ++)
		{
			dropdown[i].captionText.text = default_option;
			dropdown[i].captionText.fontSize = 50;
		}

		dic = new List<string> ();
		StartCoroutine(Requester.getcategories(User.local_url, null));

		// TODO: I HAVE TO PUT ALL THE CATEGORY GROUPS EXISTING IN THE PDF INTO THE YAML THEN SET EVERY CATEGORY INSIDE THEIR GROUP, THEN PARSE FIRST THE PARENT CATEGORIES AND CREATE A DICTIONARY WITH A KEY FOREACH PARENT CATEGORY AND SET INSIDE THAT DICTIONARY ECAH KEY
	}

	void Update()
	{
		if (dropdown[0].captionText.text != default_option)
		{
			if(!dropdown[1].interactable)
			{
				dropdown[1].interactable = true;
			}
			if (dropdown[1].captionText.text != default_option)
			{
				if(!dropdown[2].interactable)
				{
					dropdown[2].interactable = true;
				}
				if (dropdown[2].captionText.text != default_option)
				{
					able_to_submit = true;
				}
			}
		}
		else
		{
			return;
		}
	}

	public void Reset()
	{
		for(int i = 0 ; i < dropdown.Length; i++)
		{
			dropdown[i].captionText.text = default_option;
			if(i > 0)
			{
				dropdown[i].interactable = false;
			}
		}
		button_search.SetActive (false);
		button_add_tags.SetActive (false);
		able_to_submit = false;
	}

	public void Submit()
	{
		if (!able_to_submit)
			return;
		dic.Clear ();

		dic.Add (dropdown [0].captionText.text);
		dic.Add (dropdown [1].captionText.text);
		dic.Add (dropdown [2].captionText.text);

		oc_view_controller.ShowTags (dic);

		Debug.Log ("Success retrieving tags");
		Reset ();
		tag_view.closed = true;
		// here is where the request for offers will be made or tags returned to the offer to be submitted
	}

	public void Initialize(bool search)
	{
		if (search)
			button_search.SetActive (true);
		else
			button_add_tags.SetActive (true);
		last_type = search;
	}
}
