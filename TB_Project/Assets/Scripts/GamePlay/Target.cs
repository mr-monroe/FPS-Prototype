using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Target : MonoBehaviour
{
    [SerializeField] private float health = 50f;

    public void TakeDamage(float dmgAmount) 
    {
        health -= dmgAmount;
        if (health <= 0) 
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
