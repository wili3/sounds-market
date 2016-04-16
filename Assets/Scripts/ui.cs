using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ui : MonoBehaviour {
	
	public Canvas canvas;
	public ScrollRect scroll;
	public bool moving_horizontal;
	public string moving_vertical_direction = "Down";
	public string current_swipe_direction;
	
	public Vector3 lastPos;
	public contentmanager content_ref;
	public bool direction_taken;
	int frame_count;
	float total_swipe_x;
	float total_swipe_y;
	
	Vector3 last_mouse_pos;
	
	public bool menu_visible;
	// Use this for initialization
	void Start () 
	{

		Application.targetFrameRate = 60;
		scroll.horizontal = false;
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		bool up = false;
		
		if (scroll.content.transform.position.y < lastPos.y) 
		{
			moving_vertical_direction = "Up";
			checkOutsiders();
		}
		
		if (scroll.content.transform.position.y > lastPos.y) 
		{
			moving_vertical_direction = "Down";
			checkOutsiders();
		}
		
		if (!menu_visible) 
		{
			checkScrollMovement ();
		}
		
		if (menu_visible) 
		{
			checkMenuState();
		}
		
		lastPos = scroll.content.transform.position;
	}
	
	
	bool going_down ()
	{
		bool down = false;
		
		if (scroll.content.transform.position.y > lastPos.y) 
		{
			down = true;
		}
		
		return down;
	}
	
	void checkOutsiders ()
	{

	}
	
	void checkScrollMovement()
	{

	}
	
	void checkMenuState()
	{

	}
	
}
