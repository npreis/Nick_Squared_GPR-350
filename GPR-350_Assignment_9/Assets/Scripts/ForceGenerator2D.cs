using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PhysicsData2D
{
    public int id;
    public float inverseMass;
    public Vector2 pos;
    public Vector2 vel;
    public Vector2 acc;
    public Vector2 accumulatedForces;
    public float facing;
    public float rotVel;
    public float rotAcc;
    public float dampingConstant;
    public bool shouldIgnoreForces;
}

public abstract class ForceGenerator2D
{
    abstract public void UpdateForce(ref PhysicsDataPtr pData, float dt);

    //couldn't figure out how to set this to return a const boolean
    public bool GetShouldEffectAll() 
    {
        return shouldEffectAll;
    }

    protected bool shouldEffectAll = true;
}
