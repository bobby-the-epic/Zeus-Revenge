using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float moveSpeed = 10.0f;

    void FixedUpdate()
    {
        if(gameObject.CompareTag("Player Projectile"))
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime, Space.World);
        else if(gameObject.CompareTag("Enemy Projectile"))
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime, Space.World);
        if (transform.position.x < -GameManager.xBound || transform.position.x > GameManager.xBound)
            Destroy(gameObject);
        else if (transform.position.y < -GameManager.yBound || transform.position.y > GameManager.yBound)
            Destroy(gameObject);
    }
}
