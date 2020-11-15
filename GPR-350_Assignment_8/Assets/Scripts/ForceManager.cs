using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ForceManager : MonoBehaviour
{
    static List<SpringForceGenerator2D> springForceGenerators = new List<SpringForceGenerator2D>();
    static List<SpringForceGenerator2D> springToDelete = new List<SpringForceGenerator2D>();
    static List<PointForceGenerator2D> pointForceGenerators = new List<PointForceGenerator2D>();
    static List<PointForceGenerator2D> pointToDelete = new List<PointForceGenerator2D>();
    static List<BouyancyForceGenerator2D> bouyancyForceGenerators = new List<BouyancyForceGenerator2D>();
    static List<BouyancyForceGenerator2D> bouyancyToDelete = new List<BouyancyForceGenerator2D>();
    static List<RodForceGenerator2D> rodForceGenerators = new List<RodForceGenerator2D>();
    static List<RodForceGenerator2D> rodToDelete = new List<RodForceGenerator2D>();


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
        bouyancyToDelete.Add(fg);
    }
    static public void DeleteForceGenerator(SpringForceGenerator2D fg)
    {
        springToDelete.Add(fg);
    }
    static public void DeleteForceGenerator(PointForceGenerator2D fg)
    {
        pointToDelete.Add(fg);
    }
    static public void DeleteForceGenerator(ref RodForceGenerator2D fg)
    {
        rodToDelete.Add(fg);
    }

    static public void ApplyAllForces(float dt)
    {
        while(springToDelete.Count != 0)
        {
            springForceGenerators.Remove(springToDelete[0]);
            springToDelete.RemoveAt(0);
        }

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

        while (pointToDelete.Count != 0)
        {
            pointForceGenerators.Remove(pointToDelete[0]);
            pointToDelete.RemoveAt(0);
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

        while (bouyancyToDelete.Count != 0)
        {
            bouyancyForceGenerators.Remove(bouyancyToDelete[0]);
            bouyancyToDelete.RemoveAt(0);
        }
        foreach (BouyancyForceGenerator2D fg in bouyancyForceGenerators)
        {
             PhysicsDataPtr p = new PhysicsDataPtr();
             fg.UpdateForce(ref p, dt);
        }

        while (rodToDelete.Count != 0)
        {
            rodForceGenerators.Remove(rodToDelete[0]);
            rodToDelete.RemoveAt(0);
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
