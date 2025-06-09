using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject AndroidGameOverPanel;
    public GameObject NewGamePanel;
    public GameObject AndroidNewGamePanel;
    public GameObject AndroidUI;

    private int phoneFontSize = 56;

    // before OnSceneLoaded
    private void Awake()
    {
        /* ---- Android code ---- */
#if UNITY_ANDROID 

        // makes panel with phone controlls appear instead of the PC one
        // Panel is activated from gamecontroller
        NewGamePanel = AndroidNewGamePanel;
        GameOverPanel = AndroidGameOverPanel;
        AndroidUI.SetActive(true);
        timeText.fontSize = phoneFontSize;
        /* ---- PC code ---- */
#else
        AndroidNewGamePanel.SetActive(false);
        AndroidUI.SetActive(false);
#endif

    }

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
