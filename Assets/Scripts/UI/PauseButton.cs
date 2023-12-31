using UnityEngine;
using TMPro;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _playMenu;
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _unpauseButton;

    public void Pause()
    {
        _pauseMenu.SetActive(true);
        _playMenu.SetActive(false);
        _pauseButton.SetActive(false);
        _unpauseButton.SetActive(true);
        Time.timeScale = 0;

        JsonSaveSystem.Instance.SaveGame();
    }
    public void Unpause()
    {
        _pauseMenu.SetActive(false);
        _playMenu.SetActive(true);
        _unpauseButton.SetActive(false);
        _pauseButton.SetActive(true);
        Time.timeScale = 1;

        JsonSaveSystem.Instance.SaveGame();
    }
}