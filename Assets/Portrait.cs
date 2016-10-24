using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Portrait : Singleton<Portrait> {

	InfoView info_view;
	RawImage image;
	public Image fade;

	void Start()
	{
		image = transform.GetChild (0).GetComponent<RawImage> ();
		info_view = transform.parent.GetComponent<InfoView> ();
		fade = this.GetComponent<Image> ();
	}

	// Update is called once per frame
	public void Pop (Texture tex) 
	{
		image.texture = tex;
		image.rectTransform.sizeDelta = new Vector2 (image.rectTransform.sizeDelta.x, (image.rectTransform.sizeDelta.x * tex.height) / tex.width);

		image.enabled = true;
		fade.enabled = true;
		// here i have to load the image with the current infoproductindex and then  grab currentImageShown and retrieve the correct one

		// then average with the fixed width
	}

	public void Close()
	{
		image.enabled = false;
		fade.enabled = false;
	}
}
