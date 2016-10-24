using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UserView : MonoBehaviour {

	public Color starsColor;
	public sidemenu side_menu;
	public float target_position = -259, initial_position, acceleration = 80;
	public RectTransform rec, content_rec;
	public bool closed = true;
	public int current_index_user = -1;
	public RawImage profile_pic;
	public InputField user_name, user_rates, user_mail;
	public bool my_user = false;
	public RawImage[] stars = new RawImage[5];
	public ProductsManager products_manager;
	public UsersManager users_manager;
	public InfoView info_view;
	public Hashtable user_ratings_table, my_user_ratings_table;
	public Button edit_button;
	public LoadUserPic load_user_pic;

	public int num_of_total_ratings, num_of_5, num_of_4, num_of_3, num_of_2, num_of_1, average, total_height = 400;
	public List<GameObject> instantiated_offers;
	public GameObject prefab_scroll_item, scroll_view;


	public RectTransform rec_ref_target;
	public ImageManager image_manager;
	public GameObject button_update_location;
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
			if(rec.anchoredPosition.y > rec_ref_target.anchoredPosition.y)
			{
				rec.anchoredPosition = new Vector2(rec.anchoredPosition.x,rec_ref_target.anchoredPosition.y);
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
		DisableStars ();
		EnableStars ();
		closed = false;
		side_menu.closed = true;
		my_user = is_my_user;
		if (!is_my_user) {
			LoadUserInfo ();
		} else {
			LoadMyUserInfo();
		}
	}

	public void LoadUserInfo()
	{
		button_update_location.SetActive (false);
		scroll_view.gameObject.SetActive (true);
		users_manager.AskUser (info_view.current_user_id.ToString());
		user_name.text = users_manager.other_users_info [info_view.current_user_id] ["name"] [0];
		if (users_manager.other_users_info [info_view.current_user_id] ["email"] [0] != null) {
			user_mail.text = users_manager.other_users_info [info_view.current_user_id] ["email"] [0];
		} else {
			user_mail.text = "no email";
		}
		EnableStars ();
		LoadRatings ();
		edit_button.gameObject.SetActive (false);
		user_name.interactable = false;
		user_mail.interactable = false;
		my_user = false;
		StartCoroutine (load_user_pic.GetOtherUserPic (users_manager.other_users_info [info_view.current_user_id] ["facebook_profile_pic"][0]));
		StartCoroutine (Requester.getotheruserproducts(User.current_url(), "api/products/search?user_id=" + info_view.current_user_id.ToString(), null));
		// HERE I SHOULD LOAD THE RATES TO THE STARS
	}

	public void LoadMyUserInfo ()
	{
		button_update_location.SetActive (true);
		scroll_view.gameObject.SetActive (false);
		user_name.text = users_manager.my_user_info ["name"] [0];
		user_mail.text = users_manager.my_user_info ["email"] [0];

		user_name.interactable = true;
		user_mail.interactable = true;

		edit_button.gameObject.SetActive (true);
		LoadMyUserRatings ();

		DisableStars ();

		load_user_pic.user_image.texture = load_user_pic.sidemenu_image.texture;
		// HERE I SHOULD LOAD THE RATES TO THE STARS
	}

	public void LoadRatings()
	{
		if (user_ratings_table ["1"] != null) {
			num_of_1 = (int)user_ratings_table ["1"];
		}
		if (user_ratings_table ["2"] != null) {
			num_of_2 = (int)user_ratings_table ["2"];
		}
		if (user_ratings_table ["3"] != null) {
			num_of_3 = (int)user_ratings_table ["3"];
		}
		if (user_ratings_table ["4"] != null) {
			num_of_4 = (int)user_ratings_table ["4"];
		}
		if (user_ratings_table ["5"] != null) {
			num_of_5 = (int)user_ratings_table ["5"];
		}

		num_of_total_ratings = num_of_1 + num_of_2 + num_of_3 + num_of_4 + num_of_5;

		user_rates.text = num_of_total_ratings.ToString() + " Valoraciones";
		float average_not_ceiled = 0;
		if (num_of_total_ratings > 0)
		{
			average_not_ceiled = ((num_of_1) + (num_of_2 * 2) + (num_of_3 * 3) + (num_of_4 * 4) + (num_of_5 * 5)) / num_of_total_ratings;
			average = Mathf.FloorToInt (average_not_ceiled);

			SetStars ();
		}
		else
		{
			SetAllStarsGrey();
		}
	}

	public void LoadMyUserRatings()
	{
		if (my_user_ratings_table ["1"] != null) {
			num_of_1 = (int)my_user_ratings_table ["1"];
		}
		if (my_user_ratings_table ["2"] != null) {
			num_of_2 = (int)my_user_ratings_table ["2"];
		}
		if (my_user_ratings_table ["3"] != null) {
			num_of_3 = (int)my_user_ratings_table ["3"];
		}
		if (my_user_ratings_table ["4"] != null) {
			num_of_4 = (int)my_user_ratings_table ["4"];
		}
		if (my_user_ratings_table ["5"] != null) {
			num_of_5 = (int)my_user_ratings_table ["5"];
		}
		
		num_of_total_ratings = num_of_1 + num_of_2 + num_of_3 + num_of_4 + num_of_5;
		
		user_rates.text = num_of_total_ratings.ToString() + " Valoraciones";
		if (num_of_total_ratings > 0) {
			float average_not_ceiled = ((num_of_1) + (num_of_2 * 2) + (num_of_3 * 3) + (num_of_4 * 4) + (num_of_5 * 5)) / num_of_total_ratings;
			average = Mathf.FloorToInt (average_not_ceiled);
			SetStars ();
		} else {
			SetAllStarsGrey();
		}
	}

	public void DisableStars()
	{
		for(int i = 0; i < stars.Length; i++)
		{
			stars[i].GetComponent<StarScript>().enabled = false;
		}

		num_of_1 = 0;
		num_of_2 = 0;
		num_of_3 = 0;
		num_of_4 = 0;
		num_of_5 = 0;
		
	}

	public void EnableStars()
	{
		for(int i = 0; i < stars.Length; i++)
		{
			stars[i].GetComponent<StarScript>().enabled = true;
			stars[i].GetComponent<StarScript>().Reset();
		}
	}

	public void SetStars()
	{
		for (int i = 0; i < 5; i++) {
			if(i < average)
			{
				stars[i].color = starsColor;
			}
			else
			{
				stars[i].color = Color.grey;
			}
		}
	}

	void SetAllStarsGrey()
	{
		for (int i = 0; i < stars.Length; i++) 
		{
			stars[i].color = Color.grey;
		}
	}

	public void SetAlreadyRated()
	{
		for(int i = 0; i < stars.Length; i++)
		{
			stars[i].GetComponent<StarScript>().alreadyRated = true;
		}
	}

	public void InstantiateOtherUserProducts()
	{
		image_manager.index_other_user_products = 0;
		image_manager.product_images_user_view.Clear ();
		for(int i = 0; i < instantiated_offers.Count; i++)
		{
			Destroy(instantiated_offers[i]);
		}

		instantiated_offers.Clear ();

		content_rec.sizeDelta = new Vector2 (content_rec.sizeDelta.x, (total_height * products_manager.other_user_offers.Count));

		for(int i = 0; i < products_manager.other_user_offers.Count; i++)
		{
			GameObject temp_obj = Instantiate(prefab_scroll_item);
			temp_obj.transform.SetParent(content_rec.transform);
			temp_obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -total_height * i);
			temp_obj.GetComponent<ScrollItem>().Initialize(products_manager.other_user_offers[i.ToString()]);
			image_manager.product_images_user_view.Add(products_manager.other_user_offers[i.ToString()]["key"][0]);
			Debug.Log(i+": " + products_manager.other_user_offers[i.ToString()]["key"][0]);
			temp_obj.transform.localScale = new Vector3(1,1,1);
			instantiated_offers.Add(temp_obj);
		}
		// here i will make a loop foreach products in otheruserproducts, and Instantiate a prefab, set its position and pass the info to the prefab initializer
	}

	public void GetLocation()
	{
		GetLocationScript.Instance.RequestLocation ();
	}
}
