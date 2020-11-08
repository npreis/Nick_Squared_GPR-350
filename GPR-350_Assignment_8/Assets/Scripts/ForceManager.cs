using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceManager : MonoBehaviour
{
    static List<ForceGenerator2D> forceGenerators;

    static void AddForceGenerator(ref ForceGenerator2D fg)
    {
        forceGenerators.Add(fg);
    }

    static void DeleteForceGenerator(ref ForceGenerator2D fg)
    {
        forceGenerators.Remove(fg);
    }

    static void ApplyAllForces(float dt)
    {
        foreach(ForceGenerator2D fg in forceGenerators)
        {
            if(fg.GetShouldEffectAll())
            {
                foreach(Particle2D particle2D in FindObjectsOfType<Particle2D>())
                {
                    //fg.UpdateForce(particle2D, dt);
                }
            }
            else
            {
                //fg.UpdateForce(new PhysicsData2D(), dt);
            }
        }
    }
}
