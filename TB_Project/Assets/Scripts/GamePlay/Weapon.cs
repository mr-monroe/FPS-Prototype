using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletRoot;
    [SerializeField] private float fireRate = 15f;

    private Camera mainCamera;
    private float range = 300f;
    private float nextTimeToFire = 0f;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public void Fire(float attackModificateur)
    {
        if (Time.time >= nextTimeToFire) 
        {
            nextTimeToFire = Time.time + 1f / fireRate;

            // calculate shooting point
            Vector3 hittedPoint;
            Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                hittedPoint = hit.point;
            }
            else hittedPoint = ray.GetPoint(range);

            Vector3 shootDirection = (hittedPoint - bulletRoot.position);

            // instantiate bullet prefab
            GameObject currentBullet = Instantiate(bulletPrefab, bulletRoot.position, bulletRoot.transform.rotation);
            currentBullet.GetComponent<Bullet>().AddFirePower(attackModificateur);
            currentBullet.transform.forward = shootDirection;
        }       
    }

    public void Equip() 
    {
        this.gameObject.SetActive(true);
    }

    public void Unequip() 
    {
        this.gameObject.SetActive(false);
    }
}
