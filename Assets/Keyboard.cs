using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class Keyboard : MonoBehaviour
{
	public static Keyboard instance;

	[SerializeField] Letter LetterPrefab;
	readonly Char[] _keyboardLeyout = {
			'Q', 'W', 'E' ,'R', 'T', 'Y', 'U', 'I', 'O', 'P',
			'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L', ' ',
			'Z', 'X', 'C', 'V', 'B', 'N', 'M', ' ', '<', '>'};

	[DoNotSerialize] Letter[] _letters;


	[ContextMenu("Start")]
	void Start()
	{
		instance = this;
		for (int i = transform.childCount-1; i >= 0 ; i--)
			DestroyImmediate(transform.GetChild(i).gameObject);

		_letters = new Letter[_keyboardLeyout.Length];
		for (int i = 0; i < _keyboardLeyout.Length; i++)
		{
			char letter = _keyboardLeyout[i];

			_letters[i] = Instantiate(LetterPrefab, transform);
			_letters[i].Init();
			_letters[i].name = letter.ToString();
			_letters[i].Value = letter;

			_letters[i].OnClick += ClickHandeler;
		}

		InputSystem.onAnyButtonPress.Call(KeyboardHandeler);
	}

	

	private void KeyboardHandeler(InputControl inputControl)
	{
		//TODO multipe keys
		switch (inputControl.name)
		{
			case string n when n.Length == 1:
				ClickHandeler(char.ToUpper(n[0]));
				break;
			case "enter":
				ClickHandeler('>');
				break;
			case "backspace":
				ClickHandeler('<');
				break;
			default:
				break;
		}
		//print(inputControl.name + " " + inputControl.name.Length);
		
	}

	public delegate void keyPressFunc(char c);
	public event keyPressFunc KeyPressEvent;

	void ClickHandeler(char c)
	{
		KeyPressEvent?.Invoke(c);
		//print(c);
	}

	public void UpdateLetterColour(char c, ComparisonResult color)
	{
		_letters.First((letter)=>letter.Value==char.ToUpper(c)).SetColor(color);
	}
}
