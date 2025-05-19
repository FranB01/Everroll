using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    [SerializeField] private GameObject GameOverPanel;
    public GameObject NewGamePanel;


    public void SetTimer(float time)
    {
        string timeString = TimeSpan.FromSeconds(time).ToString("mm\\:ss\\.ff");
        timeText.SetText(timeString);
    }

    public void GameOver()
    {
        GameOverPanel.SetActive(true);
        GameOverPanel.GetComponentInChildren<TextMeshProUGUI>().SetText("Game Over\n\n" + timeText.text);
        timeText.enabled = false;
    }

    public void ButtonStart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    
}
