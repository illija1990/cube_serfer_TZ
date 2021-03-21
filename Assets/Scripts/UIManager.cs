using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
	public static UIManager Instance { get; set; }

	public GameObject Win, Lose, Restart, StartScreen, Finger;
	public Text scoretext, level;
	private int _coinCount;

	void Awake()
	{
		Instance = this;
		StartScreen.SetActive(true);
		Time.timeScale = 0;
	}

	void Start()
	{
		level.text = "LEVEL: " + PlayerPrefs.GetInt("level");
		_coinCount = PlayerPrefs.GetInt("score");
	}

	void Update()
	{
		scoretext.text = "" + _coinCount;
	}

	public void PlayGame()
	{
		Time.timeScale = 1;
		StartScreen.SetActive(false);
	}

	public void CoinAdd()
	{
		_coinCount++;
		PlayerPrefs.SetInt("score", _coinCount);
	}

	public void WinOrLose(bool win) // выграл или проиграл
	{
		if (win)
		{
			Win.SetActive(true);
		}
		else
		{
			Time.timeScale = 0;
			Lose.SetActive(true);
		}
		Restart.SetActive(true);
	}

	public void RestartButton()
	{
		SceneManager.LoadScene(0);
	}

}
