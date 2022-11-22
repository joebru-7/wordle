using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class Keyboard : MonoBehaviour
{
	[SerializeField] Letter LetterPrefab;
	readonly Char[] _keyboardLeyout = {
			'Q', 'W', 'E' ,'R', 'T', 'Y', 'U', 'I', 'O', 'P',
			'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L', ' ',
			'Z', 'X', 'C', 'V', 'B', 'N', 'M', ' ', '<', '>'};

	[DoNotSerialize] Letter[] _letters;


	[ContextMenu("Start")]
	void Start()
	{
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

			InputSystem.onAnyButtonPress.Call(KeyboardHandeler);
		}
	}

	private void KeyboardHandeler(InputControl obj)
	{
		print(obj.IsPressed());
		if (!obj.IsPressed())
			return;
		print(obj.name);
	}

	void ClickHandeler(char c)
	{
		print(c);
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
