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
		_fade.alpha = 0;
	}

	public static void ShowInvalidWord()
	{
		instance._fade.alpha = 1;
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
		if (_fade.alpha > 0)
			_fade.alpha -= Time.deltaTime;
	}
}
