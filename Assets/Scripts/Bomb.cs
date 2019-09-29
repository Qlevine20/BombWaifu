﻿using UnityEngine;

public class Bomb : MonoBehaviour
{
    private bool flying = false;
    private Collider owner;
    public Collider myCollider;
    private Vector3 lastPos;
    private float flightTimer = 0;
    private Vector3 velocity = Vector3.zero;
    private Vector3 direction = Vector3.zero;
    public float acceleration;
    private Vector3 straightPathPos = Vector3.zero;
    private Vector3 startPos = Vector3.zero;
    private float distanceTravelled = 0;
    private float distance = 0;
    private float duration = 0;
    public AnimationCurve flightPath;
    private float curveScaler;
    public float defaultCurveScaler;

    public Transform gfx;
    public float rotationSpeed;

    public TrailRenderer trail;

    void Start()
    {
        lastPos = transform.position;
    }

    void Update()
    {
        if(flying)
        {
            // Position
            flightTimer += Time.deltaTime;
            velocity += direction * acceleration * Time.deltaTime;
            straightPathPos += velocity * Time.deltaTime;
            distanceTravelled = (startPos - straightPathPos).magnitude;
            var pos = straightPathPos + transform.up * flightPath.Evaluate(distanceTravelled/distance) * curveScaler;

            RaycastHit[] hits = Physics.RaycastAll(lastPos, pos - lastPos, Vector3.Distance(lastPos, pos));
            for (int i = hits.Length - 1; i >= 0; i--)
            {
                // Don't collide with self
                if(hits[i].collider.gameObject == gameObject || hits[i].collider == owner)
                    continue;

                transform.position = hits[i].point;
                Explode();
            }

            transform.position = pos;
            lastPos = transform.position;


            // Rotation
            gfx.Rotate(Vector3.forward, rotationSpeed, Space.Self);

        }
    }

    public void Throw(Vector3 point, Collider thrownByCollider)
    {
        flying = true;
        owner = thrownByCollider;
        Physics.IgnoreCollision(myCollider, thrownByCollider);

        startPos = transform.position;
        straightPathPos = transform.position;
        lastPos = transform.position;
        direction = (point - transform.position).normalized;
        distance = Vector3.Distance(transform.position, point);
        duration = Mathf.Sqrt(Vector3.Distance(transform.position, point) / (.5f * acceleration));

        curveScaler = defaultCurveScaler * distance;
    }

    private void Explode()
    {
        Explosion newExplosion = ExplosionPool.Instance.Get();
        newExplosion.transform.position = transform.position;
        newExplosion.transform.rotation = transform.rotation;
        newExplosion.transform.localScale = transform.localScale;
        newExplosion.gameObject.SetActive(true);

        newExplosion.AdjustExplosion(Random.Range(0.5f, 1f));
        newExplosion.Explode();
        ReturnBomb();
    }

    protected virtual void ReturnBomb()
    {
        flying = false;
        velocity = Vector3.zero;
        direction = Vector3.zero;
        straightPathPos = Vector3.zero;
        startPos = Vector3.zero;
        distanceTravelled = 0;
        distance = 0;
        duration = 0;
        flightTimer = 0;
        trail.Clear();        
    }

    void OnCollisionEnter(Collision other) {
        if(other.collider.gameObject.CompareTag("Weebs"))
        {
            Explode();
        }
    }
}
