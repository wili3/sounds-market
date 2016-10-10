using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScrollItem : MonoBehaviour {

	public RawImage image;
	public Text price_text, title_text;
	// Use this for initialization
	public void Initialize(Dictionary<string,List<string>> dic)
	{
		title_text.text = dic ["tittle"] [0];
		price_text.text = string.Concat (dic ["price"] [0], "€");;
	}
}
