using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private HealthView view;
    [SerializeField] private HealthModel model;

    [SerializeField] private DamageNumberController damageNumberPrefab;
    [SerializeField] private ParticleSystem healParticles;

    private int currentHealth;

    public void Start()
    {
        currentHealth = model.MaxHealth;

        view.SetHealth(currentHealth, model.MaxHealth);
    }

    public void TakeDamage(int damage)
    {
        UpdateHealth(-damage);

        view.Blink(Color.white);
        view.Shake();

        var damageNumber = Instantiate(damageNumberPrefab,  transform);
        damageNumber.Initialize(damage);
    }

    public void Heal(int amount)
    {
        UpdateHealth(amount);

        view.Blink(Color.green);

        healParticles.Play(true);
    }

    private void UpdateHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, model.MaxHealth);
        view.SetHealth(currentHealth, model.MaxHealth);
    }
}
