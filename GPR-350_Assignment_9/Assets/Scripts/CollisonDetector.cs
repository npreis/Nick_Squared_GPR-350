using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisonDetector : MonoBehaviour
{
    public GameObject mObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectCollision();
    }

    bool DetectCollision()
    {
        bool isCollision = false;

        if(mObj)
        {
            Destroy(mObj);
            isCollision = true;
        }
        else
        {
            isCollision = false;
        }
        return isCollision;
    }
}
