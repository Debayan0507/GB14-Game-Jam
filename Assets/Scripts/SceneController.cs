using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneController : MonoBehaviour
{
    public Animator transition;
    public TextMeshProUGUI dayCountText;
    public TextMeshProUGUI endText;
    public CustomerSpawn customerSpawnScript;
    public int dayCount = 1;
    public float nextSceneDelay = 3f;
    private static SceneController sceneControllerScript;
    void Awake()
    {
        endText = GameObject.Find("EndText").GetComponent<TextMeshProUGUI>();
        endText.text = string.Empty;
        dayCountText = GameObject.Find("DayCount Text").GetComponent<TextMeshProUGUI>();
        transition = GameObject.Find("Black Image").GetComponent<Animator>();
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            StartCoroutine(FirstDay());
        }
        if (sceneControllerScript == null)
        {
            sceneControllerScript = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dayCountText = GameObject.Find("DayCount Text").GetComponent<TextMeshProUGUI>();
        transition = GameObject.Find("Black Image").GetComponent<Animator>();
        customerSpawnScript = GameObject.Find("Customer Spawn Point").GetComponent<CustomerSpawn>();
        if (customerSpawnScript.i >= customerSpawnScript.customerPrefab.Length && GameObject.FindWithTag("Customer") == null && SceneManager.GetActiveScene().buildIndex != 5)
        {
            StartCoroutine(NextScene());
        }
        if(customerSpawnScript.i >= customerSpawnScript.customerPrefab.Length && GameObject.FindWithTag("Customer") == null && SceneManager.GetActiveScene().buildIndex == 5)
        {
            StartCoroutine(LastDay());
        }
    }
    IEnumerator NextScene()
    {   
        customerSpawnScript.i = 0;
        yield return new WaitForSeconds(nextSceneDelay);
        transition.SetTrigger("End");
        dayCountText.text = $"Day {++dayCount}";
        yield return new WaitForSeconds(2f);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        dayCountText.text = string.Empty;
    }
    IEnumerator FirstDay()
    { 
        dayCountText.text = $"Day {dayCount}";
        yield return new WaitForSeconds(1f);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);   
        dayCountText.text = string.Empty;
    }
    IEnumerator LastDay()
    {
        customerSpawnScript.i = 0;
        yield return new WaitForSeconds(nextSceneDelay);
        transition.SetTrigger("End");
        yield return new WaitForSeconds(1f);
        endText.text = "<b>THE END</b>";
        yield return new WaitForSeconds(3f);
        endText.text = "Thanks for Playing!";
        yield return new WaitForSeconds(3f);
        endText.text = string.Empty;
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
