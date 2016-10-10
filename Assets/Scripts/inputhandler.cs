using UnityEngine;
using System.Collections;

public class inputhandler : MonoBehaviour {

	public topbar ref_topbar;
	public sidemenu ref_sidemenu;
	public InfoView ref_info;
	public OfferCreationViewController ref_creation;

	public Vector2 initial_swipe_position;
	public float difference_x;
	public float[] swipe_positions;
	public float inertia_sidemenu;
	public int count;
	public bool on_product = false;
	// Use this for initialization
	void Start () 
	{
		swipe_positions = new float[5];
	}
	
	// Update is called once per frame
	void Update ()
	{

		if ( Input.GetButtonDown("Fire1"))
		{
			Vector2 worldPoint = Camera.main.ScreenToWorldPoint( Input.mousePosition );
			initial_swipe_position = worldPoint;
			CheckSideMenu(worldPoint);
			RaycastHit2D hit = Physics2D.Raycast( worldPoint, Vector2.zero );

			if ( hit.collider != null )
			{
				if(hit.collider.name == "Burger")
				{
					ref_sidemenu.closed = !ref_sidemenu.closed;
				}
				else if(hit.collider.name == "Close")
				{
					//ref_creation.closed = true;
					//ref_info.edit_button.SetActive(false);
					// all this will have to be moved to each class DO THIS
				}
				else if(hit.collider.name == "OfferCreation")
				{
					if(ref_info.closed)
					{
						ref_creation.closed = false;
						ref_creation.clear_button.gameObject.SetActive(true);
						ref_creation.edit_button.gameObject.SetActive(false);

						Debug.Log("HOLA PASO POR AQUI");

						ref_creation.gameObject.SetActive(true);
					}
				}
				else if(hit.collider.name == "OfferCreationImage1" || hit.collider.name == "OfferCreationImage2" || hit.collider.name == "OfferCreationImage3" || hit.collider.name == "OfferCreationImage4")
				{
					char last_char = hit.collider.name[hit.collider.name.Length -1];
					int index = 0;
					int.TryParse(last_char.ToString(),out index);
					//ref_creation.SetImage(index-1);
				}
			}
		}
		if(Input.GetButton("Fire1"))
		{
			count += 1;
			Vector2 worldPoint = Camera.main.ScreenToWorldPoint( Input.mousePosition );
			handleSideMenu(worldPoint);
		}
		if(Input.GetButtonUp("Fire1"))
		{
			if(ref_sidemenu.handled)
			{
				ref_sidemenu.ref_scroll.vertical = true;
				int i = 3;
				while(i>0)
				{
					float dif = swipe_positions[swipe_positions.Length-1] - swipe_positions[i];
					inertia_sidemenu += dif;
					swipe_positions[i] = 0;
					i--;
				}

				ref_sidemenu.handled = false;
				Debug.Log("recT sidemenu position x : " + ref_sidemenu.rec.position.x);
				ref_sidemenu.closed = true;
				Debug.Log("inertia side menu : " + inertia_sidemenu);
				if(inertia_sidemenu > 10)ref_sidemenu.closed = false;
				initial_swipe_position = Vector2.zero;
				inertia_sidemenu = 0;
			}
		}
	
	}
	void CheckSideMenu(Vector2 ref_world_point)
	{
		if(!ref_sidemenu.closed)
		{
			if (ref_world_point.x > ref_sidemenu.ref_input_position.position.x && ref_world_point.y < ref_sidemenu.ref_input_position.position.y) 
			{
				//ref_sidemenu.closed = !ref_sidemenu.closed;
			}
		}
	}
	void handleSideMenu(Vector2 ref_world_point)
	{
		
		if(ref_world_point.x < ref_sidemenu.ref_input_position.position.x && ref_world_point.y < ref_sidemenu.ref_input_position.position.y && !ref_sidemenu.handled)// && ref_sidemenu.rec.position.x < 1.04f)
		{
			ref_sidemenu.ref_scroll.vertical = false;
			ref_topbar.get_down = true;
			ref_topbar.ref_ui.moving_vertical_direction = "Up";
			ref_sidemenu.handled = true;
			initial_swipe_position = Camera.main.ScreenToWorldPoint( Input.mousePosition );
			difference_x = initial_swipe_position.x - ref_sidemenu.rec.position.x;
			
			//ref_sidemenu.rec.position =  new Vector3(ref_world_point.x - difference_x , ref_sidemenu.rec.position.y, ref_sidemenu.rec.position.z);
			//if(ref_sidemenu.rec.anchoredPosition.x > ref_sidemenu.target_x) ref_sidemenu.rec.anchoredPosition = new Vector2(ref_sidemenu.target_x,ref_sidemenu.rec.anchoredPosition.y);
			fillPositions();
		}
		if(ref_sidemenu.handled)
		{
			ref_sidemenu.rec.position =  new Vector3(ref_world_point.x - difference_x , ref_sidemenu.rec.position.y, ref_sidemenu.rec.position.z);
			if(ref_sidemenu.rec.anchoredPosition.x > ref_sidemenu.target_x + 10) ref_sidemenu.rec.anchoredPosition = new Vector2(ref_sidemenu.target_x + 10,ref_sidemenu.rec.anchoredPosition.y);
			ref_sidemenu.calculatePerc();
			fillPositions();
		}
	}
	void fillPositions ()
	{
		for (int i = 0; i < swipe_positions.Length -1; i++) {
			swipe_positions[i] = swipe_positions[i + 1];
		}
		swipe_positions [swipe_positions.Length - 1] = Input.mousePosition.x;
	}
}
