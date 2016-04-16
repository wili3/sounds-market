using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class ProductEditor : MonoBehaviour {

	public OfferCreationViewController oc_viewcontroller;
	public ProductsManager products_manager;

	// Use this for initialization
	public void LoadCreatedOffer(int index)
	{
		oc_viewcontroller.gameObject.SetActive (true);
		oc_viewcontroller.closed = false;
		oc_viewcontroller.input_fields [0].text = products_manager.current_offers_view [index.ToString ()] ["tittle"][0];
		oc_viewcontroller.input_fields [1].text = products_manager.current_offers_view [index.ToString ()] ["desc"][0];
		oc_viewcontroller.input_fields [3].text =products_manager.current_offers_view [index.ToString ()] ["email"][0];
		oc_viewcontroller.input_fields [2].text =products_manager.current_offers_view [index.ToString ()] ["price"][0];

		oc_viewcontroller.textures =  new Texture2D[4];
		for (int i = 0; i < oc_viewcontroller.textures.Length; i++)
		{
			oc_viewcontroller.textures[i] = products_manager.current_textures [index.ToString ()][i];
			oc_viewcontroller.images_to_upload[i].texture = oc_viewcontroller.textures[i]; 
		}
		oc_viewcontroller.edit_mode = true;
	}
}
