using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int powerOfConstant = -11;
    ParticleManager particleManager;


    // Start is called before the first frame update
    void Start()
    {
        PlanetaryForceGenerator.powerOfConstant = powerOfConstant;

        particleManager = GetComponent<ParticleManager>();
        foreach(Particle2D par in GameObject.FindObjectsOfType<Particle2D>())
        {
            particleManager.AddParticle(par);
            ForceManager.AddForceGenerator(new PlanetaryForceGenerator(par));
        }

    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        ForceManager.ApplyAllForces(Time.fixedDeltaTime);
    }
}
