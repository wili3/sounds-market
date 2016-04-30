using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Facebook.Unity;
using Facebook.MiniJSON;
using Ionic;

public class FacebookInitializer : MonoBehaviour {

	public bool called_once = false;
	public string result_string;
	public User user;
	// Use this for initialization
	void Start () {
		PlayerPrefs.DeleteAll ();
		FB.Init ("750504335081382");
		DontDestroyOnLoad (this.gameObject);
		user = GameObject.FindGameObjectWithTag ("Player").GetComponent<User>();
	}

	public void CallFBLogin()
	{
		FB.LogInWithReadPermissions(new List<string>() { "public_profile", "email", "user_friends" }, this.HandleResult);
	}

	protected void HandleResult(IResult result)
	{
		Debug.Log("LOGGED IN" + FB.AppId);

		IDictionary dic = (IDictionary)Json.Deserialize (result.RawResult);
		Debug.Log(result.RawResult);
		string token = dic ["access_token"] as String;

		Debug.Log (token.ToString ());

		if (token == null) {
			return;
		}

		Hashtable table = new Hashtable ();
		Hashtable table_parent = new Hashtable ();
		table.Add ("access_token", dic ["access_token"]);
		table_parent.Add ("session", table);

		StartCoroutine(Requester.postsessions (User.local_url, "api/sessions", table_parent)); // works on editor

		//user.SaveToken (token);
		// the token that i want to save will have to be saved in other place
		return;
	}
}
