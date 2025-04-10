using UnityEngine;
using UnityEngine.UI;

public class ManaSystem : MonoBehaviour
{
    public float maxMana = 100;
    public float startingMana = 100;
    public float manaRegenRate = 5;

    public Slider manaBar2d;
    public Slider manaBar3d;
    public Text manaText2d;

    public float currentMana;
    void Start()
    {
        currentMana = startingMana;
        UpdateManaUI();
    }

    // Update is called once per frame
    void Update()
    {
        RegenerateMana();
    }

    private void RegenerateMana()
    {
        if (currentMana < maxMana)
        {
            currentMana += manaRegenRate * Time.deltaTime;
            currentMana = Mathf.Clamp(currentMana, 0f, maxMana);
            UpdateManaUI();
        }
    }

    public void UpdateManaUI()
    {
        if (manaBar2d != null)
        {
            manaBar2d.value = currentMana / maxMana;
        }

        if (manaBar3d != null)
        {
            manaBar3d.value = currentMana / maxMana;
        }

        if (manaText2d != null)
        {
            manaText2d.text = Mathf.RoundToInt(currentMana).ToString() + " / " + maxMana;
        }
    }

    public bool CanAffordAbility(float abilityCost)
    {
        return currentMana >= abilityCost;
    }

    public void UseAbility(float abilityCost)
    {
        currentMana -= abilityCost;
        currentMana = Mathf.Clamp(currentMana, 0f, maxMana);
        UpdateManaUI();
    }
}
