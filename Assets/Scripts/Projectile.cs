using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    public float speed = 5f;
    public GameObject hitEffect;

    public void Seek(Transform target)
    {
        this.target = target;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distance = speed * Time.deltaTime;
        // Check for collision
        if (dir.magnitude <= distance)
        {
            HitTarget();
            return;
        }

        // Move projectile towards target
        transform.Translate(dir.normalized * distance, Space.World);

        // Rotate projectile towards target
        Quaternion quaternion = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, quaternion, Time.deltaTime * 10f).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void HitTarget()
    {
        // GameObject particle = Instantiate(hitEffect, transform.position, transform.rotation);
        // Destroy(particle, 2f);
        Destroy(gameObject);
        return;
    }
}
