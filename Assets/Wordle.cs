using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

enum ComparisonResult
{
	NotInWord,
	WrongPlace,
	Correct
}

public class Wordle : MonoBehaviour
{
	// Start is called before the first frame update
	public string[] words;
	public int WordSize = 5;

	public int SelectedIndex;
	public string SelectedWord;

	[ContextMenu("Start")]
	void Start()
	{
		LoadWords();
		GenerateRandomWord();

		var x = Analyze("hello");
		x.All((r) => r == ComparisonResult.Correct);
	}

	void GenerateRandomWord()
	{
		if (words == null)
			throw new Exception("No words loaded");

		SelectedIndex =  UnityEngine.Random.Range(0, words.Length - 1);
		SelectedWord = words[SelectedIndex];

	}

	[ContextMenu("loadWords")]
	void LoadWords()
	{
		TextAsset x;
		if (WordSize <0)
			x = Resources.Load<TextAsset>("wordsAll");
		else if (WordSize > 10)
			x = Resources.Load<TextAsset>("words11+");
		else
			x = Resources.Load<TextAsset>("words" + WordSize);

		words = x.text.Split('\n');
		for (int i = 0; i < words.Length; i++)
		{
			words[i] = words[i].Trim();
		}
	}

	ComparisonResult[] Analyze(string guess)
	{
		ComparisonResult[] result = new ComparisonResult[guess.Length];
		for (int i = 0; i < guess.Length; i++)
		{
			if ( !SelectedWord.Contains(guess[i]) )
			{
				result[i] = ComparisonResult.NotInWord;
			}
			else if (guess[i] == SelectedWord[i])
			{
				result[i] = ComparisonResult.Correct;
			}
			else if ( SelectedWord.Count((c) => c == guess[i]) >= SelectedWord.Substring(0, i).Count((c) => c == guess[i]))
			{ 

			}
		}
		
		return result;
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public static int BinSearch(string obj,IList<string> collection )
	{
		int start = 0;
		int end = collection.Count;
		
		while(true)
		{
			int middle = (end-start) / 2 + start;
			var compRes = String.Compare(obj, collection[middle]);
			if (compRes == 0)
			{
				return middle;
			}
			else if (compRes < 0)
			{
				end = middle-1;
			}
			else
			{
				start = middle+1;
			}

			if (end < start || start == middle && middle == end)
				return -1;
		}
	}
}
