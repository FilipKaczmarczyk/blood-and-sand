using UnityEngine;
using UnityEngine.UI;

public class CharacterWorldUI : MonoBehaviour
{
    [SerializeField] private Image healthBarImage;
    [SerializeField] private HealthSystem healthSystem;

    private void Start()
    {
        healthSystem.OnHealthChange += (sender, args) => UpdateHealthBar();
        
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        healthBarImage.fillAmount = healthSystem.GetHealthNormalized();
    }
}