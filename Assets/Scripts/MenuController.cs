using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuController : MonoBehaviour
{
    public Button playButton; // drag Play button here in Inspector
    public Button creditButton;
    public Button quitButton;
    public Button nextButton;
    public Button backButton;
    public GameObject selector;
    public GameObject creditsPanel;
    public GameObject attributesPanel;
    public Animator startTransition;
    public TextMeshProUGUI storyText;
    public AudioSource menuAudioSource;
    public AudioClip menuAudioClip;
    public AudioClip transitionAudioClip;
    public float transitionDelay = 3f;
    public string[] story;

    void Start()
    {
        // Highlight Play button at start
        EventSystem.current.SetSelectedGameObject(playButton.gameObject);
        creditsPanel.SetActive(false);
        attributesPanel.SetActive(false);
        startTransition = GameObject.Find("Black Image").GetComponent<Animator>();
        storyText = GameObject.Find("StoryText").GetComponent<TextMeshProUGUI>();
        storyText.text = string.Empty;
        menuAudioSource.clip = menuAudioClip;
        menuAudioSource.Play();
    }
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == playButton.gameObject)
        {
            MoveArrow(playButton.gameObject.transform);
        }
        else if (EventSystem.current.currentSelectedGameObject == creditButton.gameObject)
        {
            MoveArrow(creditButton.gameObject.transform);
        }
        else if (EventSystem.current.currentSelectedGameObject == quitButton.gameObject)
        {
            MoveArrow(quitButton.gameObject.transform);
        }
    }
    public void Play()
    {
        StartCoroutine(StartTransition());
    }
    IEnumerator StartTransition()
    {
        menuAudioSource.Stop();
        menuAudioSource.clip = transitionAudioClip;
        startTransition.SetTrigger("End");
        menuAudioSource.pitch = 2f;
        menuAudioSource.Play();
        yield return new WaitForSeconds(transitionDelay);
        for(int i = 0; i < story.Length; i++)
        {
            if(i >= story.Length - 2)
            {
                //Make the line bold
                storyText.text += "\n<b>" + story[i] + "</b>\n";
                yield return new WaitForSeconds(3f);
            }
            else
            {
                storyText.text += story[i] + "\n";
                yield return new WaitForSeconds(5f);

            }
        }
        storyText.text = string.Empty;
        menuAudioSource.Stop();
        menuAudioSource.pitch = 1f;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(1);
    }
    public void Credits()
    {
        creditsPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(nextButton.gameObject);
        MoveArrow(nextButton.gameObject.transform);
        playButton.gameObject.SetActive(false);
        creditButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
    }
    public void Next()
    {
        EventSystem.current.SetSelectedGameObject(backButton.gameObject);
        MoveArrow(backButton.gameObject.transform);
        creditsPanel.SetActive(false);
        attributesPanel.SetActive(true);
    }
    public void Back()
    {
        playButton.gameObject.SetActive(true);
        creditButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(playButton.gameObject);
        attributesPanel.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
    }
    void MoveArrow(Transform targetButton)
    {
        // Position arrow next to the selected button
        selector.transform.position = new Vector3(
            targetButton.position.x - 150, // offset to the left
            targetButton.position.y,
            targetButton.position.z
        );
    }
}
