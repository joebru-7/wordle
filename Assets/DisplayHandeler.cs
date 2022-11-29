using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DisplayHandeler : MonoBehaviour
{
	public static DisplayHandeler instance;

	[SerializeField] private TMPro.TMP_Text _fade;
	[SerializeField] private TMPro.TMP_Text _winLoss;

	// Start is called before the first frame update
	void Start()
	{
		instance = this;
	}

	public static void ShowInvalidWord()
	{
		instance._fade.color = 
			new Color(
				instance._fade.color.r, 
				instance._fade.color.g, 
				instance._fade.color.b, 1
				);
	}



	// Update is called once per frame
	void Update()
	{
		_fade.color = new Color(_fade.color.r, _fade.color.g, _fade.color.b, _fade.color.a - .001f);
	}
}
