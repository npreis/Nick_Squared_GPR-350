using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodForceGenerator2D : ForceGenerator2D
{
    public RodForceGenerator2D(Particle2D object1, Particle2D object2, float rodLength, float restitution)
    {
        startingObject1 = object1;
        startingObject2 = object2;
        maxRodLength = rodLength;
        mRestitution = restitution;
    }

    public Particle2D startingObject1;
    public Particle2D startingObject2;
    public float maxRodLength;
    public float mRestitution;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public override void UpdateForce(ref PhysicsDataPtr pData, float dt)
    {
        Vector2 pos1 = startingObject1.mpPhysicsData.pos;
        Vector2 pos2 = startingObject2.mpPhysicsData.pos;

        Vector2 diff = pos1 - pos2;
        float dist = diff.magnitude;

        if(dist != maxRodLength)
        {
            dist = maxRodLength;
        }

        diff.Normalize();

        startingObject1.mpPhysicsData.accumulatedForces += diff;
        startingObject2.mpPhysicsData.accumulatedForces += diff;

        startingObject1.mpPhysicsData.accumulatedForces -= diff;
        startingObject2.mpPhysicsData.accumulatedForces -= diff;
    }
}
