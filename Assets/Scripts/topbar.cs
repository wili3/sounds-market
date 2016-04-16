using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class topbar : MonoBehaviour {

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
	void Start ()
	{
		initial_y = transform.position.y;
		rec = this.GetComponent<RectTransform> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (ref_scroll.velocity.y > 1 || ref_scroll.velocity.y < -1)
		{
			if (ref_ui.moving_vertical_direction == "Down" && transform.position.y < ref_y && ref_scroll.normalizedPosition.y < 1) 
			{
				time_to_begin_to_move += Time.deltaTime;

				if(time_to_begin_to_move > 0.5f)transform.position = new Vector3 (transform.position.x, transform.position.y + increment_y, transform.position.y);
				//increment_y += 0.01f;
			}
			if (ref_ui.moving_vertical_direction == "Up" && transform.position.y > initial_y)
			{
				transform.position = new Vector3 (transform.position.x, transform.position.y - increment_y, transform.position.y);
				//increment_y += 0.01f;
			}
		}
		else
		{
			if (ref_ui.moving_vertical_direction == "Down" && transform.position.y < ref_y && ref_scroll.normalizedPosition.y < 1) 
			{
				time_to_begin_to_move +=Time.deltaTime;
				if(time_to_begin_to_move > 0.5f)transform.position = new Vector3 (transform.position.x, transform.position.y + increment_y, transform.position.y);
				//increment_y += 0.01f;
			}
			if (ref_ui.moving_vertical_direction == "Up" && transform.position.y > initial_y)
			{
				transform.position = new Vector3 (transform.position.x, transform.position.y - increment_y, transform.position.y);
				//increment_y += 0.01f;
			}
		}

		if(get_down)
		{
			transform.position = new Vector3 (transform.position.x, transform.position.y - increment_y, transform.position.y);
		}

		if (transform.position.y > ref_y) 
		{
			time_to_begin_to_move = 0;
			transform.position = new Vector3(transform.position.x,ref_y,transform.position.y);
		}

		if (transform.position.y < initial_y) 
		{
			time_to_begin_to_move = 0;
			get_down = false;
			transform.position = new Vector3(transform.position.x,initial_y,transform.position.y);
		}

	
	}
}
