using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //ForceManager forceManager;

    // Start is called before the first frame update
    void Start()
    {
        //forceManager = GetComponent<ForceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        ForceManager.ApplyAllForces(Time.fixedDeltaTime);
    }
}
