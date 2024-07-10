using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public int health;
    public bool isDead = false;
    public GameObject enemyProjectile;
    public AudioClip projectileSFX;
    protected float fireRate = 1;
    protected float startDelay = 3;
    protected int score;
    protected int weaponDamage;
    protected GameObject player;
    protected PlayerController playerStats;
    protected AudioSource enemyAudio;
    protected Animator animatorController;

    public virtual void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerStats = player.GetComponent<PlayerController>();
        enemyAudio = GetComponent<AudioSource>();
        enemyAudio.volume = GameManager.volume;
        animatorController = GetComponent<Animator>();
        InvokeRepeating("FireProjectile", startDelay, fireRate);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player Projectile"))
        {
            TakeDamage(playerStats.weaponDamage);
            Destroy(collision.gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            isDead = true;
            GameManager.score += score;
            Destroy(gameObject);
        }
    }
    protected virtual void FireProjectile()
    {
        Instantiate(enemyProjectile, transform.position, enemyProjectile.transform.rotation);
        enemyAudio.PlayOneShot(projectileSFX, GameManager.volume);
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
