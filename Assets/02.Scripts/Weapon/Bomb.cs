using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject ExplosionEffectPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(ExplosionEffectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
