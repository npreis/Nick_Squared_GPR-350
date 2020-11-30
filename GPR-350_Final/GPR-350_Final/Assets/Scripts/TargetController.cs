using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public float radius;

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach(Particle2D p in GameObject.FindObjectsOfType<Particle2D>())
        {
            if(p != GetComponent<Particle2D>() && (p.mpPhysicsData.pos-GetComponent<Particle2D>().mpPhysicsData.pos).magnitude <= radius)
            {
                //GameObject.FindObjectOfType<GameManager>().score++;
                Destroy(p.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}
