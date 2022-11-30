using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordHolder : MonoBehaviour
{
	public static WordHolder Instance;

	[SerializeField] Letter LetterPrefab;
	private Letter[,] _letters;

	public int WordLength = 5;
	public int AllowedGuesses = 6;
	private int _madeGuesses = 0;

	private int _wordIndex = 0;


	[ContextMenu("Start")]
	void Start()
	{
		Instance = this;
		for (int i = transform.childCount - 1; i >= 0; i--)
			DestroyImmediate(transform.GetChild(i).gameObject);

		_letters = new Letter[AllowedGuesses, WordLength];
		for (int i = 0; i < AllowedGuesses; i++)
		{
			for (int j = 0; j < WordLength; j++)
			{
				var letter = Instantiate(LetterPrefab, transform);
				letter.Init();
				letter.Value = ' ';
				_letters[i, j] = letter;
			}
		}

		Keyboard.instance.KeyPressEvent += KeyPress;
	}

	public void Restart()
	{
		Start();
		Keyboard.instance.KeyPressEvent -= KeyPress;
		_madeGuesses = 0;
		_wordIndex = 0;

	}

	void KeyPress(char c)
	{
		if (c == '<' || c== '>')
		{
			SpecialKey(c);
			return;
		}

		if (_wordIndex >= WordLength || _madeGuesses >= AllowedGuesses)
			return;

		_letters[_madeGuesses, _wordIndex++].Value = c;

	}

	void SpecialKey(char c)
	{
		switch(c)
		{
			case '>':
				if (_wordIndex == WordLength)
				{
					string guess = "";
					for (int i = 0; i < WordLength; i++)
						guess += _letters[_madeGuesses, i].Value;

					if (!Wordle.instance.words.HasWord(guess))
					{
						DisplayHandeler.ShowInvalidWord();
						return;
					}

					ComparisonResult[] result = Wordle.instance.MakeGuess(guess);
					
					for (int i = 0; i < WordLength; i++)
					{
						_letters[_madeGuesses, i].SetColor(result[i]);
					}

					_madeGuesses++;
					_wordIndex = 0;

					if (_madeGuesses == AllowedGuesses)
						Wordle.instance.Loss();

				}
				return;
			case '<':
				if (_wordIndex == 0) return;
				_letters[_madeGuesses, --_wordIndex].Value = ' ';
				return;
			default:
				throw new NotImplementedException("Unreachable");
		}
			
	}
}
