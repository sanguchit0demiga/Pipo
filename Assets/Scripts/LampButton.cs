using UnityEngine;

public class LampButton : MonoBehaviour
{
    [SerializeField] GameObject dark;

    public void LightsOn()
    {
        if (dark.activeSelf)
        {
            dark.SetActive(false);
        }

        else if (!dark.activeSelf)
        {
            dark.SetActive(true);
        }
    }
}
