using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    [Header("Abilitiy 1")]
    public Image abilityImage1;
    public Text abilityText1;
    public KeyCode ability1Key;
    public float ability1Cooldown = 5;

    public float ability1ManaCost = 30;
    public Canvas ability1Canvas;
    public Image ability1Skillshot;
    
    [Header("Abilitiy 2")]
    public Image abilityImage2;
    public Text abilityText2;
    public KeyCode ability2Key;
    public float ability2Cooldown = 7;
    public float ability2ManaCost = 70;
    
    [Header("Abilitiy 3")]
    public Image abilityImage3;
    public Text abilityText3;
    public KeyCode ability3Key;
    public float ability3Cooldown = 10;
    
    public float ability3ManaCost = 40;
    public Canvas ability3Canvas;
    public Image ability3Cone;
    
    private bool isAbility1Cooldown;
    private bool isAbility2Cooldown;
    private bool isAbility3Cooldown;

    private float currentAbility1Cooldown;
    private float currentAbility2Cooldown;
    private float currentAbility3Cooldown;
    
    private Vector3 position;
    private RaycastHit hit;
    private Ray ray;
    
    public ManaSystem manaSystem;
    
    void Start()
    {
        manaSystem = GetComponent<ManaSystem>();
        
        abilityImage1.fillAmount = 0;
        abilityImage2.fillAmount = 0;
        abilityImage3.fillAmount = 0;
        
        abilityText1.text = "";
        abilityText2.text = "";
        abilityText3.text = "";
        
        ability1Skillshot.enabled = false;
        ability3Cone.enabled = false;
        
        ability1Canvas.enabled = false;
        ability3Canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        Ability1Input();
        Ability2Input();
        Ability3Input();
        
        AbilityCooldown(ability1Cooldown, ability1ManaCost, ref currentAbility1Cooldown, ref isAbility1Cooldown, abilityImage1, abilityText1);
        AbilityCooldown(ability2Cooldown, ability2ManaCost, ref currentAbility2Cooldown, ref isAbility2Cooldown, abilityImage2, abilityText2);
        AbilityCooldown(ability3Cooldown, ability1ManaCost, ref currentAbility3Cooldown, ref isAbility3Cooldown, abilityImage3, abilityText3);

        Ability1Canvas();
        Ability3Canvas();
    }

    private void Ability1Canvas()
    {
        if (ability1Skillshot.enabled)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            }
            
            Quaternion ab1Canvas = Quaternion.LookRotation(position - transform.position);
            ab1Canvas.eulerAngles = new Vector3(0, ab1Canvas.eulerAngles.y, ab1Canvas.eulerAngles.z);
            
            ability1Canvas.transform.rotation = Quaternion.Lerp(ab1Canvas, ability1Canvas.transform.rotation, 0);
        }
    }
    
    private void Ability3Canvas()
    {
        if (ability3Cone.enabled)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            }
            
            Quaternion ab3Canvas = Quaternion.LookRotation(position - transform.position);
            ab3Canvas.eulerAngles = new Vector3(0, ab3Canvas.eulerAngles.y, ab3Canvas.eulerAngles.z);
            
            ability3Canvas.transform.rotation = Quaternion.Lerp(ab3Canvas, ability3Canvas.transform.rotation, 0);
        }
    }
    
    

    private void Ability1Input()
    {
        if (Input.GetKeyDown(ability1Key) && !isAbility1Cooldown && manaSystem.CanAffordAbility(ability1ManaCost))
        {
            ability1Canvas.enabled = true;
            ability1Skillshot.enabled = true;
            
            ability3Canvas.enabled = false;
            ability3Cone.enabled = false;

            Cursor.visible = true;
        }
        
        if (ability1Skillshot.enabled && Input.GetMouseButtonDown(0))
        {
            if (manaSystem.CanAffordAbility(ability1ManaCost))
            {
                manaSystem.UseAbility(ability1ManaCost);
                isAbility1Cooldown = true;
                currentAbility1Cooldown = ability1Cooldown;
                                
                ability1Canvas.enabled = false;
                ability1Skillshot.enabled = false;
            }
        }
    }
    
    private void Ability2Input()
    {
        if (Input.GetKeyDown(ability2Key) && !isAbility2Cooldown && manaSystem.CanAffordAbility(ability2ManaCost))
        {
            isAbility2Cooldown = true;
            currentAbility2Cooldown = ability2Cooldown;
        }
    }
    private void Ability3Input()
    {
        if (Input.GetKeyDown(ability3Key) && !isAbility3Cooldown && manaSystem.CanAffordAbility(ability3ManaCost))
        {
            ability3Canvas.enabled = true;
            ability3Cone.enabled = true;
            
            ability1Canvas.enabled = false;
            ability1Skillshot.enabled = false;
            
            Cursor.visible = true;
        }
        
        if (ability3Cone.enabled && Input.GetMouseButtonDown(0))
        {
            if (manaSystem.CanAffordAbility(ability3ManaCost))
            {
                manaSystem.UseAbility(ability3ManaCost);
                isAbility3Cooldown = true;
                currentAbility3Cooldown = ability3Cooldown;
                                
                ability3Canvas.enabled = false;
                ability3Cone.enabled = false;
            }
            
        }
    }

    private void AbilityCooldown(float abilityCooldown, float abilityManaCost, ref float currentCooldown, ref bool isCooldown, Image skillImage, Text skillText)
    {
        if (isCooldown)
        {
            currentCooldown -= Time.deltaTime;

            if (currentCooldown <= 0f)
            {
                isCooldown = false;
                currentCooldown = 0;
            }

            if (skillImage != null)
            {
                skillImage.color = Color.grey;
                skillImage.fillAmount = 1;
            }

            if (skillText != null)
            {
                skillText.text = Mathf.Ceil(currentCooldown).ToString();
            }
        }
        else
        {
            if (manaSystem.CanAffordAbility(abilityManaCost))
            {
                if (skillImage != null)
                {
                    skillImage.color = Color.grey;
                    skillImage.fillAmount = 0;
                }

                if (skillText != null)
                {
                    skillText.text = " ";
                }
            }
            else
            {
                if (skillImage != null)
                {
                    skillImage.color = Color.red;
                    skillImage.fillAmount = 1;
                }

                if (skillText != null)
                {
                    skillText.text = "X";
                }
            }
        }
    }
}
