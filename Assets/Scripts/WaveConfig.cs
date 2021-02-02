using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
  [SerializeField] GameObject enemyPrefab;
  [SerializeField] GameObject pathPrefab;
  [SerializeField] float spawnPeriod = .5f;
  [SerializeField] float randomSpawnPeriodFactor = .2f;
  [SerializeField] int numberOfEnemies = 5;
  [SerializeField] float moveSpeed = 2f;

  public GameObject GetEnemyPrefab() { return enemyPrefab; }

  public List<Transform> GetWaypoints()
  {
    var waypoints = new List<Transform>();
    foreach (Transform child in pathPrefab.transform)
    {
      waypoints.Add(child);
    }

    return waypoints;
  }

  public float GetSpawnPeriod() { return spawnPeriod; }

  public float GetRandomSpawnPeriodFactor() { return randomSpawnPeriodFactor; }

  public int GetNumberOfEnemies() { return numberOfEnemies; }

  public float GetMoveSpeed() { return moveSpeed; }
}
