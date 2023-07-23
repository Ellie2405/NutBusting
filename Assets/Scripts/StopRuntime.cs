using UnityEngine;
using UnityEngine.UI;

public class StopRuntime : MonoBehaviour
{
    private void Start()
    {
        // Get the Button component attached to this GameObject
        Button button = GetComponent<Button>();

        // Add a listener to the button's onClick event
        button.onClick.AddListener(StopGame);
    }

    private void StopGame()
    {
        // Stop the runtime
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
