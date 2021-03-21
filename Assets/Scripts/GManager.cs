using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
	public static GManager GMinstance { get; set; }

	public GameObject GroundPrefab;
	private int _level;

    private void Awake()
    {
		GMinstance = this;
    }

	void Start()
	{
		_level = PlayerPrefs.GetInt("level");
		if(_level == 0)
        {
			LevelUp();
        }
		GroundInstantiate();
	}

	void GroundInstantiate()
	{
		Vector3 groundPosition = new Vector3(0, -1, 0); // начальная и дальше следующая позиция платформы
		Quaternion RotationY = Quaternion.Euler(0, 0, 0); // начальный и дальше следующий поворот платформы

		for (int i = 0; i < _level; i++)
		{
			GameObject ground = Transform.Instantiate(GroundPrefab, groundPosition, RotationY); // префаб платформы
			if (i == _level - 1)
			{
				ground.GetComponent<Platform>().FinishInstantiate(); // на последней платформе ставим финиш
			}
			ground.transform.parent = transform;
			
			groundPosition = new Vector3(Mathf.Abs(ground.transform.position.x) + ground.transform.localScale.x / 2 - 2, -1, Mathf.Abs(ground.transform.position.x) + ground.transform.localScale.x / 2 - 2); // находим конец платформы
			if (ground.transform.rotation == Quaternion.Euler(0, 0, 0))
			{
				RotationY = Quaternion.Euler(0, 90, 0); // меняем поворот платформы
			}
			else
			{
				RotationY = Quaternion.Euler(0, 0, 0); // меняем поворот платформы
			}
		}
	}

	public void LevelUp()
    {
		_level++;
		PlayerPrefs.SetInt("level", _level);
    }
}
