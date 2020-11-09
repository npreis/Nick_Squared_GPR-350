﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringForceGenerator2D : ForceGenerator2D
{
    public Particle2D startingObject1;
    public Particle2D startingObject2;
    public float startingSpringConstant;
    public float startingRestLength;

    private Particle2D object1;
    private Particle2D object2;
    private float springConstant;
    private float restLength;

    // Start is called before the first frame update
    void Start()
    {
        object1 = startingObject1;
        object2 = startingObject2;
        springConstant = startingSpringConstant;
        restLength = startingRestLength;
        shouldEffectAll = false;
    }

    public override void UpdateForce(ref PhysicsDataPtr pData, float dt)
    {

        Vector2 pos1 = object1.mpPhysicsData.pos;
        Vector2 pos2 = object2.mpPhysicsData.pos;

        Vector2 diff = pos1 - pos2;
        float dist = diff.magnitude;

        float magnitude = restLength - dist;
        //if (magnitude < 0.0f)
        //magnitude = -magnitude;
        magnitude *= springConstant;

        diff.Normalize();
        diff *= magnitude;

        object1.mpPhysicsData.accumulatedForces += diff;
        object2.mpPhysicsData.accumulatedForces -= diff;
    }

}
