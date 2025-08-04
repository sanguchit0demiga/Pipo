using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject story;
    public static bool storyOpen =false;
    private void Start()
    {
       Cursor.visible = true;
    }
    void Play()
    {
        SceneManager.LoadScene("Patio");
    }

    void Story()
    {
        ReadStory();
    }

    void ReadStory()
    {
        storyOpen = true;
        story.SetActive(true);
    }

    void CloseStory()
    {
        story.SetActive(false);
        storyOpen=false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
