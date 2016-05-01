using UnityEngine;
using System.Collections;

public class User : MonoBehaviour {

	public string user_name;
	public string email;

	public static string staging_url = "http://sm-staging.victorblasco.me/";
	public static string local_url = "http://sounds-market.dev/";
	public static bool local = false;
	public bool is_local;
	// Use this for initialization
	void Start () {
		local = is_local;
		DontDestroyOnLoad (this.gameObject);
		if (CheckToken()) 
		{
			if(IsTokenValid())
			{
				Application.LoadLevel (1);
				Debug.Log("ALREADY HAVE TOKEN : " + PlayerPrefs.GetString ("access_token"));
				return;
			}
		}
	}

	public static string current_url()
	{
		if (local) {
			return local_url;
		}
		else {
			return staging_url;
		}
	}

	public void SaveToken(string token)
	{
		PlayerPrefs.SetString ("access_token", token);
	}

	public void SaveUserID(string user_id)
	{
		PlayerPrefs.SetString ("user_id", user_id);
		Debug.Log ("SAVED " + user_id);
	}

	bool CheckToken()
	{
		return PlayerPrefs.HasKey("access_token");
	}

	bool IsTokenValid()
	{
		// Here will make a request to check if the current token is valid
		return true;
	}

	public void SaveUserName(string _user_name)
	{
		user_name = _user_name;
	}

	public static string stagingURL()
	{
		return "http://sm-staging.victorblasco.me/";
	}

	public static string localURL()
	{
		return "http://sounds-market.dev/";
	}
}
