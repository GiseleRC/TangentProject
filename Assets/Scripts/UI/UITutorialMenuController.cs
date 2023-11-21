using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITutorialMenuController : MonoBehaviour
{
    [SerializeField] private UIController _uIController;

    [SerializeField] private GameObject _tutorialImageGO;
    [SerializeField] private GameObject _firstSection;
    [SerializeField] private GameObject _secondSection;
    [SerializeField] private GameObject _thirdSection;
    [SerializeField] private GameObject _fourthSection;
    [SerializeField] private GameObject _fifthSection;

    private GameObject _currSection;

    private void Start()
    {
        _tutorialImageGO.SetActive(true);

        GameManager.Instance.LoadPersistentData();
        if (!GameManager.Instance.PersistentData.tutorialCompleted)
        {
            PlayTutorial();
        }
        else
        {
            _tutorialImageGO.SetActive(false);
        }
    }

    public void PlayTutorial()
    {
        _uIController.EnableMenu(false);
        _firstSection.SetActive(true);
        _tutorialImageGO.SetActive(true);

        _currSection = _firstSection;

        EventManager.TriggerEvent(EventType.TutorialStarted);
    }

    private void EndTutorial()
    {
        _uIController.EnableMenu(true);
        gameObject.SetActive(false);

        EventManager.TriggerEvent(EventType.TutorialCompleted);
    }

    public void CloseSection()
    {
        if (_currSection == _firstSection)
        {
            _currSection.SetActive(false);
            _secondSection.SetActive(true);

            _currSection = _secondSection;
        }
        else if (_currSection == _secondSection)
        {
            _currSection.SetActive(false);
            _thirdSection.SetActive(true);

            _currSection = _thirdSection;
        }
        else if (_currSection == _thirdSection)
        {
            _currSection.SetActive(false);
            _fourthSection.SetActive(true);

            _currSection = _fourthSection;
        }
        else if (_currSection == _fourthSection)
        {
            _currSection.SetActive(false);
            _fifthSection.SetActive(true);

            _currSection = _fifthSection;
        }
        else if (_currSection == _fifthSection)
        {
            _currSection.SetActive(false);
            EndTutorial();
        }
    }
}
