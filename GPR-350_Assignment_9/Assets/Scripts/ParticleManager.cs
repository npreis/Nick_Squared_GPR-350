using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    
    public List<Particle2D> particles = new List<Particle2D>();
    List<Particle2D> particlesToDelete = new List<Particle2D>();
    public GameObject particlePrefab;

    void AddParticle(Particle2D par)
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
        while(particles.Contains(null))
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
        for(int i = 0; i < particles.Count; i++)
        {
            if (particles[i] != null)
                for (int j = i + 1; j < particles.Count; j++)
                {
                    if (particles[j] != null)
                        //DeleteParticle(particles[j]);
                    //else
                    {
                        if (false)//CollisionDetector.DetectCollision(particles[i],particles[j])
                        {
                            particlesToDelete.Add(particles[i]);
                            particlesToDelete.Add(particles[j]);
                        }
                    }
                }
            //else
                //DeleteParticle(particles[i]);
        }
        ClearOutDeadParticles();
    }

    public void GenerateRandomParticle()
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

        Vector2.Dot(new Vector2(), new Vector2());
    }
}
