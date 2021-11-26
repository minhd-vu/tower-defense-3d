using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float range = 3f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public GameObject projectile;
    public Transform firePoint;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        // Rotate towards target
        Vector3 dir = target.position - transform.position;
        Quaternion quaternion = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, quaternion, Time.deltaTime * 10f).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject child = Instantiate(projectile, firePoint.position, firePoint.rotation);
        Projectile p = child.GetComponent<Projectile>();

        if (p != null)
        {
            p.Seek(target);
        }

        // child.transform.parent = this.transform;
    }

    void UpdateTarget()
    {
        // Find the nearest enemy
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float min = Mathf.Infinity;
        GameObject nearest = null;
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < min)
            {
                min = distance;
                nearest = enemy;
            }
        }

        // Set the target to the nearest enemy if it is in range
        target = nearest != null && min <= range ? nearest.transform : null;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
