using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Facebook.Unity;
using Facebook.MiniJSON;
using Ionic;

public class Requester : MonoBehaviour {

	public static void post(string url, string endpoint, Hashtable parameters)
	{
		HTTP.Request request = new HTTP.Request("post", url + endpoint,parameters);
		request.synchronous = true;
		request.SetHeader ("Content-Type", "application/json");
		request.SetHeader ("X-Auth-Token", PlayerPrefs.GetString("access_token"));
		if (PlayerPrefs.GetString ("Latitude") != null) {
			request.SetHeader ("X-Lat", PlayerPrefs.GetString ("Latitude"));
			request.SetHeader ("X-Lng", PlayerPrefs.GetString ("Longitude"));
		}

		request.Send((request_obj)=>{
			Debug.Log("REQUEST MADE" + request_obj.response.Text);

			bool result = false;

			Hashtable table = (Hashtable)JSON.JsonDecode(request.response.Text, ref result );

			if(!result)
			{
				//here will go a retry
				Debug.Log("CANNOT PARSE JSON");
			}
		});
	}

	public static void get(string url, string endpoint, Hashtable parameters = null)
	{
		HTTP.Request request = new HTTP.Request("get", url + endpoint,parameters);
		if (parameters != null) {
			request = new HTTP.Request ("get", url + endpoint, parameters);
		} else {
			request = new HTTP.Request ("get", url + endpoint);
		}

		
		request.SetHeader ("Content-Type", "application/json");
		request.SetHeader ("X-Auth-Token", PlayerPrefs.GetString("access_token"));
		if (PlayerPrefs.GetString ("Latitude") != null) {
			request.SetHeader ("X-Lat", PlayerPrefs.GetString ("Latitude"));
			request.SetHeader ("X-Lng", PlayerPrefs.GetString ("Longitude"));
		}
		request.SetHeader ("X-Lng", PlayerPrefs.GetString ("Longitude"));

		request.synchronous = true;
		request.Send((request_obj)=>{
			Debug.Log("User info: " + request_obj.response.Text);
		});
	}

	public static IEnumerator postsessions(string url, string endpoint, Hashtable parameters)
	{
		HTTP.Request request = new HTTP.Request("post", url + endpoint,parameters);
		request.synchronous = true;
		request.SetHeader ("Content-Type", "application/json");
		if (PlayerPrefs.GetString ("Latitude") != null) {
			request.SetHeader ("X-Lat", PlayerPrefs.GetString ("Latitude"));
			request.SetHeader ("X-Lng", PlayerPrefs.GetString ("Longitude"));
		}
		request.SetHeader ("X-Lng", PlayerPrefs.GetString ("Longitude"));

		request.Send((request_obj)=>{
			Debug.Log("REQUEST MADE:   " + request_obj.protocol.ToString());
			Debug.Log("REQUEST MADE:   " + request_obj.response.status.ToString());

			bool result = false;
			
			Hashtable table = (Hashtable)JSON.JsonDecode(request.response.Text, ref result );

			Debug.Log("REQUEST MADE user id:   " + table["user_id"] as String);

			User user = GameObject.FindGameObjectWithTag("Player").GetComponent<User>();
			user.SaveToken(table["auth_token"] as String);
			int user_id = (int)table["user_id"];
			user.SaveUserID(user_id.ToString());

			Application.LoadLevel(1);
			if(!result)
			{
				//here will go a retry
				Debug.Log("CANNOT PARSE JSON");
			}
		});
		yield return request;
	}

