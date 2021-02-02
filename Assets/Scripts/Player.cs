using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  // Configuration parameters
  [Header("Player")]
  [SerializeField] float moveSpeed = 10f;
  [SerializeField] float padding = .4f;
  [SerializeField] int health = 5;
  [SerializeField] GameObject explosionParticles;
  [SerializeField] AudioClip deathSound;
  [SerializeField] [Range(0, 1)] float deathSoundVolume = 1;

  [Header("Projectile")]
  [SerializeField] float projectileSpeed = 10f;
  [SerializeField] float firingPeriod = .1f;
  [SerializeField] AudioClip fireSound;
  [SerializeField] [Range(0, 1)] float fireSoundVolume = 1;

  Coroutine firingCoroutine;

  // Cached references
  [SerializeField] GameObject laserPrefab;

  float minX;
  float maxX;
  float minY;
  float maxY;

  // Start is called before the first frame update
  void Start() { SetMoveBoundaries(); }

  // Update is called once per frame
  void Update()
  {
    Move();
    Fire();
  }

  public int GetHealth() { return health; }

  private void Move()
  {
    float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
    float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

    float newXPosition = Mathf.Clamp(transform.position.x + deltaX, minX, maxX);
    float newYPosition = Mathf.Clamp(transform.position.y + deltaY, minY, maxY);

    transform.position = new Vector2(newXPosition, newYPosition);
  }

  private void SetMoveBoundaries()
  {
    Camera mainCamera = Camera.main;

    minX = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
    maxX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

    minY = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
    maxY = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
  }

  private void Fire()
  {
    if (Input.GetButtonDown("Fire1"))
    {
      firingCoroutine = StartCoroutine(FireContinuously());
    }

    if (Input.GetButtonUp("Fire1"))
    {
      StopCoroutine(firingCoroutine);
    }
  }

  IEnumerator FireContinuously()
  {
    while (true)
    {
      GameObject laser = Instantiate(
          laserPrefab,
          transform.position,
          Quaternion.identity
      );

      laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
      AudioSource.PlayClipAtPoint(fireSound, Camera.main.transform.position, fireSoundVolume);

      yield return new WaitForSeconds(firingPeriod);
    }
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
      health = 0;
      Explode();
    }
  }

  private void Explode()
  {
    Destroy(gameObject);
    GameObject explosion = Instantiate(explosionParticles, transform.position, Quaternion.identity);
    AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
    FindObjectOfType<Level>().LoadGameOver();
  }
}
