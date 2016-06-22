using UnityEngine;
using System.Collections;

public class EditUserScript : MonoBehaviour {

	public UsersManager user_manager;
	public UserView user_view;
	
	// Update is called once per frame
	public void EditUser()
	{
		if (!user_view.my_user) {
			Debug.Log("triggering EDIT USER when souldn't");
			return;
		}
		string name = user_view.user_name.text;

		string first_name = name;
		string last_name = ""; 

		string email = user_view.user_mail.text;

		Debug.Log (first_name);
		Debug.Log (last_name);

		user_manager.my_user_info ["email"][0] = email;
		user_manager.my_user_info ["name"][0] = first_name + last_name;

		Hashtable table = new Hashtable ();
		Hashtable table_child = new Hashtable ();

		table_child.Add ("email", email);
		table_child.Add ("first_name", first_name);
		table_child.Add ("last_name", last_name);
		table.Add ("user", table_child);

		StartCoroutine (Requester.edituser (User.current_url(), "api/users/" + user_manager.my_user_info ["id"] [0],table));
	}
}
