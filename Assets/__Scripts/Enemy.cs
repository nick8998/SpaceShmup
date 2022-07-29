using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    public float fireRate = 0.3f;
    public float health = 10;
    public int score = 100;

    private BoundsCheck bndCheck;

    public Vector3 pos { 
        get { return (this.transform.position); }
        set { this.transform.position = value; }
    }

    private void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }

    void Update()
    {
        Move();

        if (bndCheck != null && bndCheck.offDown)
        {
            Destroy(gameObject);
        }
    }

    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }

    private void OnCollisionEnter(Collision coll)
    {
        GameObject otherGo = coll.gameObject;
        if (otherGo.tag == "ProjectileHero")
        {
            Destroy(otherGo);
            Destroy(gameObject);
        }
        else if (otherGo.tag == "Hero")
        {
            Destroy(gameObject);
        }
        else
        {
            print("Enemy hit by non-Hero and non-Projectile" + otherGo.name);
        }
    }
}
