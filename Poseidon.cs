using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poseidon : Boss
{
    public Poseidon()
    {
        fireRate = 0.5f;
    }
    protected override void FireProjectile()
    {
        Instantiate(enemyProjectile, projectileSpawnPos.transform.position, enemyProjectile.transform.rotation);
        Instantiate(enemyProjectile, projectileSpawnPos.transform.position + new Vector3(0, 1), enemyProjectile.transform.rotation);
        Instantiate(enemyProjectile, projectileSpawnPos.transform.position + new Vector3(0, -1), enemyProjectile.transform.rotation);
        enemyAudio.PlayOneShot(projectileSFX, GameManager.volume);
    }
}
