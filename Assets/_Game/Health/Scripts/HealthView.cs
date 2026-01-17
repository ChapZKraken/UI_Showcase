using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image delayedHealthBar;

    private Sequence sequenceHealth;
    private Tween shakeTween;
    private Tween blinkTween;

    public void SetHealth(int currentHealth, int maxHealth)
    {
        healthText.text = currentHealth + " / " + maxHealth;

        var fill = (float)currentHealth / maxHealth;

        

        sequenceHealth.Kill();
        sequenceHealth = DOTween.Sequence();
        sequenceHealth.SetEase(Ease.OutQuad);
        sequenceHealth.Append(healthBar.DOFillAmount(fill, 0.2f))
                      .Append(delayedHealthBar.DOFillAmount(fill, 0.2f).SetDelay(0.3f));
    }

    public void Blink(Color color)
    {
        healthBar.color = Color.red;

        blinkTween.Kill();
        blinkTween = healthBar.DOColor(color, 0.1f).SetLoops(2, LoopType.Yoyo);
    }

    public void Shake()
    {
        shakeTween.Kill();
        shakeTween = transform.DOShakePosition(0.3f, 10, 20, 90);
    }

    private void OnDestroy()
    {
        sequenceHealth.Kill();
        shakeTween.Kill();
        blinkTween.Kill();
    }
}
