using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UI_ModuleAnimator : MonoBehaviour
{
    public enum SlideDirection
    {
        None,
        Top,
        Bottom,
        Left,
        Right
    }

    [Header("Animation Settings")]
    [Header("Fade Settings")]
    public float fadeDuration = 0.5f;

    [Header("Slide Settings")]
    public SlideDirection slideDirection = SlideDirection.None;
    public float slideDistance = 100f;
    public float slideDuration = 0.5f;
    public Ease slideEase = Ease.OutCubic;

    private CanvasGroup canvasGroup;
    private Tween currentTween;

    private Vector3 originalPosition;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = transform.localPosition;
    }

    public void Show()
    {
        gameObject.SetActive(true);

        currentTween?.Kill();
        canvasGroup.alpha = 0f;
        transform.localPosition = GetStartPosition();

        Sequence sequence = DOTween.Sequence();
        sequence.Append(canvasGroup.DOFade(1f, fadeDuration).SetEase(Ease.Linear));
        sequence.Join(transform.DOLocalMove(originalPosition, slideDuration).SetEase(slideEase));

        currentTween = sequence;
    }

    public void Hide()
    {
        // Fade and deactivate
        currentTween?.Kill();
        currentTween = canvasGroup.DOFade(0f, fadeDuration).SetUpdate(true)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
    }

    public void HideImmediate()
    {
        currentTween?.Kill();
        canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }

    public void Delete()
    {
        // Fade and destroy
        currentTween?.Kill();
        currentTween = canvasGroup.DOFade(0f, fadeDuration).SetUpdate(true)
            .OnComplete(() =>
            {
                Destroy(gameObject);
            });
    }

    public void DeleteImmediate()
    {
        // Immediately delete without animation
        currentTween?.Kill();
        Destroy(gameObject);
    }

    private Vector3 GetStartPosition()
    {
        Vector3 offset = originalPosition;

        switch (slideDirection)
        {
            case SlideDirection.Left:
                offset.x -= slideDistance;
                break;
            case SlideDirection.Right:
                offset.x += slideDistance;
                break;
            case SlideDirection.Top:
                offset.y += slideDistance;
                break;
            case SlideDirection.Bottom:
                offset.y -= slideDistance;
                break;
        }

        return offset;
    }
}