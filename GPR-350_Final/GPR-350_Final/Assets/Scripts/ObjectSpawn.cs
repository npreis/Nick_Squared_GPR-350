using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{
    public GameObject asteroid;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);
            Vector3 changeZ = new Vector3(origin.x, origin.y, asteroid.transform.position.z);

<<<<<<< HEAD
            Instantiate(asteroid).transform.position = changeZ;
            GetComponent<ParticleManager>().AddParticle(asteroid.GetComponent<Particle2D>());
            ForceManager.AddForceGenerator(new PlanetaryForceGenerator(asteroid.GetComponent<Particle2D>()));
=======
            GameObject obj = Instantiate(asteroid);
            obj.transform.position = changeZ;
            GetComponent<ParticleManager>().AddParticle(obj.GetComponent<Particle2D>());
            ForceManager.AddForceGenerator(new PlanetaryForceGenerator(obj.GetComponent<Particle2D>()));
>>>>>>> c5f071aa5bd14544498c9aa0796876c0ee000af3
        }
    }
}
