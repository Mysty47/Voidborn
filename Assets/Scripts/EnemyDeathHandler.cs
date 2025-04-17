using UnityEngine;

public class EnemyDeathHandler : MonoBehaviour
{
    public int experienceValue = 50;

    public void Die()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            player.SendMessage("GainExperienceFromEnemy", experienceValue);
        }
        
        Destroy(gameObject);
    }
}
