using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int numberSpawned;
    ParticleManager particleManager;

    // Start is called before the first frame update
    void Start()
    {
        particleManager = GetComponent<ParticleManager>();
        for(int i = 0; i < numberSpawned; i++)
        {
            particleManager.GenerateRandomParticle();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
    }
}