	public static IEnumerator postproduct(string url, string endpoint, Hashtable parameters, Dictionary<string,List<string>> dic_inside)
	{
		HTTP.Request request = new HTTP.Request("post", url + endpoint,parameters);
		request.synchronous = true;
		request.SetHeader ("Content-Type", "application/json");
		request.SetHeader ("X-Auth-Token", PlayerPrefs.GetString("access_token"));
		if (PlayerPrefs.GetString ("Latitude") != null) {
			request.SetHeader ("X-Lat", PlayerPrefs.GetString ("Latitude"));
			request.SetHeader ("X-Lng", PlayerPrefs.GetString ("Longitude"));
		}
		request.SetHeader ("X-Lng", PlayerPrefs.GetString ("Longitude"));

		request.Send((request_obj)=>{
			Debug.Log("REQUEST MADE:   " + request_obj.response.Text.ToString());
			bool result = false;
			
			Hashtable table = (Hashtable)JSON.JsonDecode(request.response.Text, ref result );

			if(!result)
			{
				//here will go a retry
				Debug.Log("CANNOT PARSE JSON");
			}
			OfferCreationViewController oc_view_controller = GameObject.FindGameObjectWithTag("OCViewController").GetComponent<OfferCreationViewController>();
			oc_view_controller.SubimtAfterPost(dic_inside);
		});
		yield return request;
	}
	// this 2 voids must return a Hashtable and if not succeded, retry the current request

	public static IEnumerator getproducts(string url, string endpoint, Hashtable parameters = null)
	{
		HTTP.Request request = new HTTP.Request("get", url + endpoint,parameters);
		if (parameters != null) {
			request = new HTTP.Request ("get", url + endpoint, parameters);
		} else {
			request = new HTTP.Request ("get", url + endpoint);
		}
		
		
		request.SetHeader ("Content-Type", "application/json");
		request.SetHeader ("X-Auth-Token", PlayerPrefs.GetString("access_token"));
		if (PlayerPrefs.GetString ("Latitude") != null) {
			request.SetHeader ("X-Lat", PlayerPrefs.GetString ("Latitude"));
			request.SetHeader ("X-Lng", PlayerPrefs.GetString ("Longitude"));
		}
		request.SetHeader ("X-Lng", PlayerPrefs.GetString ("Longitude"));
		
		request.synchronous = true;
		request.Send((request_obj)=>{
			Debug.Log("REQUEST MADE" + request_obj.response.Text);
			Debug.Log("REQUEST MADE" + request_obj.response.status);

			bool result = false;
			
			Hashtable table = (Hashtable)JSON.JsonDecode(request.response.Text, ref result );
			

			ImageManager image_manager = GameObject.FindGameObjectWithTag("ImageManager").GetComponent<ImageManager>();
			image_manager.products_table = table;
			image_manager.checkhash = true;

			if(!result)
			{
				//here will go a retry
				Debug.Log("CANNOT PARSE JSON");
				return;
			}
		});
		yield return request;
	}

	public static IEnumerator getmyproducts(string url, string endpoint, Hashtable parameters = null)
	{
		HTTP.Request request = new HTTP.Request("get", url + endpoint,parameters);
		if (parameters != null) {
			request = new HTTP.Request ("get", url + endpoint, parameters);
		} else {
			request = new HTTP.Request ("get", url + endpoint);
		}
		
		
		request.SetHeader ("Content-Type", "application/json");
		request.SetHeader ("X-Auth-Token", PlayerPrefs.GetString("access_token"));
		if (PlayerPrefs.GetString ("Latitude") != null) {
			request.SetHeader ("X-Lat", PlayerPrefs.GetString ("Latitude"));
			request.SetHeader ("X-Lng", PlayerPrefs.GetString ("Longitude"));
		}
		request.SetHeader ("X-Lng", PlayerPrefs.GetString ("Longitude"));
		
		request.synchronous = true;
		request.Send((request_obj)=>{
			Debug.Log("REQUEST MADE" + request_obj.response.Text);
			Debug.Log("REQUEST MADE" + request_obj.response.status);
			
			bool result = false;
			
			Hashtable table = (Hashtable)JSON.JsonDecode(request.response.Text, ref result );
			
			if(!result)
			{
				//here will go a retry
				Debug.Log("CANNOT PARSE JSON");
				return;
			}
			else
			{
				ImageManager image_manager = GameObject.FindGameObjectWithTag("ImageManager").GetComponent<ImageManager>();
				image_manager.my_products_table = table;
				image_manager.ParseHashToDicMyProducts();
			}
		});
		yield return request;
	}

