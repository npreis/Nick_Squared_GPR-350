using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    public List<Particle2D> particles = new List<Particle2D>();
    List<Particle2D> particlesToDelete = new List<Particle2D>();
    public GameObject particlePrefab;

    public void AddParticle(Particle2D par)
    {
        if (!particlesToDelete.Contains(par))
            particles.Add(par);
    }

    public void DeleteParticle(Particle2D par)
    {
        particlesToDelete.Add(par);
        ClearOutDeadParticles();
    }

    void ClearOutDeadParticles()
    {
        while (particles.Contains(null))
        {
            particles.Remove(null);
        }

        while (particlesToDelete.Count != 0)
        {
            particles.Remove(particlesToDelete[0]);
            Destroy(particlesToDelete[0].gameObject);
            particlesToDelete.RemoveAt(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < particles.Count; i++)
        {
            if (particles[i] != null)
                for (int j = i + 1; j < particles.Count; j++)
                {
                    if (particles[j] != null)
                    //DeleteParticle(particles[j]);
                    //else
                    {
                        if (CollisonDetector.DetectCollision(particles[i], particles[j]))
                        {
                            HandlePlanetaryCollision(particles[i], particles[j]);
                        }
                    }
                }
            //else
            //DeleteParticle(particles[i]);
        }
        ClearOutDeadParticles();
    }

    public float coeffiientOfRestitution = .9f;
    void HandlePlanetaryCollision(Particle2D obj1, Particle2D obj2)
    {
        Vector2 normal = (obj1.mpPhysicsData.pos - obj2.mpPhysicsData.pos).normalized;
        float seperationVelocity = coeffiientOfRestitution * Vector2.Dot(obj1.mpPhysicsData.vel - obj2.mpPhysicsData.vel, normal);

        if (seperationVelocity > 0) return;

        float newSepVel = -seperationVelocity * coeffiientOfRestitution;
        float deltaVel = newSepVel - seperationVelocity;

        float totalInverseMass = obj1.mpPhysicsData.inverseMass + obj2.mpPhysicsData.inverseMass;

        float impulse = deltaVel / totalInverseMass;

        Vector2 impulsePerIMass = normal * impulse;

        obj1.mpPhysicsData.vel += impulsePerIMass * obj1.mpPhysicsData.inverseMass;
        obj2.mpPhysicsData.vel += impulsePerIMass * -obj2.mpPhysicsData.inverseMass;
    }

    public void GenerateRandomParticle()
    {
        float x = Random.Range(-Camera.main.orthographicSize, Camera.main.orthographicSize);
        float y = Random.Range(-Camera.main.orthographicSize * Camera.main.aspect,
                                Camera.main.orthographicSize * Camera.main.aspect);
        GameObject newPar = Instantiate(particlePrefab, new Vector3(x, y, 0), Quaternion.identity);
        float angle = Random.Range(0.0f, 360.0f);
        newPar.GetComponent<Particle2D>().mpPhysicsData.vel = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0.0f)
            * newPar.GetComponent<Particle2D>().mpPhysicsData.vel.magnitude;

        PlanetaryForceGenerator pfg = new PlanetaryForceGenerator(newPar.GetComponent<Particle2D>());
        ForceManager.AddForceGenerator(pfg);

        AddParticle(newPar.GetComponent<Particle2D>());

        //Vector2.Dot(new Vector2(), new Vector2());
    }
}
