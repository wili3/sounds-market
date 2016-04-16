using UnityEngine;
using System.Collections;

public class contentmanager : MonoBehaviour {
	public Product[] products;
	public ImageManager image_manager;
	public ProductsManager products_manager;

	public RectTransform[] content;
	public int[] index_objects;
	public int[] object_ids;
	public int max_objects_per_batch =15;

	bool pool_object;

	public ui ref_scroll;
	public float tot_dist;

	public Vector2[] positions;
	public bool reset_boolean = false;
	// Use this for initialization
	void Start ()
	{
		int child_count = this.transform.childCount;
		content = new RectTransform[child_count]; 
		index_objects = new int[child_count];
		object_ids = new int[child_count];
		positions = new Vector2[child_count];

		for (int i = 0; i < child_count; i++) {
			content[i] = this.transform.GetChild(i).GetComponent<RectTransform>();
			index_objects[i] = i;
			object_ids[i] = i;
			positions[i] = content[i].anchoredPosition;
		}

		tot_dist = Vector3.Distance (content [0].transform.position, content [child_count - 1].transform.position) + Vector3.Distance (content [0].transform.position, content [1].transform.position);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetButtonDown("Fire2"))
		{
			reset_boolean = false;
			//products_manager.ChangeContext();
			Reset ();
		}

		if (ref_scroll.moving_vertical_direction == "Down") {
			if (checkPoolDown ()) {
				ObjectPoolDown ();
			}
		}

