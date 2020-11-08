using System.Collections;
using System.Collections.Generic;
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

}

public class Particle2DRod : Particle2DLink
{

}