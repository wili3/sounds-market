using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ContentInitializer : MonoBehaviour {

	public contentmanager content;
	public GameObject[] images = new GameObject[4];
	public ImageManager image_manager;

	public float total_height;
	// Use this for initialization
	void Start () {

		image_manager = this .GetComponent<ImageManager> ();
		for(int i = 0; i < image_manager.heights.Length; i++)
		{
			total_height += content.calculateProportion(image_manager.heights[i],content.content[0].sizeDelta.x,image_manager.widths[i]);
			total_height += 250;
		}

		content.GetComponent<RectTransform> ().sizeDelta = new Vector2 (content.GetComponent<RectTransform> ().sizeDelta.x, total_height);
		//content.transform.parent.parent.GetComponent<ScrollRect> ().normalizedPosition = new Vector2 (content.GetComponent<ScrollRect> ().normalizedPosition.x, 0);
		for(int i = 0; i < images.Length; i++)
		{
			images[i].transform.parent = content.transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
