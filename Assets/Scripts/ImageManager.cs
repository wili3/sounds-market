using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Runtime;
using System.IO;
using System;
using Amazon.S3.Util;
using System.Collections.Generic;
using Amazon.CognitoIdentity;
using Amazon;
using UnityEngine.iOS;

public class ImageManager : MonoBehaviour {

	public string[] product_keys;
	public string[] product_tittles; 
	public string[] product_prices;

	public int[] widths;
	public int[] heights;

	public AmazonS3Client client;
	public int index;
	public bool downloading, uploading, downloading_detail_images, checkhash = false;

	public contentmanager content;
	public ProductsManager products_manager;
	public InfoView info_view;
	public TagViewController tag_view_controller;

	public List<string> images_to_upload;
	public Dictionary<string,string> product_detail_images = new Dictionary<string, string>();
	public UsersManager user_manager;

	public Hashtable products_table, my_products_table;
	public ArrayList products_to_set;
	// Use this for initialization
	void Start () {

		Debug.Log("Current user id: " + PlayerPrefs.GetString("user_id"));

		products_table = new Hashtable ();
		my_products_table = new Hashtable ();
		product_keys = new string[products_manager.total_products_current_view];
		product_tittles = new string[products_manager.total_products_current_view];
		product_prices = new string[products_manager.total_products_current_view];
		
		widths = new int[products_manager.total_products_current_view];
		heights = new int[products_manager.total_products_current_view];

		initialize_fake_data ();
		set_fake_data ();
		set_user_fake_data ();
		client = new AmazonS3Client ("AKIAIEXLBENWSMQ3WIQA", "0ukFG7rZ98Dul4UPXWTwT/5TxO/cBKmYkKUCXfLR", RegionEndpoint.EUWest1);
	}

	void Update()
	{
		if (checkhash) 
		{
			checkhash = false;
			ParseHashToDic();
		}

		if (uploading || downloading_detail_images)
			return;

		if (images_to_upload.Count > 0)
		{
			StartCoroutine (PostObject (images_to_upload [0]));
			uploading = true;
		}
		if(product_detail_images.Count > 0)
		{
			for(int i = 0; i < 4; i++)
			{
				if(product_detail_images.ContainsKey(i.ToString()))
				{
					StartCoroutine (GetObject_async_detail(product_detail_images[i.ToString()],i));
					i = 100;
					downloading_detail_images = true;
				}
			}
		}

	}

	public IEnumerator GetObject_async(string key)
	{
		client.GetObjectAsync("github-wili3-issues", "sounds-market/" + key, (responseObj) =>
        {
			string data = null;
			GetObjectResponse response = responseObj.Response;
			if (response.ResponseStream != null)
			{
				using (BinaryReader bReader = new BinaryReader(response.ResponseStream))
				{
					byte [] buffer = bReader.ReadBytes((int)response.ResponseStream.Length);
					StartCoroutine(createSprite(buffer));
				}
			}
		});
		yield return null;
	}
	private IEnumerator createSprite(byte[] buffer)
	{
		Texture2D tex = new Texture2D (widths [index], heights [index]);
		tex.LoadImage (buffer);
		//products_manager.main_sprites.Add (tex);
		products_manager.sprites.Add (tex);
		if(content.currentlyDisplayed(index) > -1 && downloading)
		{
			content.setImage(content.currentlyDisplayed(index),index);
		}

		downloading = false;
		index += 1;
		products_manager.ChangeContextToCurrent ();
		yield return null;
	}

	public IEnumerator PostObject(string fileName)
	{
		string filename = GetFileHelper ();
		var stream = new FileStream(Application.persistentDataPath + Path.DirectorySeparatorChar + fileName,
		                            FileMode.Open, FileAccess.Read, FileShare.Read);

		var request = new PostObjectRequest()
		{
			Bucket = "github-wili3-issues",
			Key = "sounds-market/" + fileName,
			InputStream = stream,
		};
		
		client.PostObjectAsync(request, (responseObj) =>
		                       {
			if (responseObj.Exception == null)
			{
				Debug.Log(string.Format("\nobject {0} posted to bucket {1}",
				                                 responseObj.Request.Key, responseObj.Request.Bucket));
				uploading = false;
				images_to_upload.RemoveAt(0);
			}
			else
			{
				uploading = false;
				Debug.Log("\nException while posting the result object");
				Debug.Log(string.Format("\n receieved error {0}",
				                                 responseObj.Response.HttpStatusCode.ToString()));
			}
		});
		yield return null;
	}

