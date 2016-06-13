using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadUserPic : MonoBehaviour {

	public RawImage user_image, sidemenu_image;
	// Use this for initialization
	void Start () {
		sidemenu_image = this.GetComponent<RawImage> ();
	}

	public IEnumerator GetPic(string url) {
		// Start a download of the given URL
		WWW www = new WWW(url);
		
		// Wait for download to complete
		yield return www;
		
		// assign texture
		Debug.Log (url);
		user_image.texture = www.texture;
		sidemenu_image.texture = www.texture;
	}

	public IEnumerator GetOtherUserPic(string _url)
	{
		user_image.texture = null;
		WWW www = new WWW(_url);
		
		// Wait for download to complete
		yield return www;
		
		// assign texture
		user_image.texture = www.texture;
	}
}
