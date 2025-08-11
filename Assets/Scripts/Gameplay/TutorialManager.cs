using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPrefab;
    [SerializeField] private Transform tutorialTransform;

    private GameObject tutorialInstance;

    public bool IsTutorialActive { get; private set; }
    private bool isTutorialPlayed = false;

    private void OnEnable()
    {
        EventSystem.OnStartTutorial += StartTutorial;
    }

    private void OnDisable()
    {
        EventSystem.OnStartTutorial -= StartTutorial;
    }

    private void StartTutorial()
    {
        if (!isTutorialPlayed && !SaveSystem.IsTutorialCompleted())
        {
            tutorialInstance = Instantiate(tutorialPrefab, tutorialTransform.position, Quaternion.identity, tutorialTransform);
            IsTutorialActive = true;
        }
        else
        {
            EndTutorial();
        }
    }

    public void EndTutorial()
    {
        if (tutorialInstance != null)
        {
            Destroy(tutorialInstance);
            tutorialInstance = null;
        }
        IsTutorialActive = false;
        isTutorialPlayed = true;
        SaveSystem.SaveTutorialCompleted(true);

        EventSystem.TriggerGameStart();
    }
}