	public IEnumerator GetObject_async_detail(string key,int index)
	{
		client.GetObjectAsync("github-wili3-issues", "sounds-market/" + key, (responseObj) =>
		                      {
			string data = null;
			GetObjectResponse response = responseObj.Response;
			if (response.ResponseStream != null)
			{
				using (BinaryReader bReader = new BinaryReader(response.ResponseStream))
				{
					byte [] buffer = bReader.ReadBytes((int)response.ResponseStream.Length);
					StartCoroutine(createSprite_detail(buffer,index));
				}
			}
		});
		yield return null;
	}

	private IEnumerator createSprite_detail(byte[] buffer, int index)
	{
		products_manager.current_textures[info_view.current_index_offer.ToString()][index] = new Texture2D (int.Parse(products_manager.current_offers_view[info_view.current_index_offer.ToString()]["width"][index]), int.Parse(products_manager.current_offers_view[info_view.current_index_offer.ToString()]["height"][index]));
		products_manager.current_textures[info_view.current_index_offer.ToString()][index].LoadImage (buffer);

		downloading_detail_images = false;
		product_detail_images.Remove (index.ToString ());
		yield return null;
	}

	public void initialize_fake_data ()
	{
		product_keys [0] = "djset_001.jpg";
		product_keys [1] = "djset_2.png";
		product_keys [2] = "drums_1.jpg";
		product_keys [3] = "drums_2.jpg";
		product_keys [4] = "flute_1.jpg";
		product_keys [5] = "flute_2.jpg";
		product_keys [6] = "guitar_1.jpg";
		product_keys [7] = "guitar_2.jpg";
		product_keys [8] = "piano_1.JPG";
		product_keys [9] = "piano_2.JPG";
		product_keys [10] = "trumpet_11.jpg";
		product_keys [11] = "trumpet_2.jpg";
		product_keys [12] = "ukelele_2.jpg";
		product_keys [13] = "ukelele1.jpg";
		product_keys [14] = "accessories_1.png";

		product_tittles [0] = "Dj set";
		product_tittles [1] = "Full Dj set";
		product_tittles [2] = "Old drums";
		product_tittles [3] = "Drums";
		product_tittles [4] = "Flute";
		product_tittles [5] = "Metalic flute";
		product_tittles [6] = "Electric guitar";
		product_tittles [7] = "Yamaha guitar";
		product_tittles [8] = "Classic piano";
		product_tittles [9] = "Classic white piano";
		product_tittles [10] = "Classic trumpet";
		product_tittles [11] = "Silver trumpet";
		product_tittles [12] = "Blue ukelele";
		product_tittles [13] = "Ukelele";
		product_tittles [14] = "Accessories";


		for(int i = 0; i < product_prices.Length; i++)
		{
			product_prices[i] = UnityEngine.Random.Range(100.0f,1200.0f).ToString();
		}

		widths [0] = 576;
		heights [0] = 432;

		widths [1] = 620;
		heights [1] = 406;

		widths [2] = 2048;
		heights [2] = 1529;
		
		widths [3] = 1024;
		heights [3] = 768;
		
		widths [4] = 354;
		heights [4] = 209;
		
		widths [5] = 2033;
		heights [5] = 1395;
	
		widths [6] = 1000;
		heights [6] = 662;

		widths [7] = 1000;
		heights [7] = 853;

		widths [8] = 1600;
		heights [8] = 1200;

		widths [9] = 640;
		heights [9] = 480;

		widths [10] = 500;
		heights [10] = 493;

		widths [11] = 484;
		heights [11] = 329;

		widths [12] = 771;
		heights [12] = 858;

		widths [13] = 200;
		heights [13] = 436;

		widths [14] = 520;
		heights [14] = 302;
	}

