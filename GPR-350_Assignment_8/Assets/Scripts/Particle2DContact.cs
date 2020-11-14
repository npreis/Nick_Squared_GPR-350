using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Particle2DContact : MonoBehaviour
{
    public GameObject mObj1;
    public GameObject mObj2;
    public float mRestitutionCoefficient = 0.0f;
    public Vector2 mContactNormal;
    public float mPenetration = 0.0f;
    public Vector2 mMove1;
    public Vector2 mMove2;

    public Particle2DContact(GameObject obj1, GameObject obj2, float restitutionCoefficient, Vector2 contactNormal, float penetration, Vector2 move1, Vector2 move2)
    {
        mObj1 = obj1;
        mObj2 = obj2;
        mRestitutionCoefficient = restitutionCoefficient;
        mContactNormal = contactNormal;
        mPenetration = penetration;
        mMove1 = move1;
        mMove2 = move2;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Resolve();
    }

    public void Resolve()
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

    public void ResolveVelocity()
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

        float totalInverseMass = (float)(mObj1.GetComponent<Particle2D>().mpPhysicsData.inverseMass);
        totalInverseMass += (float)(mObj2.GetComponent<Particle2D>().mpPhysicsData.inverseMass);

        if (totalInverseMass <= 0)//all infinite massed objects
            return;

        float impulse = deltaVel / totalInverseMass;
        Vector2 impulsePerIMass = mContactNormal * impulse;

        Vector2 newVelocity = mObj1.GetComponent<Particle2D>().mpPhysicsData.vel + impulsePerIMass * (float)(mObj1.GetComponent<Particle2D>().mpPhysicsData.inverseMass);
        mObj1.GetComponent<Particle2D>().mpPhysicsData.vel = newVelocity;
                
        Vector2 newVelocity2 = mObj2.GetComponent<Particle2D>().mpPhysicsData.vel + impulsePerIMass * (float)-(mObj2.GetComponent<Particle2D>().mpPhysicsData.inverseMass);
        mObj2.GetComponent<Particle2D>().mpPhysicsData.vel = newVelocity2;
    }

    public void ResolveInterpolation()
    {
        if (mPenetration <= 0.0f)
            return;

        float totalInverseMass = (float)(mObj1.GetComponent<Particle2D>().mpPhysicsData.inverseMass);
        totalInverseMass += (float)(mObj2.GetComponent<Particle2D>().mpPhysicsData.inverseMass);

        if (totalInverseMass <= 0)//all infinite massed objects
            return;

        Vector2 movePerIMass = mContactNormal * (mPenetration / totalInverseMass);

        mMove1 = movePerIMass * (float)(mObj1.GetComponent<Particle2D>().mpPhysicsData.inverseMass);
        mMove2 = movePerIMass * (float)-(mObj1.GetComponent<Particle2D>().mpPhysicsData.inverseMass);

        Vector2 newPosition = mObj1.GetComponent<Particle2D>().mpPhysicsData.pos + mMove1;
        mObj1.GetComponent<Particle2D>().mpPhysicsData.pos = newPosition;

        Vector2 newPosition2 = mObj2.GetComponent<Particle2D>().mpPhysicsData.pos + mMove2;
        mObj2.GetComponent<Particle2D>().mpPhysicsData.pos = newPosition2;
    }
}
