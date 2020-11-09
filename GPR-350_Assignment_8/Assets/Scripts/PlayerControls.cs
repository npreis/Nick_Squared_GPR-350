using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public List<GameObject> particlePrefabs;

    int weaponChoice = 0;
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            ChangeWeapon();
        }
        else if(Input.GetKeyDown(KeyCode.Return))
        {
            Fire();
        }
        else if (Input.GetKey(KeyCode.Alpha1))
        {
            Rotate(true);
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            Rotate(false);
        }

    }

    void ChangeWeapon()
    {
        weaponChoice++;
        weaponChoice %= particlePrefabs.Count + 2;
    }

    void Fire()
    {
        if(weaponChoice < particlePrefabs.Count)
        {
            GameObject projectile = Instantiate(particlePrefabs[weaponChoice],transform.position,transform.rotation);
            projectile.GetComponent<Particle2D>().mpPhysicsData.vel = projectile.transform.up * projectile.GetComponent<Particle2D>().mpPhysicsData.vel.normalized;
        }
        else if(weaponChoice == particlePrefabs.Count)
        {
            GameObject projectile1 = Instantiate(particlePrefabs[0], transform.position, transform.rotation);
            projectile1.GetComponent<Particle2D>().mpPhysicsData.vel = projectile1.transform.up * projectile1.GetComponent<Particle2D>().mpPhysicsData.vel.normalized;
            GameObject projectile2 = Instantiate(particlePrefabs[0], transform.position, transform.rotation);
            projectile2.GetComponent<Particle2D>().mpPhysicsData.vel = projectile2.transform.up * projectile2.GetComponent<Particle2D>().mpPhysicsData.vel.normalized;
            projectile2.GetComponent<Particle2D>().mpPhysicsData.pos += projectile2.transform.up * projectile2.GetComponent<Particle2D>().mpPhysicsData.vel.normalized;

            SpringForceGenerator2D fg = new SpringForceGenerator2D();
            fg.startingObject1 = projectile1.GetComponent<Particle2D>();
            fg.startingObject2 = projectile2.GetComponent<Particle2D>();
            ForceManager.AddForceGenerator(ref fg);
        }
        else
        {
            GameObject projectile1 = Instantiate(particlePrefabs[0], transform.position, transform.rotation);
            projectile1.GetComponent<Particle2D>().mpPhysicsData.vel = projectile1.transform.up * projectile1.GetComponent<Particle2D>().mpPhysicsData.vel.normalized;
            GameObject projectile2 = Instantiate(particlePrefabs[0], transform.position, transform.rotation);
            projectile2.GetComponent<Particle2D>().mpPhysicsData.vel = projectile2.transform.up * projectile2.GetComponent<Particle2D>().mpPhysicsData.vel.normalized;
            projectile2.GetComponent<Particle2D>().mpPhysicsData.pos += projectile2.transform.up * projectile2.GetComponent<Particle2D>().mpPhysicsData.vel.normalized;

            RodForceGenerator2D fg = new RodForceGenerator2D();
            fg.startingObject1 = projectile1.GetComponent<Particle2D>();
            fg.startingObject2 = projectile2.GetComponent<Particle2D>();
            ForceManager.AddForceGenerator(ref fg);
        }
    }

    void Rotate(bool left)
    {
        float rotSpeed = rotationSpeed;
        if (!left) rotSpeed *= -1;
        transform.Rotate(new Vector3(0,0,rotSpeed * Time.deltaTime));
    }
}
