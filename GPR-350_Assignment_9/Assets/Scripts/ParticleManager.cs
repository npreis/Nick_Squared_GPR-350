using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    List<Particle2D> particles;
    List<Particle2D> particlesToDelete;
    public GameObject particlePrefab;

    void AddParticle(Particle2D par)
    {
        particles.Add(par);
    }

    void DeleteParticle(Particle2D par)
    {
        particlesToDelete.Add(par);
    }

    void ClearOutDeadParticles()
    {
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
        for(int i = 0; i < particles.Count; i++)
        {
            for(int j = i+1; j < particles.Count; j++)
            {
                if (false)//CollisionDetector.DetectCollision(particles[i],particles[j])
                {
                    DeleteParticle(particles[i]);
                    DeleteParticle(particles[j]);

                }
            }
        }
        ClearOutDeadParticles();
    }

    void GenerateRandomParticle()
    {
        float x = Random.Range(-Camera.main.orthographicSize, Camera.main.orthographicSize);
        float y = Random.Range(-Camera.main.orthographicSize * Camera.main.aspect,
                                Camera.main.orthographicSize * Camera.main.aspect);
        GameObject newPar = Instantiate(particlePrefab, new Vector3(x, y, 0), Quaternion.identity);
        float angle = Random.Range(0.0f, 360.0f);
        newPar.GetComponent<Particle2D>().mpPhysicsData.vel = new Vector3(Mathf.Cos(angle),Mathf.Sin(angle),0.0f) 
            * newPar.GetComponent<Particle2D>().mpPhysicsData.vel.magnitude;

        BouyancyForceGenerator2D bfg = new BouyancyForceGenerator2D(newPar.GetComponent<Particle2D>(), .5f, .25f, 0, 1.5f);
        ForceManager.AddForceGenerator(bfg);

        AddParticle(newPar.GetComponent<Particle2D>());
    }
}
