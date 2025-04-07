using System;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float health;
    public float damage;
    public float attackSpeed;

    private void Update()
    {
        Debug.Log(health);
    }

    public void TakeDamage(GameObject target, float damage)
    {
        target.GetComponent<Stats>().health -= damage;

        if (target.GetComponent<Stats>().health <= 0)
        {
            Destroy(target.gameObject);
        }
    }
}
