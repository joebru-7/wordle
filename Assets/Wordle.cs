using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

public class Wordle : MonoBehaviour
{
	public static Wordle instance;

	//public string[] words;
	public Trie words;
	public int WordSize = 5;

	public int SelectedIndex;
	public string SelectedWord;

	[ContextMenu("Start")]
	void Start()
	{
		instance = this;
		LoadWords();
		GenerateRandomWord();

	}

	[ContextMenu("SelectRandomWord")]
	void GenerateRandomWord()
	{
		if (words == null)
			throw new Exception("No words loaded");
		
		SelectedWord = words.SelectRandomValidWord();
		/*
		SelectedIndex =  UnityEngine.Random.Range(0, words.Length - 1);
		SelectedWord = words[SelectedIndex];
		*/
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
		string[] tempWords = x.text.Split('\n');
		
		words = new Trie();
		for (int i = 0; i < tempWords.Length; i++)
		{
			words.AddWord(tempWords[i].Trim());
		}
	}

	public ComparisonResult[] MakeGuess(string word)
	{
		Assert.IsTrue(word.Length == WordSize);

		var result = Analyze(word);
		if (result.All((r) => r == ComparisonResult.Correct))
		{
			Victory();
			return result;
		}

		for (int i = 0; i < word.Length; i++)
		{
			char letter = word[i];
			Keyboard.instance.UpdateLetterColour(letter, result[i]);
		}

		return result;

	}

	private void Victory()
	{
		//TODO
		print("you won"!);
	}

	ComparisonResult[] Analyze(string guess)
	{
		ComparisonResult[] result = new ComparisonResult[guess.Length];
		//bool containsWrongPlace = false;
		//List<(char,int)> chars = new(5);

		for (int i = 0; i < guess.Length; i++)
		{
			if (guess[i] == SelectedWord[i])
			{
				result[i] = ComparisonResult.Correct;
				continue;
			}
			else if ( !SelectedWord.Contains(guess[i]) )
			{
				result[i] = ComparisonResult.NotInWord;
			}
			else
			{
				//TODO check duplicates
				result[i] = ComparisonResult.WrongPlace;
				//containsWrongPlace = true;
				//chars.Add((guess[i],i));
			}
		}

		/*
		if (!containsWrongPlace)
			return result;

		chars.Sort();

		for (int i = 0; i < guess.Length; i++)
		{

		}
		*/
		
		return result;
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
