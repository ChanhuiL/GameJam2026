using UnityEngine;

public class GameOverScreenHandlerScript : MonoBehaviour
{
    public AudioManager audioManager;
    public TransitionManager transitionManager;
    
    public void RestartGame()
    {
        transitionManager.StartFadeOut();
        audioManager.ChangeSceneWithFade("Title Scene");
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
