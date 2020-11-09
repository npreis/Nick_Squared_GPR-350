using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceManager : MonoBehaviour
{
    static List<SpringForceGenerator2D> springForceGenerators;
    static List<PointForceGenerator2D> pointForceGenerators;
    static List<BouyancyForceGenerator2D> bouyancyForceGenerators;

    static public void AddForceGenerator(ref SpringForceGenerator2D fg)
    {
        springForceGenerators.Add(fg);
    }
    static public void AddForceGenerator(ref PointForceGenerator2D fg)
    {
        pointForceGenerators.Add(fg);
    }
    static public void AddForceGenerator(ref BouyancyForceGenerator2D fg)
    {
        bouyancyForceGenerators.Add(fg);
    }

    static public void DeleteForceGenerator(ref BouyancyForceGenerator2D fg)
    {
        bouyancyForceGenerators.Remove(fg);
    }
    static public void DeleteForceGenerator(ref SpringForceGenerator2D fg)
    {
        springForceGenerators.Remove(fg);
    }
    static public void DeleteForceGenerator(ref PointForceGenerator2D fg)
    {
        pointForceGenerators.Remove(fg);
    }

    static public void ApplyAllForces(float dt)
    {
        foreach(SpringForceGenerator2D fg in springForceGenerators)
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
        foreach (PointForceGenerator2D fg in pointForceGenerators)
        {
            if (fg.GetShouldEffectAll())
            {
                foreach (Particle2D particle2D in GameObject.FindObjectsOfType<Particle2D>())
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
        foreach (BouyancyForceGenerator2D fg in bouyancyForceGenerators)
        {
            if (fg.GetShouldEffectAll())
            {
                foreach (Particle2D particle2D in GameObject.FindObjectsOfType<Particle2D>())
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
