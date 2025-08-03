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

            if (doorAnimator != null && !string.IsNullOrEmpty(animationTriggerName))
            {
                doorAnimator.SetTrigger(animationTriggerName);
            }
            else
            {
                Debug.LogWarning("Animator o nombre de trigger no asignado en " + gameObject.name);
            }

            StartCoroutine(ChangeSceneAfterDelay());
        }
    }

    private System.Collections.IEnumerator ChangeSceneAfterDelay()
    {
        yield return new WaitForSeconds(waitBeforeSceneChange);

        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("Nombre de escena a cargar no asignado en " + gameObject.name);
        }
    }
}
