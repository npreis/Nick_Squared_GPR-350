using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringForceGenerator2D : ForceGenerator2D
{
    public SpringForceGenerator2D(Particle2D o1, Particle2D o2, float springConst, float length)
    {
        object1 = o1;
        object2 = o2;
        springConstant = springConst;
        restLength = length;
        shouldEffectAll = false;
    }

    Particle2D object1;
    Particle2D object2;
    float springConstant;
    float restLength;

    // Start is called before the first frame update
    void Start()
    {

    }

    public override void UpdateForce(ref PhysicsDataPtr pData, float dt)
    {
        Vector2 pos1 = object1.mpPhysicsData.pos;
        Vector2 pos2 = object2.mpPhysicsData.pos;

        Vector2 diff = pos1 - pos2;
        float dist = diff.magnitude;

        float magnitude = restLength - dist;
        //if (magnitude < 0.0f)
        //magnitude = -magnitude;
        magnitude *= springConstant;

        diff.Normalize();
        diff *= magnitude;

        object1.mpPhysicsData.accumulatedForces += diff;
        object2.mpPhysicsData.accumulatedForces -= diff;
    }

}
