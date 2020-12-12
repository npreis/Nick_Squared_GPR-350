using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetaryForceGenerator : ForceGenerator2D
{

    public PlanetaryForceGenerator(Particle2D object1)
    {
        universalGavitationalConstant = 6.6740f * Mathf.Pow(10.0f, powerOfConstant);
        mPlanet = object1;
    }

    public Particle2D mPlanet;

    static public int powerOfConstant = -11;
    static public float universalGavitationalConstant;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame

    public override void UpdateForce(ref PhysicsDataPtr pData, float dt)
    {
        if (mPlanet == null)
        {
            ForceManager.DeleteForceGenerator(this);
            return;
        }

        Vector2 radiusVec = mPlanet.mpPhysicsData.pos - pData.pos;

        if(radiusVec.magnitude == 0)
        {
            return;
        }

        //radiusVec *= .5f;
        float gravitationalForce = (universalGavitationalConstant * (1 / mPlanet.mpPhysicsData.inverseMass) * (1 / pData.inverseMass)) / (radiusVec.magnitude * radiusVec.magnitude);
        radiusVec.Normalize();
        pData.accumulatedForces += gravitationalForce * radiusVec;
        //mPlanet.mpPhysicsData.accumulatedForces -= gravitationalForce * radiusVec;
    }
}
