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
		//PlayerPrefs.DeleteAll ();
		StartCoroutine (GetLocation ());
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

	public void SaveUserPicUrl(string url)
	{
		LoadUserPic load_user_pic = GameObject.FindGameObjectWithTag ("LoadUserPic").GetComponent<LoadUserPic> ();
		StartCoroutine (load_user_pic.GetPic (url));
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

	IEnumerator GetLocation()
	{
		// First, check if user has location service enabled
		if (!Input.location.isEnabledByUser)
			yield break;
		
		// Start service before querying location
		Input.location.Start();
		
		// Wait until service initializes
		int maxWait = 20;
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
		{
			yield return new WaitForSeconds(1);
			maxWait--;
		}
		
		// Service didn't initialize in 20 seconds
		if (maxWait < 1)
		{
			print("Timed out");
			yield break;
		}
		
		// Connection has failed
		if (Input.location.status == LocationServiceStatus.Failed)
		{
			print("Unable to determine device location");
			yield break;
		}
		else
		{
			// Access granted and location value could be retrieved
			print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
			PlayerPrefs.SetString("Latitude",Input.location.lastData.latitude.ToString());
			PlayerPrefs.SetString("Longitude",Input.location.lastData.longitude.ToString());
		}
		
		// Stop service if there is no need to query location updates continuously
		Input.location.Stop();
	}
}
