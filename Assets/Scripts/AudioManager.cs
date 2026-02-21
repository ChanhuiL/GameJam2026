using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    public float fadeDuration = 1.0f;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(FadeIn());
    }

    public void ChangeSceneWithFade(string sceneName)
    {
        StartCoroutine(FadeOutAndLoad(sceneName));
    }
    
    private IEnumerator FadeIn()
    {
        float targetVolume = audioSource.volume;

        // Fade In
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(0, targetVolume, t / fadeDuration);
            yield return null;
        }
    }

    private IEnumerator FadeOutAndLoad(string sceneName)
    {
        float startVolume = audioSource.volume;

        // Fade Out
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }

        audioSource.volume = 0;
        SceneManager.LoadScene(sceneName);
    }
}