	public void set_fake_data ()
	{
		StartCoroutine(Requester.getproducts (User.current_url(), "api/products", null));
		/*
		for(int i = 0; i < product_keys.Length; i++)
		{
			Dictionary<string,List<string>> dic_inside = new Dictionary<string, List<string>>();

			string email = "guillempsx2@hotmail.com";
			string tittle = product_tittles[i];
			string desc = "Hola soy una decripción, con \n saltos de linea i tal";
			string seller = "Guillem";
			string price = product_prices[i];
			string width = widths[i].ToString();
			string height = heights[i].ToString();
			string p_key = product_keys[i];
			string num_of_pics = "1";
			string user_id = "21";

			List<string> num_of_pics_list = new List<string>();
			num_of_pics_list.Add (num_of_pics);
			dic_inside.Add ("num_of_pics", num_of_pics_list);

			List<string> tags = new List<string>();
			tags.Add("guitarra");
			tags.Add("classica");

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
			width_list.Add(width.ToString());
			dic_inside.Add("width",width_list);

			List<string> height_list = new List<string>();
			height_list.Add(height);
			dic_inside.Add("height",height_list);

			List<string> key_list = new List<string>();
			key_list.Add(p_key);
			dic_inside.Add("key",key_list);

			dic_inside.Add("tags",tags);

			List<string> user_id_list = new List<string>();
			user_id_list.Add(user_id);
			dic_inside.Add("user_id",user_id_list);

			products_manager.current_offers_view.Add(i.ToString(),dic_inside);

			//if(i == 0)ParseDicToHash(dic_inside);
		}*/
	}

	public void set_user_fake_data ()
	{
		Dictionary<string,List<string>> dic_inside = new Dictionary<string, List<string>>();
		string user_name = "Guillem San Nicolàs";
		int user_rates = 112;
		int user_average = 3;
		string user_mail = "guillempsx2@hotmail.com";

		List<string> user_name_list = new List<string> ();
		user_name_list.Add (user_name);
		dic_inside.Add ("user_name", user_name_list);

		List<string> user_rates_list = new List<string> ();
		user_rates_list.Add (user_rates.ToString ());
		dic_inside.Add ("user_rates", user_rates_list);

		List<string> user_averages_list = new List<string> ();
		user_averages_list.Add (user_average.ToString ());
		dic_inside.Add ("user_averages", user_averages_list);

		List<string> user_mail_list = new List<string> ();
		user_mail_list.Add (user_mail);
		dic_inside.Add ("user_mail", user_mail_list);

		user_manager.other_users_info.Add (21, dic_inside);
	}

	private string GetFileHelper ()
	{
		var fileName = images_to_upload[0];
		
		if (!File.Exists(Application.persistentDataPath + Path.DirectorySeparatorChar + fileName))
		{
			var streamReader = File.CreateText(Application.persistentDataPath + Path.DirectorySeparatorChar + fileName);
			streamReader.WriteLine("This is a sample s3 file uploaded from unity s3 sample");
			streamReader.Close();
		}
		return fileName;
	}

	public void ParseDicToHash(Dictionary<string,List<string>> dic_inside)
	{
		Hashtable table = new Hashtable();
		string title = dic_inside ["tittle"][0];
		table.Add ("title", title);

		string description = dic_inside ["desc"][0];
		table.Add ("description", description);

		string price = dic_inside["price"][0];
		table.Add ("price", price);

		ArrayList tags = new ArrayList ();

		for (int i = 0; i < tag_view_controller.dic.Count; i++) 
		{
			tags.Add(tag_view_controller.convert_inverse_dic[tag_view_controller.dic[i]]);
			Debug.Log(tag_view_controller.convert_inverse_dic[tag_view_controller.dic[i]]);
		}
		//here will have to go something that matches the tags with the categories provided and then sets it to this integer array
		table.Add ("category_ids", tags);

		ArrayList images_list = new ArrayList ();
		for (int i = 0; i < dic_inside["width"].Count; i++) 
		{
			Hashtable images_table = new Hashtable ();
			images_table.Add ("key",dic_inside["key"][i]);
			Debug.Log(dic_inside["width"].Count+  "     i: " + i.ToString());
			images_table.Add ("width",  dic_inside["width"][i]);
			images_table.Add ("height", dic_inside["height"][i]);
			images_list.Add (images_table);
		}

		table.Add ("images", images_list);

		Hashtable table_parent = new Hashtable ();
		table_parent.Add ("product", table);

		StartCoroutine(Requester.postproduct(User.current_url(),"api/products",table_parent, dic_inside));
		List<string> user_id_hash_list = table ["user_id"] as List<string>;

	}

