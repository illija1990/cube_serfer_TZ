using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHelper : MonoBehaviour
{
    public GameObject ParticleObject; // партикл взрыва
    private Rigidbody _cubeBody;
    public bool touchEnemy;

    private void Start()
    {
        _cubeBody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "enemy" && !touchEnemy)
        {
            WaitForCollision();
        }
        if (coll.gameObject.tag == "cube")
        {
            Destroy(coll.gameObject);
            GetComponentInParent<PlayerController>().AddCube();
        }
        if (coll.gameObject.tag == "coin")
        {
            Destroy(coll.gameObject);
            UIManager.Instance.CoinAdd();
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "ground")
        {
            float borderLimitZ = other.transform.position.z;
            float borderLimitX = other.transform.position.x;
            GetComponentInParent<PlayerController>().BorderController(borderLimitX, borderLimitZ);
        }
    }

    void WaitForCollision()
    {
        GetComponentInParent<PlayerController>().DisableChildren();
        Transform.Instantiate(ParticleObject, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
