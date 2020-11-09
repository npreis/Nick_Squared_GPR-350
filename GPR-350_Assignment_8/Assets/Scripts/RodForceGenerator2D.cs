using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodForceGenerator2D : ForceGenerator2D
{
    public Particle2D startingObject1;
    public Particle2D startingObject2;
    public float maxRodLength;
    public float mRestitution;

    private Particle2D object1;
    private Particle2D object2;
    private float rodLength;
    private float restitution;

    // Start is called before the first frame update
    void Start()
    {
        startingObject1 = object1;
        startingObject2 = object2;
        maxRodLength = rodLength;
        mRestitution = restitution;
    }

    // Update is called once per frame
    public override void UpdateForce(ref PhysicsDataPtr pData, float dt)
    {
        Vector2 pos1 = object1.mpPhysicsData.pos;
        Vector2 pos2 = object2.mpPhysicsData.pos;

        Vector2 diff = pos1 - pos2;
        float dist = diff.magnitude;

        if(dist == rodLength)
        {
            return;
        }

        diff.Normalize();

        object1.mpPhysicsData.accumulatedForces += diff;
        object2.mpPhysicsData.accumulatedForces += diff;
    }
}
