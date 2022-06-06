using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsHandler : MonoBehaviour
{
    public float PerceptionRadius = 1.0f;
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
            GameObject obj = Instantiate(BoidPrefab);
            obj.transform.SetParent(BoidParent.transform);
            Boids boid = obj.GetComponent<Boids>();
            BoidData data = new BoidData();
            data.Position = new Vector2(Random.Range(-(A.x/2)+0.01f,(A.x/2)-0.01f),Random.Range(-(A.y/2)+0.01f,(A.y/2)-0.01f));
            data.Velocity = new Vector2(Random.Range(-(A.x/2),(A.x/2)),Random.Range(-(A.y/2),(A.y/2)));
            data.Velocity.Normalize();
            data.Neighbours = new List<Boids>();
            boid.Initialize(PlayGroundArea,data);
            boids.Add(boid);
        }
    }

    void Update()
    {
        foreach (Boids boid in boids)
        {
            boid.boidData.Position = boid.transform.position;
        }

        foreach (Boids boid in boids)
        {
            foreach (Boids subBoid in boids)
            {
                if(boid != subBoid )
                {
                    if(Vector2.Distance(boid.boidData.Position,subBoid.boidData.Position) < PerceptionRadius)
                    {
                        if(!boid.boidData.Neighbours.Contains(subBoid))
                        {
                            boid.boidData.Neighbours.Add(subBoid);
                        }
                    }
                    else
                    {
                        if(boid.boidData.Neighbours.Contains(subBoid))
                        {
                            boid.boidData.Neighbours.Remove(subBoid);
                        }
                    }
                    
                }
            }
        }

        foreach (Boids boid in boids)
        {
            Vector2 avoidanceForce = new Vector2(0,0);
            Vector2 AverageDir = new Vector2(0,0);
            foreach(Boids neighbor in boid.boidData.Neighbours)
            {
                AverageDir += (neighbor.boidData.Position - boid.boidData.Position);
            }
            avoidanceForce = (AverageDir.normalized)*-1;
            boid.boidData.AvoidanceDir = avoidanceForce;
        }

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

[System.Serializable]
public struct BoidData
{
    public Vector2 Position;
    public Vector2 Velocity;
    public Vector2 Forward;
    public Vector2 Area;
    public List<Boids> Neighbours;
    public Vector2 AvoidanceDir;
}
