using UnityEngine;
using UnityEngine.UI;

class Letter : MonoBehaviour
{
	char value => tmp.text[0];
	
	Color color => img.color;

	TMPro.TextMeshPro tmp;
	Image img;

	private void Start()
	{
		tmp = GetComponentInChildren<TMPro.TextMeshPro>();
		img = GetComponent<Image>();
	}
}
