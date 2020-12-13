using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeEllipticalOrbitVelocity : MonoBehaviour
{
    public Particle2D aroundBody;
    public float semiMajorAxis;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float speed = Mathf.Sqrt(((GetComponent<Particle2D>().GetMass() + aroundBody.GetMass())
                                   * PlanetaryForceGenerator.universalGavitationalConstant)
                                   * (2/Vector2.Distance(aroundBody.mpPhysicsData.pos, GetComponent<Particle2D>().mpPhysicsData.pos) - 1 / semiMajorAxis));
        GetComponent<Particle2D>().mpPhysicsData.vel = (aroundBody.mpPhysicsData.pos - GetComponent<Particle2D>().mpPhysicsData.pos).normalized * speed;
        GetComponent<Particle2D>().mpPhysicsData.vel = new Vector2(GetComponent<Particle2D>().mpPhysicsData.vel.y, -GetComponent<Particle2D>().mpPhysicsData.vel.x) * (Random.Range(-1, 1) == 0 ? 1 : -1);
        GetComponent<Particle2D>().mpPhysicsData.vel += aroundBody.mpPhysicsData.vel;
        if (aroundBody.GetComponent<InitializeCircularOrbitVelocity>() == null && aroundBody.GetComponent<InitializeEllipticalOrbitVelocity>() == null) Destroy(this);
    }
}