	public static IEnumerator getmyuser(string url, string endpoint, Hashtable parameters = null)
	{
		HTTP.Request request = new HTTP.Request("get", url + endpoint,parameters);
		if (parameters != null) {
			request = new HTTP.Request ("get", url + endpoint, parameters);
		} else {
			request = new HTTP.Request ("get", url + endpoint);
		}
		
		
		request.SetHeader ("Content-Type", "application/json");
		request.SetHeader ("X-Auth-Token", PlayerPrefs.GetString("access_token"));
		if (PlayerPrefs.GetString ("Latitude") != null) {
			request.SetHeader ("X-Lat", PlayerPrefs.GetString ("Latitude"));
			request.SetHeader ("X-Lng", PlayerPrefs.GetString ("Longitude"));
		}
		request.SetHeader ("X-Lng", PlayerPrefs.GetString ("Longitude"));
		
		request.synchronous = true;
		request.Send((request_obj)=>{
			Debug.Log("REQUEST MADE" + request_obj.response.Text);
			Debug.Log("REQUEST MADE" + request_obj.response.status);
			
			bool result = false;
			
			Hashtable table = (Hashtable)JSON.JsonDecode(request.response.Text, ref result );
			Hashtable table_child = (Hashtable)table["user"];
			if(!result)
			{
				//here will go a retry
				Debug.Log("CANNOT PARSE JSON");
				return;
			}
			else
			{
				UsersManager users_manager = GameObject.FindGameObjectWithTag("ProductsManager").GetComponent<UsersManager>();
				users_manager.ParseHashToDicMyUser(table);
				User user = GameObject.FindGameObjectWithTag("Player").GetComponent<User>();
				user.SaveUserPicUrl(table_child["facebook_image_url"] as String);
				if(table.ContainsKey("facebook_image_url"))
				{
					Debug.Log ( "PIC URL : " + (string)table_child["facebook_image_url"]);
				}
			}
		});
		yield return request;
	}

	public static void getuser(string url, string endpoint, Hashtable parameters = null)
	{
		HTTP.Request request = new HTTP.Request("get", url + endpoint,parameters);
		if (parameters != null) {
			request = new HTTP.Request ("get", url + endpoint, parameters);
		} else {
			request = new HTTP.Request ("get", url + endpoint);
		}
		
		
		request.SetHeader ("Content-Type", "application/json");
		request.SetHeader ("X-Auth-Token", PlayerPrefs.GetString("access_token"));
		if (PlayerPrefs.GetString ("Latitude") != null) {
			request.SetHeader ("X-Lat", PlayerPrefs.GetString ("Latitude"));
			request.SetHeader ("X-Lng", PlayerPrefs.GetString ("Longitude"));
		}
		request.SetHeader ("X-Lng", PlayerPrefs.GetString ("Longitude"));
		
		request.synchronous = true;
		request.Send ();
		Debug.Log("REQUEST MADE" + request.response.Text);
		Debug.Log("REQUEST MADE" + request.response.status);
		
		bool result = false;
		
		Hashtable table = (Hashtable)JSON.JsonDecode(request.response.Text, ref result );
		
		if(!result)
		{
			//here will go a retry
			Debug.Log("CANNOT PARSE JSON");
			return;
		}
		else
		{
			UsersManager users_manager = GameObject.FindGameObjectWithTag("ProductsManager").GetComponent<UsersManager>();
			users_manager.ParseHashToDicUser(table);
		}
	}

	public static IEnumerator getcategories(string url, Hashtable parameters = null)
	{
		HTTP.Request request = new HTTP.Request("get", url + "api/categories",parameters);
		if (parameters != null) {
			request = new HTTP.Request ("get", url + "api/categories", parameters);
		} else {
			request = new HTTP.Request ("get", url + "api/categories");
		}
		
		
		request.SetHeader ("Content-Type", "application/json");
		if (PlayerPrefs.GetString ("Latitude") != null) {
			request.SetHeader ("X-Lat", PlayerPrefs.GetString ("Latitude"));
			request.SetHeader ("X-Lng", PlayerPrefs.GetString ("Longitude"));
		}
		request.SetHeader ("X-Lng", PlayerPrefs.GetString ("Longitude"));
		
		request.synchronous = true;
		request.Send((request_obj)=>{
			Debug.Log("REQUEST MADE" + request_obj.response.Text);
			Debug.Log("REQUEST MADE" + request_obj.response.status);
			
			bool result = false;
			
			Hashtable table = (Hashtable)JSON.JsonDecode(request.response.Text, ref result );
			
			if(!result)
			{
				//here will go a retry
				Debug.Log("CANNOT PARSE JSON");
				return;
			}
			else
			{
				GameObject.FindGameObjectWithTag("TagView").GetComponent<TagViewController>().LoadCategories(table);
			}
		});
		yield return request;
	}

