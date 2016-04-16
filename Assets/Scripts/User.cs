using UnityEngine;
using System.Collections;

public class User : MonoBehaviour {

	public string user_name;
	public string email;


	// Use this for initialization
	void Start () {
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

	public void SaveToken(string token)
	{
		PlayerPrefs.SetString ("access_token", token);
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
}
