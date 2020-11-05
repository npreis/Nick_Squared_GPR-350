using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PhysicsData2D
{
    public Vector2 pos;
    public Vector2 accumulatedForces;
}

public abstract class ForceGenerator2D : MonoBehaviour
{
    abstract public void updateForce(ref PhysicsData2D pData, double dt);

    //couldn't figure out how to set this to return a const boolean
    public bool getShouldEffectAll() 
    {
        return shouldEffectAll;
    }

    protected bool shouldEffectAll = true;
}
