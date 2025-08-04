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
    public void Play()
    {
        SceneManager.LoadScene("Patio");
    }

    public void Story()
    {
        ReadStory();
    }

    public void ReadStory()
    {
        storyOpen = true;
        story.SetActive(true);
    }

    public void CloseStory()
    {
        story.SetActive(false);
        storyOpen=false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
