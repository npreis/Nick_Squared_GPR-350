using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Particle2DLink : MonoBehaviour
{
    PhysicsDataPtr mObj1;
    PhysicsDataPtr mObj2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float GetCurrentLength()
    {
        float distance = Mathf.Abs(mObj1.GetComponent<PhysicsDataPtr>().pos - mObj2.GetComponent<PhysicsDataPtr>().pos);
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

    virtual void createContacts(Particle2DContact contacts)
    {
        float length = getCurrentLength();
        if (length < mMaxLength)
            return;

        Vector2 normal = mObj1.GetComponent<PhysicsDataPtr>().pos - mObj2.GetComponent<PhysicsDataPtr>().pos;
        normal.normalize();
        float penetration = length - mMaxLength;

        Particle2DContact contact(PhysicsDataPtr obj1 = mObj1, PhysicsDataPtr obj2 = mObj2, float restitutionCoefficient = mRestitution, 
            Vector2 contactNormal = normal, float penetration = penetration, Vector2 move1 = ZERO_VECTOR2D, Vector2 move2 = ZERO_VECTOR2D);

        contacts.push_back(contact);
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

    virtual void createContacts(Particle2DContact contacts)
    {
        float length = getCurrentLength();
        if (length == mRodLength)
        {
            return;
        }

        Vector2D normal;
        float penetration = length - mRodLength;
        penetration /= mDamping;

        if (penetration < 0)
        {
            penetration = mRodLength - length;
            normal = mObj1.GetComponent<PhysicsDataPtr>().pos - mObj2.GetComponent<PhysicsDataPtr>().pos;
        }
        else
        {
            normal = mObj2.GetComponent<PhysicsDataPtr>().pos - mObj1.GetComponent<PhysicsDataPtr>().pos;
        }

        normal.normalize();
        Particle2DContact contact(PhysicsDataPtr obj1 = mObj1, PhysicsDataPtr obj2 = mObj2, float restitutionCoefficient = mRestitution,
            Vector2 contactNormal = normal, float penetration = penetration, Vector2 move1 = ZERO_VECTOR2D, Vector2 move2 = ZERO_VECTOR2D);
        contacts.push_back(contact);
    }
}