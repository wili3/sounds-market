using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class ProductEditor : MonoBehaviour {

	public OfferCreationViewController oc_viewcontroller;
	public ProductsManager products_manager;
	public TagViewController tag_view;

	// Use this for initialization
	public void LoadCreatedOffer(int index)
	{
		oc_viewcontroller.clear_button.gameObject.SetActive(false);
		oc_viewcontroller.edit_button.gameObject.SetActive(true);
		oc_viewcontroller.gameObject.SetActive (true);
		oc_viewcontroller.closed = false;
		oc_viewcontroller.input_fields [0].text = products_manager.current_offers_view [index.ToString ()] ["tittle"][0];
		oc_viewcontroller.input_fields [1].text = products_manager.current_offers_view [index.ToString ()] ["desc"][0];
		oc_viewcontroller.input_fields [3].text =products_manager.current_offers_view [index.ToString ()] ["email"][0];
		oc_viewcontroller.input_fields [2].text =products_manager.current_offers_view [index.ToString ()] ["price"][0];

		List<string> temp_tags = products_manager.current_offers_view [index.ToString ()] ["tags"];

		for (int i = 0; i < temp_tags.Count; i++) {
			temp_tags[i] = tag_view.convert_dic[temp_tags[i]];
		}

		oc_viewcontroller.ShowTags (temp_tags);
		tag_view.dic = products_manager.current_offers_view [index.ToString ()] ["tags"];

		oc_viewcontroller.textures =  new Texture2D[4];
		for (int i = 0; i < oc_viewcontroller.textures.Length; i++)
		{
			try{
				oc_viewcontroller.textures[i] = products_manager.current_textures [index.ToString ()][i];
				oc_viewcontroller.images_to_upload[i].texture = oc_viewcontroller.textures[i]; 
			}
			catch{

			}
		}
		oc_viewcontroller.edit_mode = true;
		oc_viewcontroller.submit_button.gameObject.SetActive (false);
		oc_viewcontroller.sold_button.gameObject.SetActive (true);
	}
}
