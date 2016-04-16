using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TagView : MonoBehaviour {

	public sidemenu side_menu;
	public float target_position = -259, initial_position, acceleration = 80;
	public RectTransform rec;
	public bool closed = true;
	public TagViewController tag_view_controller;

	public Dictionary<string,Dictionary<string,List<string>>> tags = new Dictionary<string,Dictionary<string,List<string>>>();

	// Use this for initialization
	void Start () {
		rec = this.GetComponent<RectTransform> ();
		initial_position = rec.anchoredPosition.y;
		tag_view_controller = this.GetComponent<TagViewController> ();
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
}
