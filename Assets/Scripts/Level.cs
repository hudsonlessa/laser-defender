using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
  [SerializeField] float delayInSeconds = 3f;

  public void LoadStartMenu() { SceneManager.LoadScene(0); }

  public void LoadGameOver()
  {
    StartCoroutine(DelayedSceneChange("Game Over"));
  }

  IEnumerator DelayedSceneChange(string sceneName)
  {
    yield return new WaitForSeconds(delayInSeconds);
    SceneManager.LoadScene(sceneName);
  }

  public void LoadGameScene()
  {
    FindObjectOfType<GameSession>().ResetGame();
    SceneManager.LoadScene("Game");
  }

  public void QuitGame() { Application.Quit(); }
}
