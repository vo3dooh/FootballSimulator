
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LightRaysController : MonoBehaviour
{
    public CanvasGroup lightCanvasGroup; // Используем CanvasGroup для управления прозрачностью
    public float fadeDuration = 1f;

    private void Awake()
    {
        if (lightCanvasGroup != null)
        {
            lightCanvasGroup.alpha = 0f;
            lightCanvasGroup.gameObject.SetActive(true);
        }
    }

    public void FadeInLight()
    {
        if (gameObject.activeInHierarchy)
            StartCoroutine(FadeInCoroutine());
    }

    private IEnumerator FadeInCoroutine()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            if (lightCanvasGroup != null)
            {
                lightCanvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
            }
            yield return null;
        }
    }
}
