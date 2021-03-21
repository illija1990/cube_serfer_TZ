using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
	public GameObject cubePrefab;
	private Rigidbody playerBody;
	private float _playerSpeed = 10f;
	private float _borderLimitZ;
	private float _borderLimitX;
	public bool turnOnLeft;
	public bool turnOnRight;

	void Start()
	{
		playerBody = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		transform.Translate(Vector3.right * _playerSpeed * Time.deltaTime); // двигаем игрока

		if (transform.rotation == Quaternion.Euler(0, 0, 0)) // управление по прямой
		{
			if (Input.GetAxis("Mouse X") > 0 && transform.position.z > _borderLimitZ - 2) // если свайпаем вправо и игрок не вышел за правую границу екрана
			{
				transform.Translate(-Vector3.forward * _playerSpeed * Time.deltaTime); // двигаем игрока вправо
			}
			else if (Input.GetAxis("Mouse X") < 0 && transform.position.z < _borderLimitZ + 2) // если свайпаем влево и игрок не вышел за левую границу екрана
			{
				transform.Translate(Vector3.forward * _playerSpeed * Time.deltaTime); // двигаем игрока влево
			}
		}

		if (transform.rotation == Quaternion.Euler(0, -90, 0)) // управление после поворота
        {
			if (Input.GetAxis("Mouse X") > 0 && transform.position.x < _borderLimitX+2) // если свайпаем вправо и игрок не вышел за правую границу екрана
			{
				transform.Translate(-Vector3.forward * _playerSpeed * Time.deltaTime); // двигаем игрока вправо
			}
			else if (Input.GetAxis("Mouse X") < 0 && transform.position.x > _borderLimitX - 2) // если свайпаем влево и игрок не вышел за левую границу екрана
			{
				transform.Translate(Vector3.forward * _playerSpeed * Time.deltaTime); // двигаем игрока влево
			}
		}
	}

	private void Update() // когда игрок доходит до края платформы - поворачиваем его
	{
		if (turnOnLeft)
		{
			TurnPayer(90, 88);
		}

		if (turnOnRight)
		{
			TurnPayer(0, 2);
		}
	}

	public void TurnPayer(int Y1, int Y2)
    {
		Quaternion targetLeft = Quaternion.Euler(0, -Y1, 0);
		transform.rotation = Quaternion.Lerp(transform.rotation, targetLeft, 4f * Time.deltaTime);
		if (transform.rotation == Quaternion.Euler(0, -Y2, 0))
		{
			transform.rotation = Quaternion.Euler(0, -Y1, 0);
			turnOnLeft = false;
		}
	}

	public void AddCube() // добавляем куб под игрока
    {
		Vector3 pos = this.transform.position;
		pos.y += 1.2f;
		transform.position = pos;
		StartCoroutine(AnimationControl("jump"));
		GameObject newCube = Transform.Instantiate(cubePrefab, new Vector3(pos.x, -0.1f, pos.z), Quaternion.identity);
		newCube.transform.parent = transform;
	}

	public void DisableChildren() // отключаем касание игроком врагов
    {
		StartCoroutine(ChildrenOffTouch());
	}

	public IEnumerator ChildrenOffTouch()
    {
		yield return new WaitForSeconds(0.02f);
		GameObject[] child = GameObject.FindGameObjectsWithTag("playerCube");
		foreach (GameObject cGo in child)
		{
			cGo.GetComponent<PlayerCollisionHelper>().touchEnemy = true;
		}

		yield return new WaitForSeconds(0.2f);
		//child = GameObject.FindGameObjectsWithTag("playerCube");
		
		foreach (GameObject cGo in child)
		{
			cGo.GetComponent<PlayerCollisionHelper>().touchEnemy = false;
		}

		yield return new WaitForSeconds(0.7f); // если под игроком нет кубов - проиграл
		if (child.Length == 0)
		{
			_playerSpeed = 0;
			StartCoroutine(AnimationControl("die"));
			UIManager.Instance.WinOrLose(false);
		}
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "turnTrigger")
        {
			if (transform.rotation.y >= 0)
			{
				turnOnLeft = true;
			}
			if (transform.rotation.y < 0)
			{
				turnOnRight = true;
			}
		}

		if(other.gameObject.tag == "finish")
        {
			UIManager.Instance.WinOrLose(true);
			_playerSpeed = 0;
			GManager.GMinstance.LevelUp();
		}
    }

    public void BorderController(float borderX, float borderZ)
	{ 
			_borderLimitZ = borderZ;
			_borderLimitX = borderX;
    }

	IEnumerator AnimationControl(string what)
    {
		if (what == "jump")
		{
			GetComponentInChildren<Animator>().SetBool("Jump", true);
			yield return new WaitForSeconds(0.5f);
			GetComponentInChildren<Animator>().SetBool("Jump", false);
		}
        else
        {
			GetComponentInChildren<Animator>().SetBool("Die", true);
		}
    }
}
