using UnityEngine;
using System.Collections;

public class SupportView : MonoBehaviour {

	public sidemenu side_menu;
	public float target_position = -259, initial_position, acceleration = 80;
	public RectTransform rec;
	public bool closed = true;

	public RectTransform rec_ref_target;
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
			if(rec.anchoredPosition.y > rec_ref_target.anchoredPosition.y)
			{
				rec.anchoredPosition = new Vector2(rec.anchoredPosition.x,target_position);
			}
		}
	}
}
