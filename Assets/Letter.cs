using System;
using UnityEngine;
using UnityEngine.UI;

class Letter : MonoBehaviour
{
	public Char Value {
		get => _tmp.text[0];
		set => _tmp.text = value.ToString();
		}
	private Color Color
	{
		get => _img.color;
		set => _img.color = value;
	}
	TMPro.TMP_Text _tmp;
	Image _img;
	Button _btn;

	public delegate void OnStartFunction(Letter l);
	public event OnStartFunction OnStartCallback;

	private void Start()
	{
		Init();
		OnStartCallback?.Invoke(this);
	}

	bool _hasBeenInited = false;
	public void Init()
	{
		if (_hasBeenInited)
			return;
		_hasBeenInited = true;

		_tmp = GetComponentInChildren<TMPro.TMP_Text>();
		_img = GetComponent<Image>();
		_btn = GetComponent<Button>();
		_btn.onClick.AddListener(ClickHandeler);
	}

	public void SetColor(ComparisonResult r)
	{
		switch (r)
		{
			case ComparisonResult.Initial:
				Color = new Color(1,1,1,.5f);
				break;
			case ComparisonResult.NotInWord:
				if (Color != Color.green && Color != Color.yellow)
					Color = Color.gray;
				break;
			case ComparisonResult.WrongPlace:
				if (Color != Color.green)
				Color = Color.yellow;
				break;
			case ComparisonResult.Correct:
				Color = Color.green;
				break;
		}
	}

	public delegate void OnClickFunction(char c);
	public event OnClickFunction OnClick;

	private void ClickHandeler()
	{
		if(Value != ' ')
			OnClick?.Invoke(char.ToUpper(Value));
	}
}