	public static IEnumerator rateuser(string url, string endpoint, Hashtable parameters)
	{
		HTTP.Request request = new HTTP.Request("post", url + endpoint,parameters);
		request.synchronous = true;
		request.SetHeader ("Content-Type", "application/json");
		request.SetHeader ("X-Auth-Token", PlayerPrefs.GetString("access_token"));
		if (PlayerPrefs.GetString ("Latitude") != null) {
			request.SetHeader ("X-Lat", PlayerPrefs.GetString ("Latitude"));
			request.SetHeader ("X-Lng", PlayerPrefs.GetString ("Longitude"));
		}
		request.SetHeader ("X-Lng", PlayerPrefs.GetString ("Longitude"));

		request.Send((request_obj)=>{
			Debug.Log("REQUEST MADE:   " + request_obj.response.Text.ToString());
			bool result = false;
			
			Hashtable table = (Hashtable)JSON.JsonDecode(request.response.Text, ref result );
			
			if(!result)
			{
				//here will go a retry
				Debug.Log("CANNOT PARSE JSON");
			}
			GameObject.FindGameObjectWithTag("UserView").GetComponent<UserView>().DisableStars();
		});
		yield return request;
	}

	public static IEnumerator editproduct(string url, string endpoint, Hashtable parameters)
	{
		HTTP.Request request = new HTTP.Request("put", url + endpoint,parameters);
		request.synchronous = true;
		request.SetHeader ("Content-Type", "application/json");
		request.SetHeader ("X-Auth-Token", PlayerPrefs.GetString("access_token"));
		if (PlayerPrefs.GetString ("Latitude") != null) {
			request.SetHeader ("X-Lat", PlayerPrefs.GetString ("Latitude"));
			request.SetHeader ("X-Lng", PlayerPrefs.GetString ("Longitude"));
		}
		request.SetHeader ("X-Lng", PlayerPrefs.GetString ("Longitude"));

		request.Send((request_obj)=>{
			Debug.Log("REQUEST MADE:   " + request_obj.response.Text.ToString());
			bool result = false;
			
			Hashtable table = (Hashtable)JSON.JsonDecode(request.response.Text, ref result );
			
			if(!result)
			{
				//here will go a retry
				Debug.Log("CANNOT PARSE JSON");
			}

		});
		yield return request;
	}

	public static IEnumerator edituser(string url, string endpoint, Hashtable parameters)
	{
		HTTP.Request request = new HTTP.Request("put", url + endpoint,parameters);
		request.synchronous = true;
		request.SetHeader ("Content-Type", "application/json");
		request.SetHeader ("X-Auth-Token", PlayerPrefs.GetString("access_token"));
		if (PlayerPrefs.GetString ("Latitude") != null) {
			request.SetHeader ("X-Lat", PlayerPrefs.GetString ("Latitude"));
			request.SetHeader ("X-Lng", PlayerPrefs.GetString ("Longitude"));
		}
		request.SetHeader ("X-Lng", PlayerPrefs.GetString ("Longitude"));

		request.Send((request_obj)=>{
			Debug.Log("REQUEST MADE:   " + request_obj.response.Text.ToString());
			bool result = false;
			
			Hashtable table = (Hashtable)JSON.JsonDecode(request.response.Text, ref result );
			
			if(!result)
			{
				//here will go a retry
				Debug.Log("CANNOT PARSE JSON");
			}
			
		});
		yield return request;
	}
}
