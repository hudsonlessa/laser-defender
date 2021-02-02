﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
  private TextMeshProUGUI scoreText;
  private GameSession gameSession;

  // Start is called before the first frame update
  void Start()
  {
    scoreText = GetComponent<TextMeshProUGUI>();
    gameSession = FindObjectOfType<GameSession>();
  }

  // Update is called once per frame
  void Update() { scoreText.text = gameSession.GetScore().ToString(); }
}