		if (ref_scroll.moving_vertical_direction == "Up") {
			if (checkPoolUp ()) {
				ObjectPoolUp ();
			}
		}
	}

	void ObjectPoolDown () 
	{
		if (ref_scroll.moving_vertical_direction == "Down" && object_ids[3] < products_manager.total_products_current_view)
		{
			try
			{
				float temp_delta_size_y = content [index_objects [0]].sizeDelta.y;
				products[index_objects[0]].image.texture = products_manager.sprites[object_ids[object_ids.Length - 1] + 1]; 
				products[index_objects[0]].tittle.text = products_manager.current_offers_view[(object_ids[object_ids.Length - 1] + 1).ToString()]["tittle"][0]; 
				products[index_objects[0]].price.text = products_manager.current_offers_view[(object_ids[object_ids.Length - 1] + 1).ToString()]["price"][0]; 
				products[index_objects[0]].index = object_ids[object_ids.Length - 1] + 1;
				content [index_objects [0]].sizeDelta = new Vector2(content [index_objects [0]].sizeDelta.x,calculateProportion(float.Parse(products_manager.current_offers_view[(object_ids[object_ids.Length - 1] + 1).ToString()]["height"][0]),content[0].sizeDelta.x,float.Parse(products_manager.current_offers_view[(object_ids[object_ids.Length - 1] + 1).ToString()]["width"][0])) + 250);

				float temp_offset = calculateProportion(float.Parse(products_manager.current_offers_view[(object_ids[object_ids.Length - 1] + 1).ToString()]["height"][0]),content[0].sizeDelta.x,float.Parse(products_manager.current_offers_view[(object_ids[object_ids.Length - 1] + 1).ToString()]["width"][0])) + 250;
		
				float dist;
				dist = Vector3.Distance (content [index_objects[0]].transform.position, content [index_objects[3]].transform.position) + products[index_objects[3]].dist + 22.5f;
				content [index_objects [0]].transform.position = new Vector3 (content [index_objects [0]].transform.position.x, content [index_objects [0]].transform.position.y - dist, content [index_objects [0]].transform.position.z);
				products [index_objects [0]].calculateDist();

				int[] prov_index = new int[index_objects.Length];
				for (int i = 0; i < index_objects.Length; i++) {
					prov_index [i] = index_objects [i];
				}
				
				index_objects [0] = index_objects [1];
				index_objects [1] = prov_index [2];
				index_objects [2] = prov_index [3];
				index_objects [3] = prov_index [0];
				
				for(int i = 0; i< object_ids.Length; i++)
				{
					object_ids [i] += 1;
				}

			}
			catch
			{

			}
		}

	}

	
	void ObjectPoolUp () 
	{
		if (ref_scroll.moving_vertical_direction == "Up" && object_ids[0] > 0) 
		{

			try
			{
				float temp_delta_size_y = content [index_objects [3]].sizeDelta.y;

				products[index_objects[3]].image.texture = products_manager.sprites[object_ids[0]-1]; 
				products[index_objects[3]].tittle.text = products_manager.current_offers_view[(object_ids[0]-1).ToString()]["tittle"][0]; 
				products[index_objects[3]].price.text = products_manager.current_offers_view[(object_ids[0]-1).ToString()]["price"][0];
				products[index_objects[3]].index = object_ids[0]-1;

				content [index_objects [3]].sizeDelta = new Vector2(content [index_objects [3]].sizeDelta.x,calculateProportion(float.Parse(products_manager.current_offers_view[(object_ids[0] - 1).ToString()]["height"][0]),content[0].sizeDelta.x,float.Parse(products_manager.current_offers_view[(object_ids[0] - 1).ToString()]["width"][0])) + 250);

				float temp_offset = calculateProportion(float.Parse(products_manager.current_offers_view[(object_ids[0] - 1).ToString()]["height"][0]),content[0].sizeDelta.x,float.Parse(products_manager.current_offers_view[(object_ids[0] - 1).ToString()]["width"][0])) + 250; 

				float dist;
				products [index_objects [3]].calculateDist();
				dist = Vector3.Distance (content [index_objects[3]].transform.position, content [index_objects[0]].transform.position) + products[index_objects[3]].dist + 22.5f;
				content [index_objects [3]].transform.position = new Vector3 (content [index_objects [3]].transform.position.x, content [index_objects [3]].transform.position.y + dist, content [index_objects [3]].transform.position.z);

				int[] prov_index = new int[index_objects.Length];
				for (int i = 0; i < index_objects.Length; i++) {
					prov_index [i] = index_objects [i];
				}
				
				index_objects [3] = index_objects [2];
				index_objects [2] = prov_index [1];
				index_objects [1] = prov_index [0];
				index_objects [0] = prov_index [3];
				
				for(int i = 0; i< object_ids.Length; i++)
				{
					object_ids [i] -= 1;
				}
			}
			catch
			{
				
			}
		}
		
	}

	bool checkPoolDown ()
	{
		bool move_object = false;

		if (content [index_objects[0]].transform.position.y > 1000) 
		{
			move_object = true;
		}

		return move_object;
	}

	bool checkPoolUp ()
	{
		bool move_object = false;
		
		if (content [index_objects[3]].transform.position.y < -1000) // NOT WORKING
		{
			move_object = true;
		}
		
		return move_object;
	}

 	public int currentlyDisplayed(int index)
	{
		for(int i = 0; i < object_ids.Length; i++)
		{
			if(object_ids[i] == index)
			{
				return i;
			}
		}
		return -1;
	}

	public void setImage(int index, int image_index)
	{
		products [index_objects [index]].image.texture = products_manager.sprites [image_index];
	}

	public float calculateProportion(float ref1, float ref2, float ref3)
	{
		float result = ref1 * ref2;
		result = result / ref3;
		return result;
	}

	public void Reset()
	{
		ref_scroll.scroll.normalizedPosition = new Vector2 (0.5f, 1);
		for(int i = 0; i < content.Length; i++)
		{
			content[i].anchoredPosition = positions[i];
			index_objects[i] = i;
			object_ids[i] = i;
			content[i].sizeDelta = new Vector2(971,971);

			content[i].gameObject.SetActive(true);
			products[i].index = i;

			try
			{
				products[i].image.texture = products_manager.sprites[i]; 
				products[i].tittle.text = products_manager.current_offers_view[i.ToString()]["tittle"][0]; 
				products[i].price.text = products_manager.current_offers_view[i.ToString()]["price"][0]; 
			}
			catch
			{
				products[i].image.texture = null;
				content[i].gameObject.SetActive(false);
			}
		}
	}
}

