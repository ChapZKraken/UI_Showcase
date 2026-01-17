using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamageNumberView : MonoBehaviour
{
    [SerializeField] private TMP_Text valueText;

    [SerializeField] private float jumpPower;
    [SerializeField] private float duration;
    [SerializeField] private float horizontalOffset;
    [SerializeField] private float verticalOffset;
    [SerializeField] private float scaleUp;

    private Sequence sequence;

    public void SetValue(string value)
    {
        valueText.text = value;
    }

    public void Animate()
    {
        float randomHorizontalOffset = Random.Range(-horizontalOffset, horizontalOffset);
        Vector3 endPos = transform.localPosition + new Vector3(randomHorizontalOffset, verticalOffset, 0f);

        sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalJump(endPos, jumpPower, 1, duration))
                .Join(valueText.DOFade(0f, duration))
                .Join(transform.DOScale(transform.localScale * scaleUp, duration * 0.6f).SetEase(Ease.OutQuad).SetLoops(2, LoopType.Yoyo))
                .OnComplete(() =>
                {
                    Destroy(gameObject);
                });
    }

    private void OnDestroy()
    {
        sequence.Kill();
    }
}
