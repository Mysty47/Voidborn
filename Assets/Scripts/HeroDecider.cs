using UnityEngine;

public class HeroDecider : MonoBehaviour
{
    public static int decision = 1;
    private int count = 2;
    
    [Header("Heroes")]
    
    public GameObject Hero1;
    public Camera mainCamera1;
    
    public GameObject Hero2;
    public Camera mainCamera2;
    
    
    public MenuController mc;
    
    
    public void setDecision1() {
       decision = 1;
    }

    public void setDecision2()
    {
        decision = 2;
    }
    
    
    void Start()
    {
        if (decision < 1 && decision > count)
        {
            decision = 1;
        }
        if (decision == 1)
        {
            Hero1.SetActive(true);
            mainCamera1.enabled = true;
            Hero2.SetActive(false);
            mainCamera2.enabled = false;
        }
        else if (decision == 2)
        {
            Hero1.SetActive(false);
            mainCamera1.enabled = false;
            Hero2.SetActive(true);
            mainCamera2.enabled = true;
        }
        
    }
}
