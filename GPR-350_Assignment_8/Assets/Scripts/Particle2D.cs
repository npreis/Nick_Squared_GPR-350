﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[System.Serializable]
public struct PhysicsDataPtr
{
    public Vector2 pos;
    public Vector2 vel;
    public Vector2 acc;
    public Vector2 accumulatedForces;
    public float inverseMass;
    public float dampingConstant;
    public bool shouldIgnoreForces;
}

public class Particle2D : MonoBehaviour
{
    public double mLifeSpan = 10.0;
    public double mLifeLeft = 10.0;
    public GameObject mSprite;
    public PhysicsDataPtr mpPhysicsData;
    //Color32 mStartColor;
    //Color32 mEndColor;
    double mStartAlpha = 1.0;
    double mEndAlpha = 1.0;
    double mScale = 1.0;
    //ParticleID mID = INVALID_PARTICLE_ID;
    bool iAmDead = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame

    void Update()
    {
        mLifeLeft -= Time.deltaTime;
        if (mLifeLeft <= 0.0)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        Integrator.Integrate(ref mpPhysicsData, Time.fixedDeltaTime);
    }

    public double GetPercentageOfLifeLeft()
    {
        if(mLifeSpan <= 0.0)
        {
            return 0.0;
        }
        return mLifeLeft / mLifeSpan;
    }

    public double GetPercentageOfLifeElapsed()
    {
        return 1.0 - GetPercentageOfLifeLeft();
    }
}
