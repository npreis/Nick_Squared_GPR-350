using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouyancyForceGenerator2D : ForceGenerator2D
{
    int startingID1;
    float startingObjectVolume;
    float startingMaxDepth;
    float startingLiquidPlaneY;
    float startingLiquidDensity;

    int id1;
    float objectVolume;
    float maxDepth;
    float liquidPlaneY;
    float liquidDensity;

    // Start is called before the first frame update
    void Start()
    {
        id1 = startingID1;
        objectVolume = startingObjectVolume;
        maxDepth = startingMaxDepth;
        liquidPlaneY = startingLiquidPlaneY;
        liquidDensity = startingLiquidDensity;
        shouldEffectAll = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void updateForce(ref PhysicsData2D pData, double dt)
    {
        //TODO LINK UP TO ID THROUGH STATIC CLASS
        PhysicsData2D obj = new PhysicsData2D();

        const float yPos = obj.y;
        float yForce = 0;

        if (yPos >= liquidPlaneY - maxDepth)
            return;
        else if (yObject <= mLiquidPlaneY - mMaxDepth)
            yForce = mObjectVolume * mLiquidDensity;
        else
            yForce = mLiquidDensity * mObjectVolume * (yObject - mMaxDepth - mLiquidPlaneY) / (2 * mMaxDepth);

        Vector2D force = Vector2D(0.0f, -abs(yForce));
        obj.accumulatedForce += force;

        //TODO SET ID TO NEW DATA
    }
}
