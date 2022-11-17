using UnityEngine;
using UnityEngine.UI;

class Letter : MonoBehaviour
{
	public char Value {
		get => tmp.text[0];
		set => tmp.text = value.ToString();
		}
	public Color Color
	{
		get => img.color;
		set => img.color = value;
	}
	TMPro.TextMeshPro tmp;
	Image img;

	private void Start()
	{
		tmp = GetComponentInChildren<TMPro.TextMeshPro>();
		img = GetComponent<Image>();
	}
}
