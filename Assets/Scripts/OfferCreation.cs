using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OfferCreation : MonoBehaviour {

	public ui ref_ui;
	public contentmanager ref_content;
	public ScrollRect ref_scroll;
	
	
	public float ref_y;
	public float initial_y;
	
	public float increment_y = 30;
	
	public RectTransform rec;
	
	float time_to_begin_to_move = 0;
	
	public bool get_down = false;

	// Use this for initialization
	void Start () {
		rec = this.transform.GetChild (1).GetComponent<RectTransform> ();
		initial_y = rec.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (ref_scroll.velocity.y > 1 || ref_scroll.velocity.y < -1)
		{
			if (ref_ui.moving_vertical_direction == "Down" && rec.transform.position.y > ref_y && ref_scroll.normalizedPosition.y < 1) 
			{
				time_to_begin_to_move += Time.deltaTime;
				
				if(time_to_begin_to_move > 0.5f)rec.transform.position = new Vector3 (rec.transform.position.x, rec.transform.position.y - increment_y, rec.transform.position.z);
				//increment_y += 0.01f;
			}
			if (ref_ui.moving_vertical_direction == "Up" && rec.transform.position.y < initial_y)
			{
				rec.transform.position = new Vector3 (rec.transform.position.x, rec.transform.position.y + increment_y, rec.transform.position.z);
				//increment_y += 0.01f;
			}
		}
		else
		{
			if (ref_ui.moving_vertical_direction == "Down" && rec.transform.position.y > ref_y && ref_scroll.normalizedPosition.y < 1) 
			{
				time_to_begin_to_move +=Time.deltaTime;
				if(time_to_begin_to_move > 0.5f)rec.transform.position = new Vector3 (rec.transform.position.x, rec.transform.position.y - increment_y, rec.transform.position.z);
				//increment_y += 0.01f;
			}
			if (ref_ui.moving_vertical_direction == "Up" && rec.transform.position.y < initial_y)
			{
				rec.transform.position = new Vector3 (rec.transform.position.x, rec.transform.position.y + increment_y, rec.transform.position.z);
				//increment_y += 0.01f;
			}
		}
		
		if(get_down)
		{
			rec.transform.position = new Vector3 (rec.transform.position.x, rec.transform.position.y + increment_y, rec.transform.position.z);
		}
		
		if (rec.transform.position.y < ref_y) 
		{
			time_to_begin_to_move = 0;
			rec.transform.position = new Vector3(rec.transform.position.x,ref_y,rec.transform.position.z);
		}
		
		if (rec.transform.position.y > initial_y) 
		{
			time_to_begin_to_move = 0;
			get_down = false;
			rec.transform.position = new Vector3(rec.transform.position.x,initial_y,rec.transform.position.z);
		}
		
		
	}
}
