﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using UnityEngine;

public class Particle2DLink : MonoBehaviour
{
    protected GameObject mObj1;
    protected GameObject mObj2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected float GetCurrentLength()
    {
        float distance = Mathf.Abs(mObj1.GetComponent<Particle2D>().mpPhysicsData.pos - mObj2.GetComponent<Particle2D>().mpPhysicsData.pos);
        return distance;
    }
}

public class Particle2DCable : Particle2DLink
{
    float mMaxLength;
    float mRestitution;

    void Start()
    {

    }

    protected virtual void createContacts(List<Particle2DContact> contacts)
    {
        float length = GetCurrentLength();
        if (length < mMaxLength)
            return;

        Vector2 normal = mObj1.GetComponent<Particle2D>().mpPhysicsData.pos - mObj2.GetComponent<Particle2D>().mpPhysicsData.pos;
        normal = (Vector2)(1.0 / normal);
        float mPenetration = length - mMaxLength;

        Particle2DContact contact(PhysicsDataPtr obj1 = mObj1, PhysicsDataPtr obj2 = mObj2, float restitutionCoefficient = mRestitution, 
            Vector2 contactNormal = normal, float penetration = mPenetration, Vector2 move1 = (0, 0), Vector2 move2 = (0, 0));

        contacts.Add(contact);
    }
}

public class Particle2DRod : Particle2DLink
{
    float mRodLength;
    float mRestitution;
    float mDamping;

    void Start()
    {

    }

    protected virtual void createContacts(List<Particle2DContact> contacts)
    {
        float length = GetCurrentLength();
        if (length == mRodLength)
        {
            return;
        }

        Vector2 normal;
        float penetration = length - mRodLength;
        penetration /= mDamping;

        if (penetration < 0)
        {
            penetration = mRodLength - length;
            normal = mObj1.GetComponent<Particle2D>().mpPhysicsData.pos - mObj2.GetComponent<Particle2D>().mpPhysicsData.pos;
        }
        else
        {
            normal = mObj2.GetComponent<Particle2D>().mpPhysicsData.pos - mObj1.GetComponent<Particle2D>().mpPhysicsData.pos;
        }

        normal = (Vector2)(1.0 / normal);
        Particle2DContact contact(GameObject obj1 = mObj1, GameObject obj2 = mObj2, float restitutionCoefficient = mRestitution,
            Vector2 contactNormal = normal, float penetration = penetration, Vector2 move1 = Vector2.zero, Vector2 move2 = Vector2.zero);
        contacts.Add(contact);
    }
}