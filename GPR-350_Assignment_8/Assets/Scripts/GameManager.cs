using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //ForceManager forceManager;
    public GameObject targetPrefab;
    private GameObject target;
    public Vector2 xRange;
    public Vector2 yRange;

    public Text scoreText;
    public int score = 0;

    BouyancyForceGenerator2D bfg;

    // Start is called before the first frame update
    void Start()
    {
        //forceManager = GetComponent<ForceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "" + score;
    }

    void FixedUpdate()
    {
        CheckForTarget();
        ForceManager.ApplyAllForces(Time.fixedDeltaTime);
    }

    void CheckForTarget()
    {
        if(target == null)
        {
            Vector3 pos = new Vector3(Random.Range(xRange.x,xRange.y), Random.Range(yRange.x, yRange.y), 0);
            target = Instantiate(targetPrefab, pos, Quaternion.identity);

            ForceManager.DeleteForceGenerator(bfg);
            bfg = new BouyancyForceGenerator2D(target.GetComponent<Particle2D>(), 1.0f, 0.5f, 0, 1.5f);
            ForceManager.AddForceGenerator(bfg);
        }
    }
}
