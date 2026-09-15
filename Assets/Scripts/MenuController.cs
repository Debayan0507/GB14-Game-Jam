using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public Button playButton; // drag Play button here in Inspector
    public Button creditButton;
    public Button backButton;
    public GameObject selector;
    public GameObject creditsPanel;

    void Start()
    {
        // Highlight Play button at start
        EventSystem.current.SetSelectedGameObject(playButton.gameObject);
        creditsPanel.SetActive(false);
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
            targetButton.position.x - 50, // offset to the left
            targetButton.position.y,
            targetButton.position.z
        );
    }
}
