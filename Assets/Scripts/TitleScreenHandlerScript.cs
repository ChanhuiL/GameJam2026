using UnityEngine;

public class TitleScreenHandlerScript : MonoBehaviour
{
    public AudioManager audioManager;
    
    public void StartGame()
    {
        audioManager.ChangeSceneWithFade("Main Scene");
    }

    public void Option()
    {
        
    }
    
    public void QuitGame()
        {
            // This line closes the actual built application
            Application.Quit();
    
            // This line allows the button to work inside the Unity Editor
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
    
            Debug.Log("Game is exiting...");
        }
}
