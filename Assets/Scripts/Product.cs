using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Product : MonoBehaviour, IPointerClickHandler {

	//TODO: remove inputhandler code and pass it here, then set all the data needed in info view class from the current index ( download images, present them in gallery, and finally load all the simple data )
	public inputhandler input_handler;
	public ProductsManager products_manager;

	public RawImage image;
	public Text tittle;
	public Text price;

	public float width;
	public float height;

	public RectTransform limit_up;
	public RectTransform limit_down;
	public BoxCollider2D collider_image;

	public float dist;
	public int index;
	// Use this for initialization
	void Start () 
	{
		image = transform.GetChild (0).GetComponent<RawImage> ();
		tittle = transform.GetChild (1).GetComponent<Text> ();
		price = transform.GetChild (2).GetComponent<Text> ();
		limit_up = transform.GetChild (3).GetComponent<RectTransform> ();
		limit_down = transform.GetChild (4).GetComponent<RectTransform> ();
		collider_image = this.GetComponent<BoxCollider2D> ();
		calculateDist ();
	}
	public void calculateDist()
	{
		dist = Vector3.Distance (limit_up.transform.position, limit_down.transform.position);
	}

	public void OnPointerClick (PointerEventData eventData) 
	{
		input_handler.ref_info.closed = false;
		input_handler.ref_info.gameObject.SetActive(true);
		input_handler.ref_sidemenu.closed = true;
		input_handler.ref_topbar.get_down = true;
		input_handler.ref_topbar.ref_ui.moving_vertical_direction = "Up";
		LoadProduct ();
	}

	public void LoadProduct()
	{
		input_handler.ref_info.current_index_offer = index;

		input_handler.ref_info.price_text.text = products_manager.current_offers_view [index.ToString ()] ["price"] [0];
		input_handler.ref_info.description_text.text = products_manager.current_offers_view [index.ToString ()] ["desc"] [0];
		input_handler.ref_info.tittle_text.text = products_manager.current_offers_view [index.ToString ()] ["tittle"] [0];
		input_handler.ref_info.user_text.text = products_manager.current_offers_view [index.ToString ()] ["seller"] [0];
		input_handler.ref_info.current_user_id = int.Parse(products_manager.current_offers_view [index.ToString ()] ["user_id"] [0]);
		input_handler.ref_info.rates_text.text = "112 valoraciones";
		input_handler.ref_info.current_image.texture = products_manager.sprites [index];

		if (products_manager.sprites [index].width >= products_manager.sprites [index].height) {
			input_handler.ref_info.current_image.GetComponent<RectTransform> ().sizeDelta = new Vector2 (input_handler.ref_info.rec.sizeDelta.x, CalculateProportion (input_handler.ref_info.rec.sizeDelta.x, products_manager.sprites [index].width, products_manager.sprites [index].height));
		}
		else
		{
			input_handler.ref_info.current_image.GetComponent<RectTransform>().sizeDelta = new Vector2 (CalculateProportion (1080f, products_manager.sprites [index].height, products_manager.sprites [index].width), 1080f);
		}

		input_handler.ref_info.current_image_shown = 0;
		input_handler.ref_info.left_arrow.SetActive (false);
		int num_of_pics = int.Parse (products_manager.current_offers_view [index.ToString ()] ["num_of_pics"] [0]);
		if (num_of_pics == 1)
		{
			input_handler.ref_info.right_arrow.SetActive (false);
		}
		else
		{
			input_handler.ref_info.right_arrow.SetActive (true);
		}

		if(!products_manager.current_textures.ContainsKey(index.ToString()))
		{
			List<Texture2D> tex = new List<Texture2D> (int.Parse (products_manager.current_offers_view [index.ToString ()] ["num_of_pics"] [0]));
			products_manager.current_textures.Add (index.ToString (), tex);
			input_handler.ref_info.current_tex_list = products_manager.current_textures [index.ToString ()];
			products_manager.current_textures [index.ToString ()].Add (products_manager.sprites [index]);

			if (num_of_pics > 1 && products_manager.current_textures [index.ToString ()].Count < num_of_pics) {
				for (int i = 1; i < num_of_pics; i++) {
					products_manager.current_textures [index.ToString ()].Add(null);
					string key  = products_manager.current_offers_view [index.ToString ()] ["key"] [i];
					products_manager.image_manager.product_detail_images.Add (i.ToString (), key);
				}
			}
		}
		else
		{
			input_handler.ref_info.current_tex_list = products_manager.current_textures [index.ToString ()];
		}
		if (products_manager.current_offers_view == products_manager.my_offers)
	    {
			input_handler.ref_info.edit_button.SetActive(true);
		}
	}
	public float CalculateProportion(float ref_1,float ref_2, float ref_3)
	{
		float desired_prop = 0;

		desired_prop = ref_1 * ref_3;
		desired_prop = desired_prop / ref_2;

		return desired_prop;
	}
}
