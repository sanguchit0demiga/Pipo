using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject config;
    public GameObject land;
    public GameObject story;
    public static bool storyOpen =false;
    private void Start()
    {
        config.SetActive(false);
        land.SetActive(true);
        story.SetActive(false);
       Cursor.visible = true;
    }
    public void Play()
    {
        SceneManager.LoadScene("Patio");
    }

    public void Story()
    {
        ReadStory();
    }

    void ReadStory()
    {
        storyOpen = true;
        land.SetActive(false);
        story.SetActive(true);
    }

    public void CloseWindow()
    {
        land.SetActive(true);
        config.SetActive(false);
        story.SetActive(false);
        storyOpen=false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Configuration()
    {
        storyOpen = true;
        land.SetActive(false);
        config.SetActive(true);
    }
}
