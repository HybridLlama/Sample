using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Transform winCanvas;
    [SerializeField] private Transform PauseMenu;
    [SerializeField] private Button pauseButton;

    void Start()
    {
        GameManager.Instance.OnLevelFinished += OnLevelFinished;
        pauseButton.onClick.AddListener(OnPausePressed);
    }

    private void OnLevelFinished()
    {
        winCanvas.gameObject.SetActive(true);
    }

    private void OnPausePressed()
    {
        bool pauseActive = PauseMenu.gameObject.activeInHierarchy;
        if (!pauseActive)
        {
            Time.timeScale = 0;
            PauseMenu.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            PauseMenu.gameObject.SetActive(false);
        }
    }
}
