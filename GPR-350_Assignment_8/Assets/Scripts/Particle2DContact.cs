﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Particle2DContact : MonoBehaviour
{
    PhysicsDataPtr mObj1;
    PhysicsDataPtr mObj2;
    float mRestitutionCoefficient = 0.0f;
    public Vector2 mContactNormal;
    float mPenetration = 0.0f;
    public Vector2 mMove1;
    public Vector2 mMove2;

    // Start is called before the first frame update
    void Start(PhysicsDataPtr obj1, PhysicsDataPtr obj2, float restitutionCoefficient, Vector2 contactNormal, float penetration, Vector2 move1, Vector2 move2)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Resolve()
    {
        ResolveVelocity();
        ResolveInterpolation();
    }

    public float CalculateSeparatingVelocity()
    {
        Vector2 relativeVel = mObj1.GetComponent<Particle2D>().mpPhysicsData.vel;
        relativeVel -= mObj2.GetComponent<Particle2D>().mpPhysicsData.vel;

        return Vector2.Dot(relativeVel, mContactNormal);
    }

    void ResolveVelocity()
    {
        float separatingVel = CalculateSeparatingVelocity();
        if (separatingVel > 0.0f)//already separating so need to resolve
            return;

        float newSepVel = -separatingVel * mRestitutionCoefficient;

        Vector2 velFromAcc = mObj1.GetComponent<Particle2D>().mpPhysicsData.acc;
        velFromAcc -= mObj2.GetComponent<Particle2D>().mpPhysicsData.acc;

        float accCausedSepVelocity = Vector2.Dot(velFromAcc, mContactNormal) * Time.deltaTime;

        if (accCausedSepVelocity < 0.0f)
        {
            newSepVel += mRestitutionCoefficient * accCausedSepVelocity;
            if (newSepVel < 0.0f)
                newSepVel = 0.0f;
        }

        float deltaVel = newSepVel - separatingVel;

        float totalInverseMass = (float)(1.0 / mObj1.GetComponent<Particle2D>().mpPhysicsData.mass);
        totalInverseMass += (float)(1.0 / mObj2.GetComponent<Particle2D>().mpPhysicsData.mass);

        if (totalInverseMass <= 0)//all infinite massed objects
            return;

        float impulse = deltaVel / totalInverseMass;
        Vector2 impulsePerIMass = mContactNormal * impulse;

        Vector2 newVelocity = mObj1.GetComponent<Particle2D>().mpPhysicsData.vel + impulsePerIMass * (float)(1.0 / mObj1.GetComponent<Particle2D>().mpPhysicsData.mass);
        mObj1.GetComponent<Particle2D>().mpPhysicsData.vel = newVelocity;
                
        Vector2 newVelocity2 = mObj2.GetComponent<PhysicsDataPtr>.vel + impulsePerIMass * (float)-(1.0 / mObj2.GetComponent<Particle2D>().mpPhysicsData.mass);
        mObj2.GetComponent<Particle2D>().mpPhysicsData.vel = newVelocity2;
    }

    void ResolveInterpolation()
    {
        if (mPenetration <= 0.0f)
            return;

        float totalInverseMass = (float)(1.0 / mObj1.GetComponent<Particle2D>().mpPhysicsData.mass);
        totalInverseMass += (float)(1.0 / mObj2.GetComponent<Particle2D>().mpPhysicsData.mass);

        if (totalInverseMass <= 0)//all infinite massed objects
            return;

        Vector2 movePerIMass = mContactNormal * (mPenetration / totalInverseMass);

        mMove1 = movePerIMass * (float)(1.0 / mObj1.GetComponent<Particle2D>().mpPhysicsData.mass);
        mMove2 = movePerIMass * (float)-(1.0 / mObj1.GetComponent<Particle2D>().mpPhysicsData.mass);

        Vector2 newPosition = (Vector2)mObj1.transform.position + mMove1;
        mObj1.transform.position = newPosition;
        if (mObj2)
        {
            Vector2 newPosition2 = (Vector2)mObj2.transform.position + mMove2;
            mObj2.transform.position = newPosition2;
        }
    }
}
