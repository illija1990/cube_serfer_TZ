using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Way : MonoBehaviour
{
    public GameObject Player; // двигаем партикл следа от кубов за игроком

    private void FixedUpdate()
    {
        Vector3 position = transform.position;
        position.x = Player.transform.position.x;
        position.z = Player.transform.position.z;
        transform.position = position;

        transform.rotation = Player.transform.rotation;
    }
}