	public void ParseDicToHashEdit(Dictionary<string,List<string>> dic_inside, bool sold)
	{
		Hashtable table = new Hashtable();
		string title = dic_inside ["tittle"][0];
		table.Add ("title", title);

		if (sold) {
			table.Add("sold",true);
		}

		string description = dic_inside ["desc"][0];
		table.Add ("description", description);
		
		string price = dic_inside["price"][0];
		table.Add ("price", price);
		
		ArrayList tags = new ArrayList ();
		
		for (int i = 0; i < tag_view_controller.dic.Count; i++) 
		{
			tags.Add(tag_view_controller.convert_inverse_dic[tag_view_controller.dic[i]]);
			Debug.Log(tag_view_controller.convert_inverse_dic[tag_view_controller.dic[i]]);
		}
		//here will have to go something that matches the tags with the categories provided and then sets it to this integer array
		table.Add ("category_ids", tags);
		
		ArrayList images_list = new ArrayList ();
		for (int i = 0; i < dic_inside["width"].Count; i++) 
		{
			Hashtable images_table = new Hashtable ();
			images_table.Add ("key",dic_inside["key"][i]);
			Debug.Log(dic_inside["width"].Count+  "     i: " + i.ToString());
			images_table.Add ("width",  dic_inside["width"][i]);
			images_table.Add ("height", dic_inside["height"][i]);
			images_list.Add (images_table);
		}
		
		table.Add ("images", images_list);
		
		Hashtable table_parent = new Hashtable ();
		table_parent.Add ("product", table);
		
		StartCoroutine(Requester.editproduct(User.current_url(),"api/products/" + dic_inside["id"][0],table_parent));
		List<string> user_id_hash_list = table ["user_id"] as List<string>;
		
	}

