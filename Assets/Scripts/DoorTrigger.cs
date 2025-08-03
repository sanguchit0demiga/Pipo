using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour
{
    public Animator doorAnimator; 
    public string animationTriggerName = "Open"; 
    public float waitBeforeSceneChange = 3f; 
    public string sceneToLoad = "Bedroom"; 

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            doorAnimator.SetTrigger(animationTriggerName);
            StartCoroutine(ChangeSceneAfterDelay());
        }
    }

    private System.Collections.IEnumerator ChangeSceneAfterDelay()
    {
        yield return new WaitForSeconds(waitBeforeSceneChange);
        SceneManager.LoadScene(sceneToLoad);
    }
}
