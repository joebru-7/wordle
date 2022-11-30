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
		_winLoss.enabled = false;
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

	public void ShowWin()
	{
		_winLoss.text = "You won";
		_winLoss.enabled = true;
	}

	public void ShowLost(string correctWord)
	{
		_winLoss.text = "You Lost\nThe correct word was '" + correctWord + '\'';
		_winLoss.enabled = true;
	}

	public void Restart()
	{
		_winLoss.enabled = false;
		Wordle.instance.Restart();
	}

	// Update is called once per frame
	void Update()
	{
		if (_fade.color.a > 0)
			_fade.color = new Color(_fade.color.r, _fade.color.g, _fade.color.b, _fade.color.a - Time.deltaTime);
	}
}
