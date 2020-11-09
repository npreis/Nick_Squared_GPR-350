using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public struct PhysicsDataPtr
{
    public Vector2 pos;
    public Vector2 vel;
    public Vector2 acc;
    public float mass;
}

public class Particle2D : MonoBehaviour
{
    double mLifeSpan = 0.0;
    double mLifeLeft = 0.0;
    public GameObject mSprite;
    PhysicsDataPtr mpPhysicsData = nullptr;
    Color32 mStartColor;
    Color32 mEndColor;
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
    bool Update()
    {
        mLifeLeft -= Time.deltaTime;
        if (mLifeLeft <= 0.0)
        {
            return true;
        }
        return false;
    }

    void Draw()
    {

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
