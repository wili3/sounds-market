using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class OfferCreationViewController : MonoBehaviour {

	public InfoView info_view;
	public ImageManager image_manager;
	public ProductsManager ref_products_manager;
	public float target_position = -259, initial_position, acceleration = 80;
	public RectTransform rec;
	public bool closed = true;
	public Texture2D draw_texture;
	public Texture2D initial_sprite;

	public Texture2D[] textures = new Texture2D[4];
	public RawImage[] images_to_upload = new RawImage[4];

	public InputField[] input_fields = new InputField[4];
	public Button clear_button;
	public Button submit_button;

	public int index_to_set;

	public bool images_ready;
	public bool fields_ready;
	public bool tags_ready = true; // equals true because currently wont do this section

	public bool ready_to_submit;

	public int total_pics_to_upload;

	public bool edit_mode = false;
	public int current_index_loaded_product;

	public GameObject[] tag_display_image = new GameObject[3];
	public List<string> tags_list;
	// Use this for initialization
	void Start () 
	{
		rec = this.GetComponent<RectTransform> ();
		initial_position = rec.anchoredPosition.y;
	}

	// Update is called once per frame
	void Update () {

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

	public void SetImage(int index)
	{
		index_to_set = index;
		IOSCamera.OnImagePicked += OnImage;
		IOSCamera.Instance.PickImage(ISN_ImageSource.Album);
	}

	private void OnImage (IOSImagePickResult result)
	{
		if(result.IsSucceeded) 
		{
			
			//destroying old texture
			
			//applying new texture
			draw_texture = result.Image;
			//IOSMessage.Create("Success", "Image Successfully Loaded, Image size: " + result.Image.width + "x" + result.Image.height);
			textures [index_to_set] = draw_texture;
			images_to_upload[index_to_set].texture = textures [index_to_set];

	
		} 
		else 
		{
			IOSMessage.Create("ERROR", "Image Load Failed");
		}
		
		IOSCamera.OnImagePicked -= OnImage;

	}

	public void checkSubmitabbleWithIndex()
	{
		Submit_with_index(info_view.current_index_offer);
		clearCreation();
	}

	public void checkSubmitabble()
	{
		if (input_fields [0].text != "Título..." && input_fields[1].text != "Descripción..." && input_fields[2].text != "Precio..." && input_fields[3].text != "Email...")
		{
			fields_ready = true;
		}
		for(int i = 0; i < images_to_upload.Length; i++)
		{
			if(textures[i] != initial_sprite && textures[i] != null)
			{
				total_pics_to_upload +=1 ;
			}
			else
			{
				if(total_pics_to_upload > 0)
					i = 10;
			}
		}
		images_ready = true;
		if(images_ready && fields_ready && tags_ready && !edit_mode)
		{
			ready_to_submit = true;
			Submit();
		}
	}

	private void Submit ()
	{
		Debug.Log ("CREATING A FUCKING OFFER WHEN SHOULDN'T single");
		ref_products_manager.my_products_sprites.Add (textures [0]);
		Dictionary<string,List<string>> dic_inside = new Dictionary<string, List<string>>();

		string email = input_fields [3].text;
		string tittle = input_fields [0].text;
		string desc =  input_fields [1].text;
		string seller = "Guillem";
		string price = input_fields [2].text + "€";
		string width = textures[0].width.ToString();
		string height =textures[0].height.ToString();
		string p_key = seller; 
		string num_of_pics = total_pics_to_upload.ToString ();
		string user_id = "21";
		
		List<string> email_list = new List<string>();
		email_list.Add(email);
		dic_inside.Add("email",email_list);
		
		List<string> tittle_list = new List<string>();
		tittle_list.Add(tittle);
		dic_inside.Add("tittle",tittle_list);
		
		List<string> desc_list = new List<string>();
		desc_list.Add(desc);
		dic_inside.Add("desc",desc_list);
		
		List<string> seller_list = new List<string>();
		seller_list.Add(seller);
		dic_inside.Add("seller",seller_list);
		
		List<string> price_list = new List<string>();
		price_list.Add(price);
		dic_inside.Add("price",price_list);

		List<string> width_list = new List<string>();
		List<string> height_list = new List<string>();

		List<string> key_list = new List<string>();
		for(int i = 0; i < total_pics_to_upload; i++)
		{
			string product_key = p_key + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + i.ToString();
			key_list.Add(product_key + ".jpg");
			SaveTextureToFile(product_key,i);
			if(Application.isMobilePlatform)
			{
				width = textures[i].width.ToString();
				height = textures[i].height.ToString();
			}
			if(Application.isEditor)
			{
				width = ref_products_manager.sprites[i].width.ToString();
				height = ref_products_manager.sprites[i].height.ToString();
			}
			width_list.Add(width);
			height_list.Add(height);
		}
		key_list.Add(p_key);
		dic_inside.Add("key",key_list);

		dic_inside.Add("width",width_list);

		dic_inside.Add("height",height_list);
		Debug.Log ("Widht Length : " + width_list.Count);

		List<string> num_of_pics_list = new List<string>();
		num_of_pics_list.Add (num_of_pics);
		dic_inside.Add ("num_of_pics", num_of_pics_list);

		dic_inside.Add("tags",tags_list);

		List<string> user_ids_list = new List<string> ();
		user_ids_list.Add (user_id);
		dic_inside.Add ("user_id", user_ids_list);

		image_manager.ParseDicToHash (dic_inside);
	}

	public void SubimtAfterPost(Dictionary<string,List<string>> dic_inside)
	{
		ref_products_manager.my_offers.Add(ref_products_manager.my_offers.Count.ToString(),dic_inside);
		
		clearCreation ();
		closed = true;
	}

	private void Submit_with_index (int index_to_submit)
	{
		int index = index_to_submit;
		try
		{
			ref_products_manager.current_offers_view[index.ToString()]["tittle"][0] = input_fields[0].text;
			ref_products_manager.current_offers_view[index.ToString()]["desc"][0] = input_fields[1].text;
			ref_products_manager.current_offers_view[index.ToString()]["price"][0] = input_fields[2].text;
			ref_products_manager.current_offers_view[index.ToString()]["email"][0] = input_fields[3].text;
			
			//check images, first if one has been added, then if one of the previous added has been changed
			
			int total_images_before = ref_products_manager.current_textures[index.ToString()].Count;
			Dictionary<string,int> checker = new Dictionary<string, int>();
			int total_images_now = 0;
			
			for(int i= 0; i < images_to_upload.Length; i++)
			{
				if(images_to_upload[i].texture != initial_sprite)
				{
					total_images_now +=1;
					checker.Add(i.ToString(),1);
					if(i == 0)
					{
						ref_products_manager.my_products_sprites[index] = textures[i];
					}
				}
				else
				{
					checker.Add(i.ToString(),0);
				}
			}
			
			for(int i = 0 ; i < total_images_now; i++)
			{
				if(checker[i.ToString()] == 1 && i <= total_images_before - 1)
				{
					// save image with same key name than the images saved
					string key = ref_products_manager.current_offers_view[index.ToString()]["key"][i];
					SaveTextureToFile(key,index);
					ref_products_manager.current_textures[index.ToString()][i] = textures[i];
				}
				if(i > total_images_before - 1)
				{
					// save image with same key name than the images saved
					string key = ref_products_manager.current_offers_view[index.ToString()]["key"][0];
					
					key.Remove(key.Length-1);
					key.Insert(key.Length,i.ToString());
					ref_products_manager.current_offers_view[index.ToString()]["key"].Add(key);
					ref_products_manager.current_textures[index.ToString()].Add(textures[i]);
					SaveTextureToFile(key,index);
					// CHECK IF THIS STRING MODIFICATION WORKS
				}
			}
		}
		catch
		{
			Debug.Log("SOMETHING WENT WRONG");
		}
	}


	public void clearCreation()
	{
		for(int i = 0; i < images_to_upload.Length; i++)
		{
			images_to_upload[i].texture = initial_sprite;
			textures[i] = null;
			//Destroy (textures[i]); // this will be uncomented when dealing with real images
		}
		total_pics_to_upload = 0;
		
		input_fields[0].text =  "Título...";
		input_fields[1].text = "Descripción...";
		input_fields[2].text = "Precio...";
		input_fields[3].text = "Email...";

		//if(tags_ready)tags_ready = false;
		if(images_ready)images_ready = false;
		if(fields_ready)fields_ready = false;
		if(ready_to_submit) ready_to_submit = false;
		edit_mode = false;
		closed = true;

		for(int i = 0; i < tag_display_image.Length;i++)
		{
			tag_display_image[i].SetActive(false);
		}
	}
	public void SaveTextureToFile(string fileName,int index)
	{
		byte [] bytes = null;
		if(Application.isMobilePlatform) bytes = textures[index].EncodeToJPG();
		if(Application.isEditor) bytes = ref_products_manager.sprites[index].EncodeToJPG();
		string path_image_to_upload = String.Concat (Application.persistentDataPath,String.Concat("/",String.Concat(fileName, ".jpg")));
		File.WriteAllBytes (path_image_to_upload, bytes);
		image_manager.images_to_upload.Add (fileName+".jpg");
	}

	public void ShowTags (List<string> tags)
	{
		for(int i = 0; i < tag_display_image.Length; i++)
		{
			tag_display_image[i].SetActive(false);
		}
		for(int i = 0; i < tags.Count; i++)
		{
			tag_display_image[i].SetActive(true);
			tag_display_image[i].transform.GetChild(0).GetComponent<Text>().text = tags[i];
		}
		tags_list = tags;
	}
}
