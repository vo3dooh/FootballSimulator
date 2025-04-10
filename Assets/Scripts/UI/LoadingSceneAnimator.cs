using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LoadingSceneAnimator : MonoBehaviour
{
    [Header("UI Elements")]
    public Image whiteScreen;
    public GameObject leo;
    public Image background;

    [Header("Post Processing")]
    public Volume postProcessVolume;

    [Header("Animation Settings")]
    public float fadeDuration = 1f;
    public float vignetteTargetIntensity = 0.5f;

    [Header("Config")]
    public LeoAnimationConfig config;

    private Vignette vignette;
    private RectTransform leoRect;
    private RectTransform parentRect;
    private Vector2 startAnchoredPosition;
    private Vector2 targetAnchoredPosition;
    private Vector2 startSizeDelta;
    private Vector2 targetSizeDelta;

    private void Start()
    {
        leoRect = leo.GetComponent<RectTransform>();
        parentRect = leoRect.parent.GetComponent<RectTransform>();

        if (config != null && leoRect != null && parentRect != null)
        {
            float parentWidth = parentRect.rect.width;
            float parentHeight = parentRect.rect.height;

            // Позиции в процентах от родительского якоря
            startAnchoredPosition = new Vector2(
                parentWidth * config.startAnchorPosX,
                parentHeight * config.startAnchorPosY);

            targetAnchoredPosition = new Vector2(
                parentWidth * config.targetAnchorPosX,
                parentHeight * config.targetAnchorPosY);

            // Масштаб в процентах от родителя
            float startWidth = parentWidth * config.startScaleWidth;
            float startHeight = parentHeight * config.startScaleHeight;

            float targetWidth = parentWidth * config.targetScaleWidth;
            float targetHeight = parentHeight * config.targetScaleHeight;

            leoRect.anchoredPosition = startAnchoredPosition;
            leoRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, startWidth);
            leoRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, startHeight);

            startSizeDelta = new Vector2(startWidth, startHeight);
            targetSizeDelta = new Vector2(targetWidth, targetHeight);
        }

        whiteScreen.gameObject.SetActive(true);
        SetImageAlpha(whiteScreen, 1f);
        SetImageAlpha(background, 0f);
        leo.SetActive(false);

        if (postProcessVolume != null && postProcessVolume.profile.TryGet(out vignette))
        {
            vignette.intensity.value = 0f;
        }

        StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence()
    {
        yield return new WaitForSeconds(1f);

        leo.SetActive(true);
        StartCoroutine(MoveLeo());

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float progress = t / fadeDuration;
            SetImageAlpha(whiteScreen, Mathf.Lerp(1f, 0f, progress));
            SetImageAlpha(background, Mathf.Lerp(0f, 1f, progress));
            if (vignette != null)
                vignette.intensity.value = Mathf.Lerp(0f, vignetteTargetIntensity, progress);
            yield return null;
        }

        whiteScreen.gameObject.SetActive(false);
    }

    IEnumerator MoveLeo()
    {
        float elapsed = 0f;
        while (elapsed < config.moveDuration)
        {
            elapsed += Time.deltaTime;
            float progress = Mathf.SmoothStep(0f, 1f, elapsed / config.moveDuration);

            leoRect.anchoredPosition = Vector2.Lerp(startAnchoredPosition, targetAnchoredPosition, progress);
            leoRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Lerp(startSizeDelta.x, targetSizeDelta.x, progress));
            leoRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Lerp(startSizeDelta.y, targetSizeDelta.y, progress));

            yield return null;
        }
    }
    private void SetImageAlpha(Image img, float alpha)
    {
        Color c = img.color;
        c.a = alpha;
        img.color = c;
    }
}