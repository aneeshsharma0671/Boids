using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsHandler : MonoBehaviour
{
    public int NoOfBoids = 10;
    public Vector2 PlayGroundArea = new Vector2(10,10);
    public GameObject BoidParent;
    public GameObject BoidPrefab;
    public Color GizmosColor;
    private List<Boids> boids = new List<Boids>();

    void Start()
    {
        Vector2 A = PlayGroundArea;
        for (int i = 0; i < NoOfBoids; i++)
        {
            GameObject obj = Instantiate(BoidPrefab,new Vector3(Random.Range(-(A.x/2)+0.001f,(A.x/2)-0.001f),Random.Range(-(A.y/2)+-0.001f,(A.y/2)-0.001f)),Quaternion.identity);
            obj.transform.SetParent(BoidParent.transform);
            Boids boid = obj.GetComponent<Boids>();
            boid.Initialize(PlayGroundArea);
            boids.Add(boid);
        }
    }

    void Update()
    {
        foreach (Boids boid in boids)
        {
            boid.BoidUpdate();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = GizmosColor;
        Gizmos.DrawWireCube(new Vector3(0,0,0),new Vector3(PlayGroundArea.x,PlayGroundArea.y,0));
    }


}
