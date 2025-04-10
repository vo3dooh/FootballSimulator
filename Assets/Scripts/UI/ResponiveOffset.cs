using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(RectTransform))]
public class ResponsiveUIController : MonoBehaviour
{
    [Header("������� (� ��������� �� ������)")]
    [Range(0f, 1f)] public float offsetFromRight = 0.3f;
    [Range(0f, 1f)] public float offsetFromBottom = 0.0f;

    [Header("������� (� ��������� �� ������)")]
    [Range(0f, 1f)] public float widthPercent = 0.3f;
    [Range(0f, 1f)] public float heightPercent = 0f; // ���� 0 � ������������ ���������������

    private RectTransform rectTransform;
    private Canvas canvas;

    void Update()
    {
        if (rectTransform == null)
            rectTransform = GetComponent<RectTransform>();

        if (canvas == null)
            canvas = GetComponentInParent<Canvas>();

        if (canvas == null || rectTransform == null) return;

        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        float canvasWidth = canvasRect.rect.width;
        float canvasHeight = canvasRect.rect.height;

        // ��������� ������ � pivot � ������ ������ ����
        rectTransform.anchorMin = new Vector2(1, 0);
        rectTransform.anchorMax = new Vector2(1, 0);
        rectTransform.pivot = new Vector2(1, 0);

        // ������ �������
        float posX = -canvasWidth * offsetFromRight;
        float posY = canvasHeight * offsetFromBottom;
        rectTransform.anchoredPosition = new Vector2(posX, posY);

        // ������ �������
        float width = canvasWidth * widthPercent;

        if (heightPercent > 0)
        {
            // �������������� � �� ������
            float height = canvasHeight * heightPercent;
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        }
        else
        {
            // ��������� ��������� �����������
            Sprite sprite = GetComponent<UnityEngine.UI.Image>()?.sprite;
            if (sprite != null)
            {
                float aspect = sprite.rect.height / sprite.rect.width;
                float height = width * aspect;
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            }
        }
    }
}