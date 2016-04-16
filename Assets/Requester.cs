﻿using System;
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

		request.synchronous = true;
		request.Send((request_obj)=>{
			Debug.Log("REQUEST MADE" + request_obj.response.Text);
		});
	}

	public static IEnumerator postsessions(string url, string endpoint, Hashtable parameters)
	{
		HTTP.Request request = new HTTP.Request("post", url + endpoint,parameters);
		request.synchronous = true;
		request.SetHeader ("Content-Type", "application/json");
		request.Send((request_obj)=>{
			Debug.Log("REQUEST MADE:   " + request_obj.protocol.ToString());
			Debug.Log("REQUEST MADE:   " + request_obj.response.status.ToString());

			bool result = false;
			
			Hashtable table = (Hashtable)JSON.JsonDecode(request.response.Text, ref result );

			Debug.Log("REQUEST MADE:   " + table["auth_token"] as String);

			User user = GameObject.FindGameObjectWithTag("Player").GetComponent<User>();
			user.SaveToken(table["auth_token"] as String);
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
			ImageManager image_manager = GameObject.FindGameObjectWithTag("ImageManager").GetComponent<ImageManager>();
			image_manager.products_table = table;
			image_manager.checkhash = true;
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
}
