using UnityEngine;

public class GameOverScreenHandlerScript : MonoBehaviour
{
    public AudioClip clickSFX;
    public AudioManager audioManager;
    public TransitionManager transitionManager;
    
    public void RestartGame()
    {
        GetComponent<AudioSource>().clip = clickSFX;
        GetComponent<AudioSource>().Play();
        transitionManager.StartFadeOut();
        audioManager.ChangeSceneWithFade("Title Scene");
    }
    
    public void QuitGame()
        {
            GetComponent<AudioSource>().clip = clickSFX;
            GetComponent<AudioSource>().Play();
            // This line closes the actual built application
            Application.Quit();
    
            // This line allows the button to work inside the Unity Editor
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
    
            Debug.Log("Game is exiting...");
        }
}
