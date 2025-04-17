using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ExperienceSystem : MonoBehaviour
{
    public int currentLevel = 1;
    public int currentExp = 0;
    public int expToLevelUp = 100;
    public int expIncreaseFactor = 2;

    public Slider expBarSlider;
    public Text levelText2D;
    public TextMeshProUGUI levelText3D;

    void Update()
    {
        UpdateUI();
    }

    private void GainExperienceFromEnemy(int amount)
    {
        GainExperience(amount);
    }

    private void GainExperience(int amount)
    {
        currentExp += amount;

        while (currentExp >= expToLevelUp)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentLevel++;
        currentExp -= expToLevelUp;
        expToLevelUp *= expIncreaseFactor;
    }

    private void UpdateUI()
    {
        if (expBarSlider != null)
        {
            expBarSlider.maxValue = expToLevelUp;
            expBarSlider.value = currentExp;
        }

        if (levelText2D != null)
        {
            levelText2D.text = currentLevel.ToString();
        }

        if (levelText3D != null)
        {
            levelText3D.text = currentLevel.ToString();
        }
    }
}
