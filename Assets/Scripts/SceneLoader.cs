using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void OnMenuPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
