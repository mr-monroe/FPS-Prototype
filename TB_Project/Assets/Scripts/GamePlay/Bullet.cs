using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] protected float bulletSpeed = 100f;
    [SerializeField] protected int scorePoint = 1;
    [SerializeField] protected float attackPower = 5f;


    protected Rigidbody rigidBody;
    private float delay = 10f;
    private float AttackMod = 1f;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        StartCoroutine(DestroyBulletAfterTime());
    }

    private void LateUpdate()
    {
        BulletMove();
    }

    protected virtual void BulletMove() 
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnTargetHitted(collision);
    }

    protected virtual void OnTargetHitted(Collision collision) 
    {
        Target currentTarget = collision.gameObject.GetComponent<Target>();
        if (!GameObject.ReferenceEquals(currentTarget, null))
        {
            print(attackPower * AttackMod);
            currentTarget.TakeDamage(attackPower * AttackMod);
            ScoreCounter.IncreaseScore(scorePoint);
        }
        Destroy(gameObject);
    }

    public void AddFirePower(float mod) 
    {
        AttackMod = mod;
    }

    private IEnumerator DestroyBulletAfterTime()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
