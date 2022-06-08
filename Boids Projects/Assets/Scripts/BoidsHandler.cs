using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsHandler : MonoBehaviour
{
    public SimulationSetting setting;
    public GameObject BoidParent;
    public GameObject BoidPrefab;
    public Color GizmosColor;
    private List<Boids> boids = new List<Boids>();

    void Start()
    {
        Vector2 A =  setting.PlayGroundArea;
        for (int i = 0; i < setting.NoOfBoids; i++)
        {
            GameObject obj = Instantiate(BoidPrefab);
            obj.transform.SetParent(BoidParent.transform);
            Boids boid = obj.GetComponent<Boids>();
            BoidData data = new BoidData();
            data.Position = new Vector2(Random.Range(-(A.x/2)+0.01f,(A.x/2)-0.01f),Random.Range(-(A.y/2)+0.01f,(A.y/2)-0.01f));
            data.Velocity = new Vector2(Random.Range(-(A.x/2),(A.x/2)),Random.Range(-(A.y/2),(A.y/2)));
            data.Velocity.Normalize();
            data.Forward = data.Velocity;
            data.Neighbours = new List<Boids>();
            boid.Initialize(setting.PlayGroundArea,data,setting);
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
                    if(Vector2.Distance(boid.boidData.Position,subBoid.boidData.Position) < setting.PerceptionRadius)
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
            Vector2 AvegareForward = new Vector2(0,0);
            Vector2 CentreOfFlock = new Vector2(0,0);
            foreach(Boids neighbor in boid.boidData.Neighbours)
            {
                AverageDir += (neighbor.boidData.Position - boid.boidData.Position);
                AvegareForward += neighbor.boidData.Forward;
                CentreOfFlock += neighbor.boidData.Position/boid.boidData.Neighbours.Count;
            }
            avoidanceForce = (AverageDir.normalized)*-1;
            boid.boidData.AvoidanceDir = avoidanceForce;
            boid.boidData.CohesionDir = (CentreOfFlock - boid.boidData.Position).normalized;
            boid.boidData.AllignmentDir = AvegareForward.normalized;
        }

        foreach (Boids boid in boids)
        {
            boid.BoidUpdate();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = GizmosColor;
        Gizmos.DrawWireCube(new Vector3(0,0,0),new Vector3(setting.PlayGroundArea.x,setting.PlayGroundArea.y,0));
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
    public Vector2 AllignmentDir;
    public Vector2 CohesionDir;
}
