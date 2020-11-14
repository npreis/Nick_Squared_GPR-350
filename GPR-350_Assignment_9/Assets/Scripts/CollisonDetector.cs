using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisonDetector : MonoBehaviour
{
    //public GameObject mObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //DetectCollision();
    }

    public static bool DetectCollision(Particle2D obj1, Particle2D obj2)
    {
        bool isCollision = false;

        if(Vector2.Distance(obj1.mpPhysicsData.pos,obj2.mpPhysicsData.pos) <= obj1.radius+obj2.radius)
        {
            isCollision = true;
        }
        return isCollision;
    }
}
