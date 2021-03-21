using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject cubePrefab, coinPrefab, enemyCube, TurnTrigger, FinishPrefab;
    private Vector3 startCubePosition;

    private int countPlayerCubesX;
    private int countPlayerCubesZ;
    public static bool Finish;

    // каждая платформа генерирует на себе врагов, кубы для игрока и монетки
    // так как платформы одинаковые я прописал отступы и границы разово, в будущем можно заменить на переменные с % от размера платформы
    private void Start()
    {
        if (transform.rotation.y == 0)
        {
            // коллайдер для поворота
            GameObject turnTrigger = Transform.Instantiate(TurnTrigger, new Vector3(transform.position.x + transform.localScale.x / 2 - 5, 0, transform.position.z), Quaternion.identity);

            // позиция для первого куба
            startCubePosition = new Vector3(transform.position.x + transform.localScale.x / 2 - 180, 0, transform.position.z);

            // добавляем врагов кубы и монетки
            for (int i = 0; i <= transform.localScale.x-20; i += 8)
            {
                countPlayerCubesX++;
                if (countPlayerCubesX >= Random.Range(4, 8))
                {
                    for (int m = 0; m < 5; m++)
                    {
                        for (int l = 0; l <= Random.Range(2,countPlayerCubesX-1); l++)
                        {
                            GameObject enemy = Transform.Instantiate(enemyCube, new Vector3(startCubePosition.x + i, 0+l, transform.position.z - 2 + m), Quaternion.identity);
                        }
                    }
                    countPlayerCubesX = 0;
                }
                else
                {
                    GameObject cube = Transform.Instantiate(cubePrefab, new Vector3(startCubePosition.x + i, 0, Random.Range(transform.position.z - 2, transform.position.z + 2)), Quaternion.identity);
                }
            }

            for (int j = 0; j <= transform.localScale.x - 20; j += Random.Range(10, 20))
            {
                GameObject coin = Transform.Instantiate(coinPrefab, new Vector3(startCubePosition.x + j, 0, Random.Range(transform.position.z - 2, transform.position.z + 2)), Quaternion.identity);
            }

        }
        else // для повернутой платформы
        {
            GameObject turnTrigger = Transform.Instantiate(TurnTrigger, new Vector3(transform.position.x-2, 0, transform.position.z + transform.localScale.x / 2 -2), Quaternion.identity);

            startCubePosition = new Vector3(transform.position.x, 0, transform.position.z + transform.localScale.x / 2 - 180);
            for (int i = 0; i <= transform.localScale.x - 20; i += 8)
            {
                countPlayerCubesZ++;
                if (countPlayerCubesZ >= Random.Range(4, 8))
                {
                    for (int m = 0; m < 5; m++)
                    {
                        for (int l = 0; l <= Random.Range(2, countPlayerCubesZ-1); l++)
                        {
                            GameObject enemy = Transform.Instantiate(enemyCube, new Vector3(transform.position.x - 2 + m, 0+l, startCubePosition.z + i), Quaternion.identity);
                        }
                    }
                    countPlayerCubesZ = 0;
                }
                else
                {
                    GameObject cube = Transform.Instantiate(cubePrefab, new Vector3(Random.Range(transform.position.x - 2, transform.position.x + 2), 0, startCubePosition.z + i), Quaternion.identity);
                }
            }

            for (int j = 0; j <= transform.localScale.x - 20; j += Random.Range(10, 20))
            {
                GameObject coin = Transform.Instantiate(coinPrefab, new Vector3(Random.Range(transform.position.x - 2, transform.position.x + 2), 0, startCubePosition.z + j), Quaternion.identity);
            }
        }
    }

    public void FinishInstantiate() // добавляем финиш
    {
        if (transform.rotation.y == 0)
        {
            GameObject finish = Transform.Instantiate(FinishPrefab, new Vector3(transform.position.x + transform.localScale.x / 2 - 3, 0, transform.position.z), Quaternion.identity);
        }

        else
        {
            GameObject finish = Transform.Instantiate(FinishPrefab, new Vector3(transform.position.x, 0, transform.position.z + transform.localScale.x / 2 - 3), Quaternion.identity);
            finish.transform.rotation = Quaternion.Euler(0, -90, 0);
        }

    }
}
