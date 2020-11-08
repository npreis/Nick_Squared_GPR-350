using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Integrator : MonoBehaviour
{
    //public static int nextPhysicsID = 0;

    static void Integrate(ref PhysicsData2D physData, float dt)
    {
        physData.pos += physData.vel * dt;

        Vector2 resultingAcc = new Vector2(physData.acc.x, physData.acc.y);

        if(!physData.shouldIgnoreForces)
        {
            resultingAcc += physData.accumulatedForces * physData.inverseMass;
        }

        physData.vel += (resultingAcc * dt);
        float damping = Mathf.Pow(physData.dampingConstant, dt);
        physData.vel *= damping;

        physData.accumulatedForces = new Vector2(0, 0);
    }
}
