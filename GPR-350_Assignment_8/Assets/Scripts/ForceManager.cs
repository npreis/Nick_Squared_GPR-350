using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceManager : MonoBehaviour
{
    static List<ForceGenerator2D> forceGenerators;

    static public void AddForceGenerator(ref ForceGenerator2D fg)
    {
        forceGenerators.Add(fg);
    }

    static public void DeleteForceGenerator(ref ForceGenerator2D fg)
    {
        forceGenerators.Remove(fg);
    }

    static public void ApplyAllForces(float dt)
    {
        foreach(ForceGenerator2D fg in forceGenerators)
        {
            if(fg.GetShouldEffectAll())
            {
                foreach(Particle2D particle2D in GameObject.FindObjectsOfType<Particle2D>())
                {
                    fg.UpdateForce(ref particle2D.mpPhysicsData, dt);
                }
            }
            else
            {
                PhysicsDataPtr p = new PhysicsDataPtr();
                fg.UpdateForce(ref p, dt);
            }
        }
    }
}
