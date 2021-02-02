using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  [Header("Enemy")]
  [SerializeField] int health = 3;
  [SerializeField] int scoreValue = 1;

  [Header("Projectile")]
  [SerializeField] float minShotInterval = .2f;
  [SerializeField] float maxShotInterval = 3f;
  [SerializeField] float projectileSpeed = 10f;
  [SerializeField] GameObject laserPrefab;
  float shotCounter;

  [Header("Visual Effects")]
  [SerializeField] GameObject explosionParticles;

  [Header("Sound Effects")]
  [SerializeField] AudioClip fireSound;
  [SerializeField] [Range(0, 1)] float fireSoundVolume = 1;
  [SerializeField] AudioClip deathSound;
  [SerializeField] [Range(0, 1)] float deathSoundVolume = 1;

  void Start()
  {
    shotCounter = Random.Range(minShotInterval, maxShotInterval);
  }

  void Update()
  {
    CountDownAndShot();
  }

  private void CountDownAndShot()
  {
    shotCounter -= Time.deltaTime;

    if (shotCounter <= 0f)
    {
      Fire();
    }
  }

  private void Fire()
  {
    GameObject laser = Instantiate(
      laserPrefab,
      transform.position,
      Quaternion.identity
    );

    laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
    AudioSource.PlayClipAtPoint(fireSound, Camera.main.transform.position, fireSoundVolume);

    shotCounter = Random.Range(minShotInterval, maxShotInterval);
  }

  private void OnTriggerEnter2D(Collider2D collider)
  {
    DamageDealer damageDealer = collider.gameObject.GetComponent<DamageDealer>();
    ProcessHit(damageDealer);
  }

  private void ProcessHit(DamageDealer damageDealer)
  {
    health -= damageDealer.GetDamage();
    damageDealer.Hit();
    if (health <= 0)
    {
      Explode();
    }
  }

  private void Explode()
  {
    Destroy(gameObject);
    GameObject explosion = Instantiate(explosionParticles, transform.position, Quaternion.identity);
    AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
    FindObjectOfType<GameSession>().AddToScore(scoreValue);
  }
}
