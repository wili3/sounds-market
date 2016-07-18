using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ProductsManager : MonoBehaviour {

	public Dictionary<string,Dictionary<string,List<string>>> current_offers_view = new Dictionary<string, Dictionary<string, List<string>>>();
	public Dictionary<string,List<Texture2D>> current_textures = new Dictionary<string, List<Texture2D>>();

	public List<Texture2D> sprites;
	public List<Texture2D> main_sprites;
	public List<Texture2D> my_products_sprites;

	public Dictionary<string,Dictionary<string,List<string>>> main_offers = new Dictionary<string, Dictionary<string, List<string>>>();
	public Dictionary<string,List<Texture2D>> main_textures = new Dictionary<string, List<Texture2D>>();

	public Dictionary<string,Dictionary<string,List<string>>> my_offers = new Dictionary<string, Dictionary<string, List<string>>>();
	public Dictionary<string,List<Texture2D>> my_textures = new Dictionary<string, List<Texture2D>>();

	public int total_products_current_view = 15;
	public ImageManager image_manager;
	public contentmanager content_manager;
	void Start()
	{
		sprites = main_sprites;
		current_offers_view = main_offers;
		current_textures = main_textures;
	}

	void Update()
	{

		if(current_offers_view.Count == total_products_current_view && !image_manager.downloading && image_manager.index < total_products_current_view)
		{
			if(!image_manager.downloading)
			{
				if(current_offers_view == my_offers)
				{
					Debug.Log("Downloading my offer image");
				}
				List<string> key;
				key = current_offers_view[image_manager.index.ToString()]["key"];

				StartCoroutine(image_manager.GetObject_async(key[0]));
				image_manager.downloading = true;
			}
		}

	}

	public void ChangeContextToMyOffers()
	{
		current_offers_view = my_offers;
		current_textures = my_textures;
		sprites = my_products_sprites;
		image_manager.downloading = false;
		image_manager.index = my_products_sprites.Count;
		total_products_current_view = my_offers.Count;
		Debug.Log("My offers : " + my_offers.Count.ToString());
		content_manager.HardReset ();
	}

	public void ChangeContextToMain()
	{
		current_offers_view = main_offers;
		current_textures = main_textures;
		sprites = main_sprites;
		image_manager.downloading = false;
		content_manager.HardReset ();
		image_manager.index = main_sprites.Count;
		total_products_current_view = main_offers.Count;
	}

	public void ChangeContextToCurrent()
	{
		content_manager.Reset ();
	}


}
