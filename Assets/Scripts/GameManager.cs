using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager instance { private set; get; }

    [HideInInspector] public bool isInPlay;

    [Header("Keybinds")]
    [SerializeField] private KeyCode menuKey = KeyCode.Escape;
    [SerializeField] private KeyCode restartKey = KeyCode.R;

    [Space(10)]

    [Header("References")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text gameOverText;

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over";
        gameOverPanel.SetActive(true);
        isInPlay = false;
    }

    public void LevelComplete()
    {
        gameOverText.text = "Level Complete";
        gameOverPanel.SetActive(true);
        isInPlay = false;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gameOverPanel.SetActive(false);

        isInPlay = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(menuKey))
        {
            ReturnToMenu();
        }
        else if (Input.GetKeyDown(restartKey))
        {
            Restart();
        }
    }
}
