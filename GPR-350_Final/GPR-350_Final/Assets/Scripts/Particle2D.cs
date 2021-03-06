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
    public float radius;
    //public double mLifeSpan = 0.0;
    //double mLifeLeft = 0.0;
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
        //mLifeLeft = mLifeSpan;
        mpPhysicsData.pos = transform.position;
    }

    // Update is called once per frame

    void Update()
    {
        //mLifeLeft -= Time.deltaTime;
        //if (mLifeLeft <= 0.0)
        //{
        //    Destroy(gameObject);
        //}
        transform.position = mpPhysicsData.pos;
    }

    void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;
        Integrator.Integrate(ref mpPhysicsData, dt);
    }

    //public double GetPercentageOfLifeLeft()
    //{
    //    if(mLifeSpan <= 0.0)
    //    {
    //        return 0.0;
    //    }
    //    return mLifeLeft / mLifeSpan;
    //}

    //public double GetPercentageOfLifeElapsed()
    //{
    //    return 1.0 - GetPercentageOfLifeLeft();
    //}

    private void OnDestroy()
    {
        if(GameObject.FindObjectOfType<ParticleManager>() != null)
        GameObject.FindObjectOfType<ParticleManager>().DeleteParticle(this);
    }

    public float GetMass()
    {
        return 1 / mpPhysicsData.inverseMass;
    }

    public void AddMass(float massToAdd)
    {
        float mass = GetMass();
        float volume = 4.0f / 3.0f * Mathf.PI * Mathf.Pow(radius, 3);
        float sizePerInverseMass = volume / mass ;
        mpPhysicsData.inverseMass = 1 / (mass + massToAdd);
        volume = sizePerInverseMass * (mass + massToAdd);
        radius = Mathf.Pow((volume * 3) / (Mathf.PI * 4), 1.0f / 3.0f);
        transform.localScale = new Vector3(1, 1, 1) * radius * 2.0f;
    }
}
