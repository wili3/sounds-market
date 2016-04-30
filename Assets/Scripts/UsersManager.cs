using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class UsersManager : MonoBehaviour {

	public Texture2D user_profile_pic;
	public List<Texture2D> user_pics;

	public Dictionary<string, List<string>> my_user_info  = new Dictionary<string, List<string>>();
	public Dictionary<int,Dictionary<string,List<string>>> other_users_info = new Dictionary<int, Dictionary<string, List<string>>>();

	// Use this for initialization
	void Start () {
		StartCoroutine(Requester.getmyuser(User.local_url,"api/users/" + PlayerPrefs.GetString("user_id"),null));
		//first create my user info
		//then i will have to initialize the user array and when the user taps the button ver perfil assign the user to the list where the offer tells and download all the info and then display it
	
	}

	public void ParseHashToDicMyUser (Hashtable table)
	{
		table = (Hashtable)table ["user"];
		int user_id = (int)table ["id"];
		List<string> user_id_list = new List<string> ();
		user_id_list.Add (user_id.ToString());

		Dictionary<string,List<string>> dic_inside = new Dictionary<string, List<string>>();

		string user_name = string.Concat((string)table["first_name"],string.Concat(" ",(string)table["last_name"]));
		List<string> user_name_list = new List<string> ();
		user_name_list.Add (user_name);

		string user_mail = (string)table ["email"];
		List<string> user_mail_list = new List<string> ();
		user_mail_list.Add (user_mail);

		dic_inside.Add ("email", user_mail_list);
		dic_inside.Add ("name", user_name_list);
		dic_inside.Add ("id", user_id_list);

		//other_users_info.Add (user_id, dic_inside);
		my_user_info = dic_inside;
	}

	public void ParseHashToDicUser (Hashtable table)
	{
		table = (Hashtable)table ["user"];
		int user_id = (int)table ["id"];
		List<string> user_id_list = new List<string> ();
		user_id_list.Add (user_id.ToString());
		
		Dictionary<string,List<string>> dic_inside = new Dictionary<string, List<string>>();
		
		string user_name = string.Concat((string)table["first_name"],string.Concat(" ",(string)table["last_name"]));
		List<string> user_name_list = new List<string> ();
		user_name_list.Add (user_name);
		Debug.Log ("user name: " + user_name);
		string user_mail = (string)table ["email"];
		List<string> user_mail_list = new List<string> ();
		user_mail_list.Add (user_mail);

		dic_inside.Add ("email", user_mail_list);
		dic_inside.Add ("name", user_name_list);
		dic_inside.Add ("id", user_id_list);
		
		other_users_info.Add (user_id, dic_inside);
		Debug.Log (other_users_info[user_id]["name"][0]);
	}

	public void AskUser(string user_id)
	{
		Requester.getuser(User.local_url,"api/users/" + user_id,null);
	}
}
