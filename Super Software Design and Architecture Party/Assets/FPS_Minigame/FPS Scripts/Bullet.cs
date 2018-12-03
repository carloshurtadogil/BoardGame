using System.Collections;
using UnityEngine;

namespace S3
{
    public class Bullet : MonoBehaviour
    {
        void OnCollisionEnter(Collision collision)
        {
            var hit = collision.gameObject;
            var health = hit.GetComponent<Health>();

            if (health != null)
            {
                health.TakeDamage(10);
            }

            Destroy(gameObject);
        }
    }
}