	public void ParseHashToDic()
	{
		products_manager.main_offers.Clear ();
		products_manager.main_sprites.Clear ();
		products_manager.main_textures.Clear ();
		products_manager.ChangeContextToMain ();
		Dictionary<string,List<string>> dic_inside = new Dictionary<string, List<string>>();
		/*
		dic_inside.Add ("num_of_pics", num_of_pics_list);
*/
		products_to_set = products_table["products"] as ArrayList;
		products_manager.total_products_current_view = products_to_set.Count; 


		for(int i = 0; i < products_to_set.Count; i++)
		{
			dic_inside = new Dictionary<string, List<string>>();
			Hashtable table = products_to_set[i] as Hashtable;

			Debug.Log(table.Count);
			Debug.Log("user id: " + table["user_id"].ToString());	

			List<string> title_list = new List<string>();
			title_list.Add(table["title"].ToString());
			dic_inside.Add("tittle", title_list);

			List<string> email_list = new List<string>();
			try
			{
				email_list.Add(table["email"].ToString());
			}
			catch
			{
				email_list.Add("guillempsx2@hotmail.com");
			}
			dic_inside.Add("email",email_list);

			List<string> user_id_list = new List<string>();
			user_id_list.Add(table["user_id"].ToString());
			dic_inside.Add("user_id",user_id_list);

			List<string> desc_list = new List<string>();
			if(table["description"]!= null){
				desc_list.Add(table["description"].ToString());
			} else {
				desc_list.Add("No descripción.");
			}
			dic_inside.Add("desc", desc_list);

			List<string> id_list = new List<string>();
			id_list.Add(table["id"].ToString());
			dic_inside.Add("id", id_list);

			Debug.Log("set all data *******");

			List<string> price_list = new List<string>();
			try{
				price_list.Add(table["price"].ToString());
			}
			catch{
				price_list.Add(100.ToString());
			}
			dic_inside.Add("price",price_list);

			List<string> tags = new List<string>();
			ArrayList tags_list = (ArrayList)table["category_ids"];
			for(int j = 0; j < tags_list.Count; j++)
			{
				tags.Add((string)tags_list[j]);
			}

			dic_inside.Add("tags",tags);

			List<string> seller_list = new List<string>();
			seller_list.Add("Guillem Samni");
			dic_inside.Add("seller",seller_list);

			Debug.Log("set all data *******");

			List<string> width_list = new List<string>();
			List<string> height_list = new List<string>();
			List<string> key_list = new List<string>();
			List<string> num_of_pics = new List<string>();

			ArrayList images_array = table["images"] as ArrayList;
			num_of_pics.Add(images_array.Count.ToString());

			for(int j = 0; j < images_array.Count; j++)
			{
				Hashtable images_table = images_array[j] as Hashtable;
				key_list.Add(images_table["key"].ToString());
				width_list.Add(images_table["width"].ToString());
				height_list.Add(images_table["height"].ToString());
			}

			dic_inside.Add("width",width_list);
			dic_inside.Add("height",height_list);
			dic_inside.Add("key",key_list);
			dic_inside.Add("num_of_pics",num_of_pics);

			try{
				if(table.ContainsKey("lat"))
				{
					List<string> lat_list = new List<string>();
					lat_list.Add(table["lat"].ToString());

					List<string> lng_list = new List<string>();
					lng_list.Add(table["lng"].ToString());
					Debug.Log("adding LAT");

					dic_inside.Add("lat",lat_list);
					dic_inside.Add("lng",lng_list);
				}
			}
			catch{

			}
			/*for(int j = 0; j < key_list.Count; j++)
			{

			}*/

			products_manager.current_offers_view.Add(products_manager.current_offers_view.Count.ToString(),dic_inside);
		}
		products_manager.ChangeContextToMain ();
		products_manager.ChangeContextToCurrent ();
		RequestMyProducts ();
	}
	public void RequestMyProducts()
	{
		if(products_manager.my_offers.Count == 0)StartCoroutine(Requester.getmyproducts(User.current_url(),"api/products/search?user_id=" + PlayerPrefs.GetString("user_id"),null));
	}
	public void ParseHashToDicMyProducts()
	{
		Dictionary<string,List<string>> dic_inside = new Dictionary<string, List<string>>();
		/*
		dic_inside.Add ("num_of_pics", num_of_pics_list);
*/
		products_to_set = my_products_table["products"] as ArrayList;
		
		
		for(int i = 0; i < products_to_set.Count; i++)
		{
			dic_inside = new Dictionary<string, List<string>>();
			Hashtable table = products_to_set[i] as Hashtable;
			
			Debug.Log(table.Count);
			Debug.Log("user id: " + table["user_id"].ToString());	
			
			List<string> title_list = new List<string>();
			title_list.Add(table["title"].ToString());
			dic_inside.Add("tittle", title_list);
			
			List<string> email_list = new List<string>();
			try
			{
				email_list.Add(table["email"].ToString());
			}
			catch
			{
				email_list.Add("guillempsx2@hotmail.com");
			}
			dic_inside.Add("email",email_list);
			
			List<string> user_id_list = new List<string>();
			user_id_list.Add(table["user_id"].ToString());
			dic_inside.Add("user_id",user_id_list);
			
			List<string> desc_list = new List<string>();
			desc_list.Add(table["description"].ToString());
			dic_inside.Add("desc", desc_list);

			List<string> id_list = new List<string>();
			id_list.Add(table["id"].ToString());
			dic_inside.Add("id", id_list);

			Debug.Log("set all data *******");
			
			List<string> price_list = new List<string>();
			try{
				price_list.Add(table["price"].ToString());
			}
			catch{
				price_list.Add(100.ToString());
			}
			dic_inside.Add("price",price_list);
			
			List<string> tags = new List<string>();
			ArrayList tags_list = (ArrayList)table["category_ids"];
			for(int j = 0; j < tags_list.Count; j++)
			{
				Debug.Log("TAG: "+ (string)tags_list[j]);
				tags.Add((string)tags_list[j]);
			}

			dic_inside.Add("tags",tags);
			
			List<string> seller_list = new List<string>();
			seller_list.Add("Guillem Samni");
			dic_inside.Add("seller",seller_list);
			
			Debug.Log("set all data *******");
			
			List<string> width_list = new List<string>();
			List<string> height_list = new List<string>();
			List<string> key_list = new List<string>();
			List<string> num_of_pics = new List<string>();
			
			ArrayList images_array = table["images"] as ArrayList;
			num_of_pics.Add(images_array.Count.ToString());
			
			for(int j = 0; j < images_array.Count; j++)
			{
				Hashtable images_table = images_array[j] as Hashtable;
				key_list.Add(images_table["key"].ToString());
				width_list.Add(images_table["width"].ToString());
				height_list.Add(images_table["height"].ToString());
			}
			
			dic_inside.Add("width",width_list);
			dic_inside.Add("height",height_list);
			dic_inside.Add("key",key_list);
			dic_inside.Add("num_of_pics",num_of_pics);
			
			/*for(int j = 0; j < key_list.Count; j++)
			{

			}*/
			
			products_manager.my_offers.Add(products_manager.my_offers.Count.ToString(),dic_inside);
		}
	}
}
