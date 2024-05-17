using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public string tag;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public AudioSource exitAudio;
    public CanvasGroup caughtBackgroundImageCanvasGroup;
    public AudioSource caughtAudio;
    public GameObject survivorManagerGO;
    private SurvivorManager survivorManager;
    float m_Timer;
    bool m_HasAudioPlayed;
    int nSurvivors = 0;
    int maxSurvivors;
    bool killerWin = false;
    bool survivorWin = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tag))
        {
            ++nSurvivors;
            survivorManager.OnSurvivorArrive(other.gameObject);
            if (nSurvivors >= maxSurvivors) OnSurvivorsWin();
        }
    }

    void Start()
    {
        survivorManager = survivorManagerGO.GetComponent<SurvivorManager>();
        if (survivorManager.getNSurvivors() > 2)
            maxSurvivors = 2;
        else maxSurvivors = 1;
    }
    public void OnSurvivorsWin()
    {
        Debug.Log("Survivor");
        survivorWin = true;
    }
    public void OnKillerWin()
    {
        Debug.Log("Killer");
        killerWin = true;
    }
    private void Update()
    {
        if (killerWin) { EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio); }
        else if (survivorWin) EndLevel(exitBackgroundImageCanvasGroup, true, exitAudio);
    }

    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }

        m_Timer += Time.deltaTime;
        imageCanvasGroup.alpha = m_Timer / fadeDuration;

        if (m_Timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                Application.Quit();
            }
        }
    }
}
