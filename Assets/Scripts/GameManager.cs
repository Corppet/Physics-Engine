using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { private set; get; }

    [HideInInspector] public bool isInPlay;

    [Header("Keybinds")]
    [SerializeField] private KeyCode menuKey = KeyCode.Escape;
    [SerializeField] private KeyCode restartKey = KeyCode.R;

    [Space(10)]

    [Header("References")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private Transform gravityArrowTransform;

    private float gravityRotationOffset;

    public void SetGravity(Vector2 gravity)
    {
        // change direction of gravity arrow
        float angle = Mathf.Atan2(gravity.y, gravity.x) * Mathf.Rad2Deg;
        gravityArrowTransform.eulerAngles = (angle + gravityRotationOffset + 90f) * Vector3.forward;

        // change direction of gravity
        Physics2D.gravity = gravity;
    }

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
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        gravityRotationOffset = gravityArrowTransform.eulerAngles.z;
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
