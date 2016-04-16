using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class UsersManager : MonoBehaviour {

	public Texture2D user_profile_pic;
	public List<Texture2D> user_pics;

	public Dictionary<string, List<string>> my_user_info  = new Dictionary<string, List<string>>();
	public Dictionary<string,Dictionary<string,List<string>>> other_users_info = new Dictionary<string, Dictionary<string, List<string>>>();

	// Use this for initialization
	void Start () {

		//first create my user info
		//then i will have to initialize the user array and when the user taps the button ver perfil assign the user to the list where the offer tells and download all the info and then display it
	
	}
}
