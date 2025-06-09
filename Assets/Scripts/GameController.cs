using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance{ get; private set; }

    float timer = 0f;
    private UIController ui;
    private static bool hasReset = false; // tracks if the game has been reset, to show a different panel and set time to 0

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        // No DontDestroyOnLoad — instance will be destroyed with the scene
    }

    private void Update()
    {
        timer += Time.deltaTime;
        ui.SetTimer(timer);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ui = FindAnyObjectByType<UIController>();

        if (!hasReset)
        {
            Time.timeScale = 0;
            ui.NewGamePanel.SetActive(true);
            hasReset = true; // will be true when next awake called after first time playing
            return;
        }

        Time.timeScale = 1;
    }

}
