using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject planets;
    private Transform closestPlanet;

    public float gravityMag;
    public Vector3 gravityDir;

    PolygonCollider2D myCollider;
    public Rigidbody2D body;

    void AddGravityForce(Vector3 g) {
        // calculate the necessary force to produce the desired gravity:
        GetComponent<ConstantForce2D>().force = GetComponent<Rigidbody2D>().mass * g;
    }

    void SetGravity() {
        gravityDir = closestPlanet.position - transform.position;
        AddGravityForce(gravityMag * gravityDir);
    }

    // Start is called before the first frame update
    void Start()
    {
        planets = GameObject.Find("Planets");
        myCollider = GetComponent<PolygonCollider2D>();
        body = GetComponent<Rigidbody2D>();
        body.gravityScale = 0; // forget about physics default gravity
        gravityMag = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FindClosestPlanet();

        Collider2D closestPlanetCollider = closestPlanet.transform.GetComponent<Collider2D>();
        if(myCollider.IsTouching(closestPlanetCollider)) Destroy(gameObject);

        SetGravity();
    }

    private void FindClosestPlanet() {
        float minDist = float.MaxValue;
        foreach(Transform child in planets.transform) {
            float dist = Vector3.Distance(child.position, transform.position);
            if(dist < minDist) {
                minDist = dist;
                closestPlanet = child;
            }
        }
    }
}
