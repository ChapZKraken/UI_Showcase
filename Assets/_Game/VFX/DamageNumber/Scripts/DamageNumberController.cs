using DG.Tweening;
using UnityEngine;

public class DamageNumberController : MonoBehaviour
{
    [SerializeField] private DamageNumberView view;

    public void Initialize(int value)
    {
        view.SetValue(value.ToString());
        view.Animate();
    }
}