using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particle;
    [SerializeField]
    private float radius;
    private float explosionRadius;
    public float explosionForce;
    public float explosionUpwardsModifier;
    
    [SerializeField]
    private LayerMask layersToCollideWith;
    private bool decaying = false;
    [SerializeField]
    private float decayTime;
    private float decayTimer;

    private void Update() {
        if(decaying)
        {
            // Repool after a set time
            if(decayTimer <= 0)
            {
                decaying = false;
                decayTimer = decayTime;
                ExplosionPool.Instance.ReturnToPool(this);
                return;
            }

            // Count down
            decayTimer -= Time.deltaTime;
        }
    }

    public void AdjustExplosion (float magnitude)
    {
        explosionRadius = radius;
        explosionRadius *= transform.lossyScale.x;

        particle.transform.localScale = new Vector3(magnitude, magnitude, magnitude);
        explosionRadius *= magnitude;
        explosionForce *= magnitude;
        explosionUpwardsModifier *= magnitude;
    }
    
    public void Explode()
    {
        decayTimer = decayTime;
        particle.Play();
        decaying = true;
        
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius, layersToCollideWith);
        foreach (Collider col in hitColliders)
        {
            // Do Damage
            DoDamage(col);
            // Apply Force
            Rigidbody rb = col.gameObject.GetComponent<Rigidbody>();
            if(rb != null)
            {
                if(!rb.gameObject.CompareTag("Player"))
                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpwardsModifier);
            }
        }
    }

    private void DoDamage(Collider colliderToDamage)
    {
        // Damage
        EnemyBehaviour eBehav = colliderToDamage.GetComponent<EnemyBehaviour>();
        if(eBehav)
            EnemyPool.Instance.ReturnToPool(eBehav);
    }
}