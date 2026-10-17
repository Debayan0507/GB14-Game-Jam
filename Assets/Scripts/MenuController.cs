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
    public Button backButton;
    public GameObject selector;
    public GameObject creditsPanel;
    public Animator startTransition;
    public TextMeshProUGUI storyText;
    public float transitionDelay = 5f;
    public string[] story;

    void Start()
    {
        // Highlight Play button at start
        EventSystem.current.SetSelectedGameObject(playButton.gameObject);
        creditsPanel.SetActive(false);
        startTransition = GameObject.Find("Black Image").GetComponent<Animator>();
        storyText = GameObject.Find("StoryText").GetComponent<TextMeshProUGUI>();
        storyText.text = string.Empty;
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

    }
    public void Play()
    {
        StartCoroutine(StartTransition());
    }
    IEnumerator StartTransition()
    {
        startTransition.SetTrigger("End");
        yield return new WaitForSeconds(transitionDelay);
        for(int i = 0; i < story.Length; i++)
        {
            if(i >= story.Length - 2)
            {
                //Make the line bold
                storyText.text += "\n<b>" + story[i] + "</b>\n";
                yield return new WaitForSeconds(2f);
            }
            else
            {
                storyText.text += story[i] + "\n";
                yield return new WaitForSeconds(2f);

            }
        }
        storyText.text = string.Empty;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(1);
    }
    public void Credits()
    {
        creditsPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(backButton.gameObject);
        MoveArrow(backButton.gameObject.transform);
        playButton.gameObject.SetActive(false);
        creditButton.gameObject.SetActive(false);
    }
    public void Back()
    {
        playButton.gameObject.SetActive(true);
        creditButton.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(playButton.gameObject);
        creditsPanel.SetActive(false);
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
