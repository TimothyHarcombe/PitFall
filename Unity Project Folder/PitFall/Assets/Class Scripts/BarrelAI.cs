/*using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class BarrelAI : MonoBehaviour {
    public Transform[] Points;

    public Transform Destination;
    public BarrelPath Projectile;

    public GameObject SpawnEffect;
    public float Speed;
    public float FireRate;
    public Animator Animator;
    public AudioClip SpawnProjectileSound;

    private float _nextShotInSeconds;

    public void Start()
    {
        _nextShotInSeconds = FireRate;
    }

    public void Update()
    {
        if ((_nextShotInSeconds -= Time.deltaTime) > 0)
            return;

        _nextShotInSeconds = FireRate;
        var projectile = (PathedProjectile)Instantiate(Projectile, transform.position, transform.rotation);
        projectile.Initalize(Destination, Speed);

        if (SpawnEffect != null)
            Instantiate(SpawnEffect, transform.position, transform.rotation);

        if (SpawnProjectileSound != null)
            AudioSource.PlayClipAtPoint(SpawnProjectileSound, transform.position);
    }

    public IEnumerator<Transform> GetPathEnumerator()
    {
        if (Points == null || Points.Length < 1)
            yield break;
        var direction = 1;
        var index = 0;
        while (true)
        {
            yield return Points[index];
            if (Points.Length == 1)
                continue;
            if (index <= 0)
                direction = 1;
            else if (index >= Points.Length - 1)
                Destroy(gameObject);
            index = index + direction;
        }
    }

    public void OnDrawGizmos()
    {

        if (Points == null || Points.Length < 2)
            return;

        var points = Points.Where(t => t != null).ToList();
        if (points.Count < 2)
            return;

        for (var i = 1; i < points.Count; i++)
        {
            Gizmos.DrawLine(points[i - 1].position, points[i].position);
        }
    }
}*/
