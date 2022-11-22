using System;
using UnityEngine;
using UnityEngine.UI;

class Letter : MonoBehaviour
{
	public Char Value {
		get => _tmp.text[0];
		set => _tmp.text = value.ToString();
		}
	public Color Color
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
		_hasBeenInited=true;

		_tmp = GetComponentInChildren<TMPro.TMP_Text>();
		_img = GetComponent<Image>();
		_btn = GetComponent<Button>();
		_btn.onClick.AddListener(ClickHandeler);
	}



	public void SetColor(ComparisonResult r)
	{
		switch (r)
		{
			case ComparisonResult.NotInWord:
				Color = Color.red;
				break;
			case ComparisonResult.WrongPlace:
				Color = Color.yellow;
				break;
			case ComparisonResult.Correct:
				Color = Color.green;
				break;
			default:
				break;
		}
	}

	public delegate void OnClickFunction(char c);
	public event OnClickFunction OnClick;

	private void ClickHandeler()
	{
		OnClick?.Invoke(Value);
	}

	public void SetColor(Color c)
	{
		Color = c;
	}


}
