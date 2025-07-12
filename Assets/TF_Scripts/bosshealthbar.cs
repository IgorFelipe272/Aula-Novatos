using UnityEngine;
using UnityEngine.UI;

public class bosshealthbar : MonoBehaviour
{
    private Slider slider;

    void Awake()
    {
        // Tenta pegar o componente Slider no mesmo GameObject
        slider = GetComponent<Slider>();

        if (slider == null)
        {
            Debug.LogError("Slider não encontrado no objeto " + gameObject.name);
        }
    }

    public void UpdateHealthBarBoss(float currentHealth, float maxHealth)
    {
        if (slider != null)
        {
            slider.value = currentHealth / maxHealth;
        }
    }
}
