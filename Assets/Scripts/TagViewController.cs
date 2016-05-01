using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TagViewController : MonoBehaviour {

	TagView tag_view;
	string parent_tag, mid_tag, low_tag, current_first_option, current_second_option;
	public bool is_search, ready_to_deliver;
	public Dropdown[] dropdown;
	public Image image_parent, image_mid, image_low;
	public Text text_parent, text_mid, text_low;
	public GameObject button_search, button_add_tags;
	public bool last_type, able_to_submit;
	public string default_option = "Choose tag";
	public List<string> dic;
	public OfferCreationViewController oc_view_controller;

	public Hashtable categories_table;

	public Dictionary<string,string> convert_dic, convert_inverse_dic;
	public Dictionary<string,List<string>> parent_keys;
	public List<string> first_dropdown_keys;
	void Start ()
	{
		convert_dic = new Dictionary<string, string> ();
		convert_inverse_dic = new Dictionary<string, string> (); 
		parent_keys = new Dictionary<string, List<string>> ();
		tag_view = this.GetComponent<TagView> ();

		Reset ();
		for(int i = 0 ; i < dropdown.Length; i ++)
		{
			dropdown[i].captionText.text = default_option;
			dropdown[i].captionText.fontSize = 50;
		}

		dic = new List<string> ();
		StartCoroutine(Requester.getcategories(User.current_url(), null));

		// TODO: I HAVE TO PUT ALL THE CATEGORY GROUPS EXISTING IN THE PDF INTO THE YAML THEN SET EVERY CATEGORY INSIDE THEIR GROUP, THEN PARSE FIRST THE PARENT CATEGORIES AND CREATE A DICTIONARY WITH A KEY FOREACH PARENT CATEGORY AND SET INSIDE THAT DICTIONARY ECAH KEY
	}

	void Update()
	{
		if (dropdown[0].captionText.text != default_option)
		{
			if(dropdown[0].captionText.text != current_first_option)
			{
				dropdown[1].interactable = false;
			}
			if(!dropdown[1].interactable)
			{
				dropdown[1].interactable = true;
				able_to_submit = true;
				Debug.Log("hey this is able to submit");
				current_first_option = dropdown[0].captionText.text;
				LoadDropdownOptions(1);
				dropdown[1].captionText.text = default_option;
				dropdown[1].value = 0;
				dropdown[2].captionText.text = default_option;
				dropdown[2].value = 0;
			}
			if (dropdown[1].captionText.text != default_option)
			{
				if(dropdown[1].captionText.text != current_second_option)
				{
					dropdown[2].interactable = false;
				}
				if(!dropdown[2].interactable)
				{
					dropdown[2].interactable = true;
					Debug.Log("hey this is able to submit");
					current_second_option = dropdown[1].captionText.text;
					LoadDropdownOptions(2);
					dropdown[2].captionText.text = default_option;
					dropdown[2].value = 0;
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
			dropdown[i].value = 0;
			dropdown[i].captionText.text = default_option;
			if(i > 0)
			{
				dropdown[i].interactable = false;
				dropdown[i].options.Clear();
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
		if(dropdown [1].captionText.text!= default_option)dic.Add (dropdown [1].captionText.text);
		if(dropdown [2].captionText.text!= default_option)dic.Add (dropdown [2].captionText.text);

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

	public void LoadCategories(Hashtable table)
	{
		categories_table = table;
		LoadConvertCategories ();
		LoadFirstDropdownOptions ();
	}

	void LoadConvertCategories()
	{
		ArrayList categories = (ArrayList)categories_table ["categories"];
		ArrayList category_groups = (ArrayList)categories_table ["category_groups"];

		for (int i = 0; i < category_groups.Count; i++)
		{
			Hashtable category_group = (Hashtable)category_groups[i];
			Debug.Log("GROUP ID : " + (string)category_group["id"]);
			parent_keys.Add((string)category_group["id"],new List<string>());
			convert_dic.Add((string)category_group["id"],(string)category_group["name"]);
			convert_inverse_dic.Add((string)category_group["name"],(string)category_group["id"]);
			first_dropdown_keys.Add((string)category_group["id"]);
		}

		for(int i = 0; i < categories.Count; i ++)
		{
			Hashtable category = (Hashtable)categories[i];
			Debug.Log(" ID : " + (string)category["id"]);
			try{
			parent_keys.Add((string)category["id"], new List<string>());
			convert_dic.Add((string)category["id"],(string)category["name"]);
			convert_inverse_dic.Add((string)category["name"],(string)category["id"]);
			}
			catch{

			}
			parent_keys[(string)category["group_id"]].Add((string)category["id"]);
		}
	}

	void LoadFirstDropdownOptions()
	{
		dropdown [0].options.Clear ();

		Dropdown.OptionData new_option_default = new Dropdown.OptionData ();
		new_option_default.text = default_option;
		dropdown [0].options.Add (new_option_default);
		//Dropdown.OptionData new_option = new Dropdown.OptionData (); //build an option for the dropdown,
		//new_option.text = "bla bla";
		//dropdown [1].options.Add (new_option);
		for(int i = 0; i < first_dropdown_keys.Count; i++)
		{
			Dropdown.OptionData new_option = new Dropdown.OptionData ();
			new_option.text = convert_dic[first_dropdown_keys[i]];
			dropdown[0].options.Add(new_option);
		}
	}

	void LoadDropdownOptions(int index)
	{
		dropdown [index].options.Clear ();
		string parent_option = dropdown [index - 1].captionText.text;

		Dropdown.OptionData new_option_default = new Dropdown.OptionData ();
		new_option_default.text = default_option;
		dropdown [index].options.Add (new_option_default);

		if (parent_keys [convert_inverse_dic [parent_option]].Count != null) 
		if (parent_keys [convert_inverse_dic [parent_option]].Count > 0) {
			for (int i = 0; i < parent_keys [convert_inverse_dic [parent_option]].Count; i++) {
				Dropdown.OptionData new_option = new Dropdown.OptionData ();
				new_option.text = convert_dic[parent_keys [convert_inverse_dic [parent_option]] [i]];
				dropdown [index].options.Add (new_option);
			}
		} else {
			dropdown[index].interactable = false;
		}
	}
}
