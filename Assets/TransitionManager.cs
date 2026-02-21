using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
    private Image fadeImage;
    public float fadeDuration = 1.0f;
    
    private float originalAlpha;

    void Awake()
    {
        fadeImage = this.GetComponent<Image>();
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1f);
        originalAlpha = fadeImage.color.a;
        StartCoroutine(FadeIn());
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }
    
    private IEnumerator FadeIn()
    {
        // Fade In
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b,
                Mathf.Lerp(originalAlpha, 0, t / fadeDuration));
            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        // Fade Out
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b,
                Mathf.Lerp(0, originalAlpha, t / fadeDuration));
            yield return null;
        }
    }
}
