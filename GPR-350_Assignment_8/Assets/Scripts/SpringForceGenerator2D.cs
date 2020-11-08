using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringForceGenerator2D : ForceGenerator2D
{
    public int startingID1;
    public int startingID2;
    public float startingSpringConstant;
    public float startingRestLength;

    private int id1;
    private int id2;
    private float springConstant;
    private float restLength;

    // Start is called before the first frame update
    void Start()
    {
        id1 = startingID1;
        id2 = startingID2;
        springConstant = startingSpringConstant;
        restLength = startingRestLength;
        shouldEffectAll = false;
    }

    public override void UpdateForce(ref PhysicsData2D pData, float dt)
    {
        //TO DO: get from static class?
        PhysicsData2D object1 = new PhysicsData2D(); 
        PhysicsData2D object2 = new PhysicsData2D();

        Vector2 pos1 = object1.pos;
        Vector2 pos2 = object2.pos;

        Vector2 diff = pos1 - pos2;
        float dist = diff.magnitude;

        float magnitude = restLength - dist;
        //if (magnitude < 0.0f)
        //magnitude = -magnitude;
        magnitude *= springConstant;

        diff.Normalize();
        diff *= magnitude;

        object1.accumulatedForces += diff;
        object2.accumulatedForces -= diff;

        //TODO send the data back out?
    }

}
