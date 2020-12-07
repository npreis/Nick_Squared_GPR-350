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
        int count = particles.Count;
        for (int i = 0; i < count; i++)
        {
            if (particles[i] != null)
                for (int j = i + 1; j < count; j++)
                {
                    if (particles[j] != null)
                    //DeleteParticle(particles[j]);
                    //else
                    {
                        if (CollisonDetector.DetectCollision(particles[i], particles[j]))
                        {
                            HandlePlanetaryCollision(particles[i], particles[j]);
                            AddStyleToCollision(particles[i], particles[j]);
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

    public int piecesPlanetsBreakInto;
    public float ratioToIgnoreBreakage = 10;
    void AddStyleToCollision(Particle2D obj1, Particle2D obj2)
    {
        bool obj1Smaller = obj1.mpPhysicsData.inverseMass > obj2.mpPhysicsData.inverseMass;
        Particle2D lesserObj = obj1Smaller ? obj1 : obj2;
        Particle2D greaterObj = obj1Smaller ? obj2 : obj1;

        float greaterMass = greaterObj.GetMass();
        float lesserMass = lesserObj.GetMass();

        if (lesserMass * ratioToIgnoreBreakage <= greaterMass)
        {
            greaterObj.AddMass(lesserMass);
            particlesToDelete.Add(lesserObj);
        }
        else if (1 - lesserMass / greaterMass < 1 / ratioToIgnoreBreakage)
        {
            greaterObj.AddMass(lesserMass);
            particlesToDelete.Add(lesserObj);
            greaterObj.mpPhysicsData.vel = (greaterObj.mpPhysicsData.vel + lesserObj.mpPhysicsData.vel).normalized * greaterObj.mpPhysicsData.vel.magnitude;

            Vector2 diff = greaterObj.mpPhysicsData.pos - lesserObj.mpPhysicsData.pos;
            Vector2 offset = -diff.normalized * greaterObj.radius / diff.magnitude;

            greaterObj.mpPhysicsData.pos += offset;
            return;
        }
        else
        {
            float proportion = (lesserMass / greaterMass);
            greaterObj.AddMass(lesserMass * (1 - proportion));
            lesserObj.AddMass(-lesserMass * (1 - proportion));

            if(piecesPlanetsBreakInto > 1)
            {
                GameObject[] newParts = new GameObject[piecesPlanetsBreakInto];
                lesserMass = lesserObj.GetMass();
                for (int i = 0; i < piecesPlanetsBreakInto; i++)
                {
                    newParts[i] = Instantiate(lesserObj.gameObject, lesserObj.transform.position, lesserObj.transform.rotation);
                }

                float angleSpread = 180.0f / (piecesPlanetsBreakInto + 1);
                Vector2 oldVec = lesserObj.mpPhysicsData.vel;
                Vector2 newVel = new Vector2(Mathf.Cos(180)*oldVec.x - Mathf.Sin(180) * oldVec.y,
                                                Mathf.Sin(180) * oldVec.x + Mathf.Cos(180) * oldVec.y);

                for(int i = 0; i < piecesPlanetsBreakInto; i++)
                {
                    Particle2D par = newParts[i].GetComponent<Particle2D>();
                    par.AddMass(-(lesserMass * (1-1.0f/piecesPlanetsBreakInto)));
                    newVel = new Vector2(Mathf.Cos(angleSpread) * newVel.x - Mathf.Sin(angleSpread) * newVel.y,
                                            Mathf.Sin(angleSpread) * newVel.x + Mathf.Cos(angleSpread) * newVel.y);
                    par.mpPhysicsData.vel = newVel; //* piecesPlanetsBreakInto;
                    par.mpPhysicsData.pos += newVel.normalized * (1.0f / (Mathf.Cos(90 - (angleSpread / 2.0f)) / par.radius));// par.radius/2);
                    newParts[i].transform.position = par.mpPhysicsData.pos;
                    newParts[i].GetComponent<Particle2D>().mpPhysicsData = par.mpPhysicsData;
                    AddParticle(newParts[i].GetComponent<Particle2D>());
                    ForceManager.AddForceGenerator(new PlanetaryForceGenerator(newParts[i].GetComponent<Particle2D>()));
                }
                particlesToDelete.Add(lesserObj);
            }
        }
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
