using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Particle2DContact : MonoBehaviour
{
    public GameObject mObj1;
    public GameObject mObj2;
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
        Vector2 relativeVel = mObj1.GetComponent<Particle2D>().Velocity;
        if(mObj2)
        {
            relativeVel -= mObj2.GetComponent<Particle2D>().Velocity;
        }
        return Vector2.Dot(relativeVel, mContactNormal);
    }

    void ResolveVelocity()
    {
        float separatingVel = CalculateSeparatingVelocity();
        if (separatingVel > 0.0f)//already separating so need to resolve
            return;

        float newSepVel = -separatingVel * mRestitutionCoefficient;

        Vector2 velFromAcc = mObj1.GetComponent<Particle2D>().Acceleraton;
        if (mObj2)
            velFromAcc -= mObj1.GetComponent<Particle2D>().Acceleraton;
        float accCausedSepVelocity = Vector2.Dot(velFromAcc, mContactNormal) * Time.deltaTime;

        if (accCausedSepVelocity < 0.0f)
        {
            newSepVel += mRestitutionCoefficient * accCausedSepVelocity;
            if (newSepVel < 0.0f)
                newSepVel = 0.0f;
        }

        float deltaVel = newSepVel - separatingVel;

        float totalInverseMass = (float)(1.0 / mObj1.GetComponent<Particle2D>().Mass);
        if (mObj2)
            totalInverseMass += (float)(1.0 / mObj1.GetComponent<Particle2D>().Mass);

        if (totalInverseMass <= 0)//all infinite massed objects
            return;

        float impulse = deltaVel / totalInverseMass;
        Vector2 impulsePerIMass = mContactNormal * impulse;

        Vector2 newVelocity = mObj1.GetComponent<Particle2D>().Velocity + impulsePerIMass * (float)(1.0 / mObj1.GetComponent<Particle2D>().Mass);
        mObj1.GetComponent<Particle2D>().Velocity = newVelocity;
        if (mObj2)
        {
            Vector2 newVelocity2 = mObj2.GetComponent<Particle2D>().Velocity + impulsePerIMass * (float)-(1.0 / mObj2.GetComponent<Particle2D>().Mass);
            mObj2.GetComponent<Particle2D>().Velocity = newVelocity2;
        }
    }

    void ResolveInterpolation()
    {
        if (mPenetration <= 0.0f)
            return;

        float totalInverseMass = (float)(1.0 / mObj1.GetComponent<Particle2D>().Mass);
        if (mObj2)
            totalInverseMass += (float)(1.0 / mObj2.GetComponent<Particle2D>().Mass);

        if (totalInverseMass <= 0)//all infinite massed objects
            return;

        Vector2 movePerIMass = mContactNormal * (mPenetration / totalInverseMass);

        mMove1 = movePerIMass * (float)(1.0 / mObj1.GetComponent<Particle2D>().Mass);
        if (mObj2)
            mMove2 = movePerIMass * (float)-(1.0 / mObj2.GetComponent<Particle2D>().Mass);
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
