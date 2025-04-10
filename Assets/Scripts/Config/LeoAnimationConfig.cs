using UnityEngine;

[CreateAssetMenu(fileName = "LeoAnimationConfig", menuName = "Configs/Leo Animation Config")]
public class LeoAnimationConfig : ScriptableObject
{
    [Header("Позиция (в % от якорной области родителя)")]

    [Range(-100, 100)]
    public float startAnchorPosX;

    [Range(-100, 100)]
    public float startAnchorPosY;

    [Range(-100, 100)]
    public float targetAnchorPosX;

    [Range(-100, 100)]
    public float targetAnchorPosY;

    [Header("Масштаб (0 = 0%, 1 = 100%)")]

    [Range(0f, 1f)]
    public float startScaleWidth;

    [Range(0f, 1f)]
    public float startScaleHeight;

    [Range(0f, 1f)]
    public float targetScaleWidth;

    [Range(0f, 1f)]
    public float targetScaleHeight;

    [Header("Продолжительность анимации (в секундах)")]

    [Range(0.1f, 10f)]
    public float moveDuration = 1f;
}