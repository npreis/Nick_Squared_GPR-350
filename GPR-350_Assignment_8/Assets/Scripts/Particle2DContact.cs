using System.Collections;
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
    void Start()
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
        Vector2 relativeVel = PhysicsDataPtr::getVelocity(mObj1);
        if(mObj2)
        {
            relativeVel -= PhysicsDataPtr::getVelocity(mObj2);
        }
        return Vector2.Dot(relativeVel, mContactNormal);
    }

    void ResolveVelocity()
    {
        float separatingVel = CalculateSeparatingVelocity();
        if (separatingVel > 0.0f)//already separating so need to resolve
            return;

        float newSepVel = -separatingVel * mRestitutionCoefficient;

        Vector2 velFromAcc = PhysicsDataPtr::getAcceleration(mObj1);
        if (mObj2)
            velFromAcc -= PhysicsDataPtr::getAcceleration(mObj2);
        float accCausedSepVelocity = Vector2.Dot(velFromAcc, mContactNormal) * Time.deltaTime;

        if (accCausedSepVelocity < 0.0f)
        {
            newSepVel += mRestitutionCoefficient * accCausedSepVelocity;
            if (newSepVel < 0.0f)
                newSepVel = 0.0f;
        }

        float deltaVel = newSepVel - separatingVel;

        float totalInverseMass = (float)PhysicsDataPtr::getInverseMass(mObj1);
        if (mObj2)
            totalInverseMass += (float)PhysicsDataPtr::getInverseMass(mObj2);

        if (totalInverseMass <= 0)//all infinite massed objects
            return;

        float impulse = deltaVel / totalInverseMass;
        Vector2 impulsePerIMass = mContactNormal * impulse;

        Vector2 newVelocity = PhysicsDataPtr::getVelocity(mObj1) + impulsePerIMass * (float)PhysicsDataPtr::getInverseMass(mObj1);
        PhysicsDataPtr::getVelocity(mObj1) = newVelocity;
        if (mObj2)
        {
            Vector2 newVelocity2 = PhysicsDataPtr::getVelocity(mObj2) + impulsePerIMass * (float)-PhysicsDataPtr::getInverseMass(mObj2);
            PhysicsDataPtr::getVelocity(mObj2) = newVelocity2;
        }
    }

    void ResolveInterpolation()
    {
        if (mPenetration <= 0.0f)
            return;

        float totalInverseMass = (float)PhysicsDataPtr::getInverseMass(mObj1);
        if (mObj2)
            totalInverseMass += (float)PhysicsDataPtr::getInverseMass(mObj1);

        if (totalInverseMass <= 0)//all infinite massed objects
            return;

        Vector2 movePerIMass = mContactNormal * (mPenetration / totalInverseMass);

        mMove1 = movePerIMass * (float)(PhysicsDataPtr::getInverseMass(mObj1));
        if (mObj2)
            mMove2 = movePerIMass * (float)-(PhysicsDataPtr::getInverseMass(mObj1));
        else
            mMove2 = ZERO_VECTOR2D;

        Vector2 newPosition = (Vector2)mObj1.transform.position + mMove1;
        mObj1.transform.position = newPosition;
        if (mObj2)
        {
            Vector2 newPosition2 = (Vector2)mObj2.transform.position + mMove2;
            mObj2.transform.position = newPosition2;
        }
    }
}
