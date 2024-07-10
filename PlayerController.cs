using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    float xInput, yInput;
    float timer = 0;
    float fireDelay = 0.5f, nextFire = 0.5f;
    float moveSpeed = 10;
    Animator playerAnim;
    AudioSource playerAudio;
    public int health = 100;
    public int weaponDamage = 25;
    public bool isDead = false;
    public GameObject thunderbolt;
    public GameObject tbSpawnPos;
    public AudioClip thunderboltThrow;

    private void Awake()
    {
        playerAudio = GetComponent<AudioSource>();
        playerAnim = GetComponent<Animator>();
        playerAnim.speed = 3;
    }
    private void FixedUpdate()
    {
        SetMovement();
        timer += Time.deltaTime;
        if ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)) && 
            timer > nextFire && !PauseMenu.isPaused && !isDead)
        {
            nextFire = timer + fireDelay;
            playerAnim.SetTrigger("FireProjectile");
            nextFire -= timer;
            timer = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.gameObject.CompareTag("Player Projectile"))
        {
            Destroy(collision.gameObject);
            if (!isDead)
            {
                TakeDamage(5);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(5);
        }
    }
    void SetMovement()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
        transform.Translate(Vector2.right * xInput * Time.deltaTime * moveSpeed);
        transform.Translate(Vector2.up * yInput * Time.deltaTime * moveSpeed);
        if (transform.position.x > GameManager.xBound)
            transform.position = new Vector3(GameManager.xBound, transform.position.y);
        else if (transform.position.x < -GameManager.xBound)
            transform.position = new Vector3(-GameManager.xBound, transform.position.y);
        if (transform.position.y > GameManager.yBound - 3)
            transform.position = new Vector3(transform.position.x, GameManager.yBound - 3);
        else if (transform.position.y < -GameManager.yBound + 1)
            transform.position = new Vector3(transform.position.x, -GameManager.yBound + 1);
    }
    public void FireProjectile()
    {
        Instantiate(thunderbolt, tbSpawnPos.transform.position, thunderbolt.transform.rotation);
        playerAudio.PlayOneShot(thunderboltThrow, GameManager.volume);
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            isDead = true;
            DeathState();
        }
    }
    void DeathState()
    {
        print("Game Over!");
        Destroy(gameObject);
    }
}
