using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InfoView : MonoBehaviour {

	public float target_position = -259, initial_position, acceleration = 80;
	public RectTransform rec;
	public bool closed = true;
	public int current_index_offer = -1, current_image_shown, current_user_id;
	public RawImage current_image;
	public Text price_text, user_text, rates_text, tittle_text, description_text;
	public GameObject edit_button;
	public List<Texture2D>  current_tex_list;
	public GameObject right_arrow,left_arrow;
	public string current_email;
	// Use this for initialization
	void Start () 
	{
		rec = this.GetComponent<RectTransform> ();
		initial_position = rec.anchoredPosition.y;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(closed)
		{
			rec.anchoredPosition = new Vector2(rec.anchoredPosition.x,rec.anchoredPosition.y - acceleration);
			if(rec.anchoredPosition.y < initial_position)
			{
				rec.anchoredPosition = new Vector2(rec.anchoredPosition.x,initial_position);
				this.gameObject.SetActive(false);
			}
		}

		if(!closed)
		{
			rec.anchoredPosition = new Vector2(rec.anchoredPosition.x,rec.anchoredPosition.y + acceleration);
			if(rec.anchoredPosition.y > target_position)
			{
				rec.anchoredPosition = new Vector2(rec.anchoredPosition.x,target_position);
			}
		}

	}

	public void ResizeImage(int index)
	{
		current_image.texture = current_tex_list [index];
		if (current_tex_list [index].width > current_tex_list [index].height)
		{
			current_image.GetComponent<RectTransform>().sizeDelta = new Vector2(1080,CalculateProportion(1080,current_tex_list[index].width,current_tex_list[index].height));
   		}
		else
		{
			current_image.GetComponent<RectTransform>().sizeDelta = new Vector2(CalculateProportion(1080,current_tex_list[index].height,current_tex_list[index].width),1080);
		}
	}

	public float CalculateProportion(float ref_1,float ref_2, float ref_3)
	{
		float desired_prop = 0;
		
		desired_prop = ref_1 * ref_3;
		desired_prop = desired_prop / ref_2;
		
		return desired_prop;
	}

	public void MoveRight()
	{
		current_image_shown += 1;
		if(current_image_shown+1 == current_tex_list.Count)
		{
			right_arrow.SetActive(false);
			left_arrow.SetActive(true);
		}
		else
		{
			left_arrow.SetActive(true);
		}
		ResizeImage (current_image_shown);
	}

	public void MoveLeft()
	{
		current_image_shown -= 1;
		if(current_image_shown == 0)
		{
			right_arrow.SetActive(true);
			left_arrow.SetActive(false);
		}
		else
		{
			right_arrow.SetActive(true);
		}
		ResizeImage (current_image_shown);
	}
}