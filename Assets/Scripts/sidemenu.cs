using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class sidemenu : MonoBehaviour {

	public bool closed = true, handled = false;
	public float initial_x;
	public float target_x;

	public float acceleration = 0.2f;
	public RectTransform rec;

	public ScrollRect ref_scroll;
	public RectTransform ref_input_position;
	public RectTransform ref_parent_position;

	public float perc;
	public float diff;

	public Image shadow;
	public float shadow_level = 0.60f;
	public GameObject sidemenu_object;
	// Use this for initialization
	void Start () 
	{
		rec = this.GetComponent<RectTransform> ();
		initial_x = rec.anchoredPosition.x;
		ref_input_position = this.transform.GetChild (0).GetChild(0).GetComponent<RectTransform> ();

		diff = -(initial_x - target_x);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(closed && !handled)
		{
			if(rec.anchoredPosition.x > initial_x)
			{
				rec.anchoredPosition = new Vector2(rec.anchoredPosition.x - acceleration,rec.anchoredPosition.y);
				calculatePerc();
				if (ref_scroll.velocity.y > 1 || ref_scroll.velocity.y < -1)
				{
					ref_scroll.velocity = Vector2.zero;
				}
			}
			if(sidemenu_object.activeInHierarchy)
			{
				sidemenu_object.SetActive(false);
			}
		}
		if(!closed && !handled)
		{
			if(rec.anchoredPosition.x < target_x)
			{
				rec.anchoredPosition = new Vector2(rec.anchoredPosition.x + acceleration,rec.anchoredPosition.y);
				calculatePerc();
				if (ref_scroll.velocity.y > 1 || ref_scroll.velocity.y < -1)
				{
					ref_scroll.velocity = Vector2.zero;
				}
			}
			
			if(!sidemenu_object.activeInHierarchy)
			{
				sidemenu_object.SetActive(true);
			}
		}

		if (rec.anchoredPosition.x < initial_x)
		{
			rec.anchoredPosition = new Vector2 (initial_x, rec.anchoredPosition.y);
		}
	
	}
	public void calculatePerc()
	{
		float ref_1 = rec.anchoredPosition.x + -(initial_x);
		float ref_2 = target_x + -(initial_x);

		float temp_perc = ref_1 * 100;
		perc = temp_perc / ref_2;

		if(perc < 0 )perc = 0;
		if(perc > 100)perc = 100;



		float temp_a = perc * shadow_level;
		shadow.color= new Color(0,0,0, temp_a / 100);
	}
}
