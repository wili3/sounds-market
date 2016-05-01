using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class StarScript : MonoBehaviour ,IPointerClickHandler {

	UserView user_view;

	public int num;
	public bool alreadyRated = false;
	// Use this for initialization
	void Start () {
		user_view = GameObject.FindGameObjectWithTag ("UserView").GetComponent<UserView> ();
	}

	public void OnPointerClick (PointerEventData eventData) 
	{
		if (alreadyRated)
			return;
		SetStars (num);
		RateUser ();
	}

	public void SetStars(int num_of_average)
	{
		for (int i = 0; i < 5; i++) {
			if(i < num_of_average)
			{
				user_view.stars[i].color = Color.yellow;
			}
			else
			{
				user_view.stars[i].color = Color.grey;
			}
		}
	}

	void RateUser()
	{

		user_view.SetAlreadyRated();
		// rate user request
		Hashtable table = new Hashtable();
		Hashtable table_child = new Hashtable();
		int user_id = user_view.info_view.current_user_id;
		table_child.Add("rated_user_id",user_id);
		table_child.Add("points",num);
		table.Add("rating",table_child);

		StartCoroutine(Requester.rateuser(User.local_url,"api/ratings",table));
	}

	public void Reset()
	{
		alreadyRated = false;
	}
}
