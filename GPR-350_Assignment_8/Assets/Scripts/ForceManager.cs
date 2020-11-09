using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceManager : MonoBehaviour
{
<<<<<<< HEAD
    static List<SpringForceGenerator2D> springForceGenerators;
    static List<PointForceGenerator2D> pointForceGenerators;
    static List<BouyancyForceGenerator2D> bouyancyForceGenerators;
    static List<RodForceGenerator2D> rodForceGenerators;
=======
    static List<SpringForceGenerator2D> springForceGenerators = new List<SpringForceGenerator2D>();
    static List<PointForceGenerator2D> pointForceGenerators = new List<PointForceGenerator2D>();
    static List<BouyancyForceGenerator2D> bouyancyForceGenerators = new List<BouyancyForceGenerator2D>();
>>>>>>> f5dc9e2abfba869bc80559a6bed551be0e241fca

    static public void AddForceGenerator(SpringForceGenerator2D fg)
    {
        springForceGenerators.Add(fg);
    }
    static public void AddForceGenerator(PointForceGenerator2D fg)
    {
        pointForceGenerators.Add(fg);
    }
    static public void AddForceGenerator(BouyancyForceGenerator2D fg)
    {
        bouyancyForceGenerators.Add(fg);
    }
    static public void AddForceGenerator(ref RodForceGenerator2D fg)
    {
        rodForceGenerators.Add(fg);
    }

    static public void DeleteForceGenerator(BouyancyForceGenerator2D fg)
    {
        bouyancyForceGenerators.Remove(fg);
    }
    static public void DeleteForceGenerator(SpringForceGenerator2D fg)
    {
        springForceGenerators.Remove(fg);
    }
    static public void DeleteForceGenerator(PointForceGenerator2D fg)
    {
        pointForceGenerators.Remove(fg);
    }
    static public void DeleteForceGenerator(ref RodForceGenerator2D fg)
    {
        rodForceGenerators.Remove(fg);
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
        foreach (RodForceGenerator2D fg in rodForceGenerators)
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
