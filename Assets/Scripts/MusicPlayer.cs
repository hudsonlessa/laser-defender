using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
  void Awake()
  {
    SetUpSingleton();
  }

  private void SetUpSingleton()
  {
    int musicPlayerCounter = FindObjectsOfType(GetType()).Length;

    if (musicPlayerCounter > 1)
    {
      Destroy(gameObject);
    }
    else
    {
      DontDestroyOnLoad(gameObject);
    }
  }
}
