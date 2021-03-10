using UnityEngine;
using System.Collections;

public class ExplosiveBullet : Bullet
{
    [SerializeField] float radiusOfDestruction = 3f;
    [SerializeField] float explosionForce = 1f;
    private void start()
    {
        print("exp bullet position " + transform.position);
    }

    protected override void BulletMove()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
    }

    protected override void OnTargetHitted(Collision collision)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radiusOfDestruction);
        foreach (Collider nearbyObject in colliders) 
        {
            Target currentTarget = nearbyObject.GetComponent<Target>();
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (!GameObject.ReferenceEquals(currentTarget, null) && !GameObject.ReferenceEquals(rb, null)) 
            {
                rb.AddExplosionForce(explosionForce, transform.position, radiusOfDestruction);
                currentTarget.TakeDamage((attackPower * Vector3.Distance(transform.position, currentTarget.transform.position)) / radiusOfDestruction);
                ScoreCounter.IncreaseScore(scorePoint);
            }
        }
        Destroy(gameObject);
    }
}
