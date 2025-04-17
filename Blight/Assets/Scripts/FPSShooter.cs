using UnityEngine;

public class FPSShooter : MonoBehaviour
{
    public Camera cam;
    public GameObject projectile;
    public Transform LHFirePoint, RHFirePoint;
    
    private Vector3 destination;
    private bool leftHand;
    private float projectileSpeed = 30;
    public float arcRange = 1;
    
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // right click
        {
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(1000);
        }

        if (leftHand)
        {
            leftHand = false;
            InstantiateProjectile(LHFirePoint);
        }
        else
        {
            leftHand = true;
            InstantiateProjectile(RHFirePoint);
        }
    }

    void InstantiateProjectile(Transform firePoint)
    {
       var projectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity); 
       projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectileSpeed;
       
       iTween.PunchPosition(projectileObj, new Vector3 (Random.Range(-arcRange, arcRange), Random.Range(-arcRange, arcRange), 0), Random.Range(0.5f, 2));
    }
}
