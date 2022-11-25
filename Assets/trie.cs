using System;
using System.Linq;
using System.Reflection;

public class Trie
{
	readonly Node root = new Node();
	public bool HasWord(string s)
	{
		return root.HasWord(s);
	}

	public void AddWord(string s)
	{
		root.AddWord(s);
	}

	public bool this[string s]
	{
		get { return HasWord(s); }
	}

	public string SelectRandomValidWord()
	{
		if (!root.IsValidOrHasChildren())
		{
			throw new IndexOutOfRangeException("No words in list");
		}

		return root.SelectRandomValidWord();
	}

	class Node
	{
		bool IsValid = false;
		const int ArrSize = 'Z' - 'A' +1;
		Node[] children;

		int Map(char c)
		{
			if(!char.IsLetter(c))
				throw new IndexOutOfRangeException();
			return char.ToUpper(c) - 'A';
		}
		char InvMap(int i)
		{
			return (char)(i + 'A');
		}

		public bool IsValidOrHasChildren() => IsValid || children != null; 

		public bool HasWord(string s)
		{
			if (s == "")
				return IsValid;
			else if (children == null || children[Map(s[0])] == null) 
				return false;
			else
				return children[Map(s[0])].HasWord(s[1..]);
		}

		public void AddWord(string s)
		{
			if(s == "")
			{
				IsValid = true;
				return;
			}

			children ??= new Node[ArrSize];

			int index = Map(s[0]);
			if (children[index] == null)
				children[index] = new Node();

			children[index].AddWord(s[1..]);
		}

		public string SelectRandomValidWord()
		{
			if (children == null)
				if (IsValid)
					return "";
				else
					throw new Exception("unreacheble");

			int num = children.Count((n) => n != null && n.IsValidOrHasChildren()) ;

			int randomnumber = UnityEngine.Random.Range(IsValid?-1:0, num );

			if (randomnumber == -1)
			{
				return "";
			}
			else
			{
				int count = 0;
				for (int i = 0; i < children.Length; i++)
				{
					Node n = children[i];
					if (n != null && n.IsValidOrHasChildren())
					{
						if (count++ == randomnumber)
						{
							return InvMap(i) + n.SelectRandomValidWord();
						}
					}
				}
			}
			throw new Exception("unreacheble");
		}
	}
}
