using UnityEngine;
using UnityEngine.EventSystems;

public class CardHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalPosition;
    private Vector3 hoverPosition;
    private Vector3 originalScale;
    private Vector3 hoverScale = new Vector3(1.7f, 1.7f, 1.0f);
    private float hoverDuration = 0.3f;

    private void Start()
    {
        // Store original position and scale for resetting
        originalPosition = transform.position;
        originalScale = transform.localScale;

        // Calculate hover position slightly above the original position
        hoverPosition = originalPosition + new Vector3(0f, 0.2f, -1f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Play hover animation
        LeanTween.move(gameObject, hoverPosition, hoverDuration).setEase(LeanTweenType.easeOutQuad);
        LeanTween.scale(gameObject, hoverScale, hoverDuration).setEase(LeanTweenType.easeOutQuad);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Play exit animation
        LeanTween.move(gameObject, originalPosition, hoverDuration).setEase(LeanTweenType.easeOutQuad);
        LeanTween.scale(gameObject, originalScale, hoverDuration).setEase(LeanTweenType.easeOutQuad);
    }
}

