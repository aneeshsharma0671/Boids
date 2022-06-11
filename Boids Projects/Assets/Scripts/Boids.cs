using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class Boids : MonoBehaviour
{
    public BoidData boidData;
    private Transform visuals;
    private SimulationSetting setting;
    public Vector2 Area = new Vector2(2,2);
    public void Initialize(Vector2 area,BoidData data,SimulationSetting simSetting)
    {
        setting = simSetting;
        Area = area;
        boidData = data;
        transform.position = new Vector3(boidData.Position.x,boidData.Position.y,0);
        visuals = gameObject.transform.GetChild(0).transform;
    }
    public void BoidUpdate()
    {
        WrapinArea();

        Vector2 Acceleration = Vector2.zero;
        Vector2 Velocity = boidData.Velocity;
        //Obstacle Avoidance Dir 
        if(boidData.isHeadingToObstacle)
        {
            Vector2 ObstacleAvoidanceDir = boidData.ObstacleAvoidanceDir;
            Acceleration += ObstacleAvoidanceDir*setting.ObstacleAvoidanceWeight;
        }
        // Seperation Force
        Vector2 SeperationForce = boidData.AvoidanceDir*setting.SeperationWeight;
        Acceleration += SeperationForce;
        // Allignment Force
        Vector2 AllignmentForce = boidData.AllignmentDir*setting.AllignmentWeight;
        Acceleration += AllignmentForce;
        // Coheision Force
        Vector2 CoheisionForce = boidData.CohesionDir*setting.CoheisionWeight;
        Acceleration += CoheisionForce;

        Velocity += Acceleration*Time.deltaTime;
        float Speed = Velocity.magnitude;
        Vector2 dir = Velocity/Speed;
        Speed = Mathf.Clamp(Speed,1f,1.5f);
        boidData.Forward = dir;

        Velocity = dir*Speed;

        boidData.Velocity = Velocity;
        transform.position += new Vector3(boidData.Velocity.x,boidData.Velocity.y,0)*Time.deltaTime;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        visuals.localRotation = Quaternion.Euler(0,0,Mathf.Rad2Deg*Mathf.Atan2(boidData.Forward.y,boidData.Forward.x));
    }

    void WrapinArea()
    {
        Vector2 newPos = transform.position;
        if(transform.position.x > Area.x/2 || transform.position.x < -Area.x/2)
        {
            newPos.x = -newPos.x + (newPos.x>0?0.01f:-0.01f);
        }
        if(transform.position.y > Area.y/2 || transform.position.y < -Area.y/2)
        {
            newPos.y = -newPos.y+ (newPos.y>0?0.01f:-0.01f);
        }

        transform.position = new Vector3(newPos.x,newPos.y,0);
    }

    void OnDrawGizmos()
    {

    }

    void OnDrawGizmosSelected()
    {
        // Gizmos.color = new Color(0,1,0,1);
        // Gizmos.DrawWireSphere(transform.position,1.0f);
        Handles.color = new Color(0,1,0,1);
        Handles.DrawWireDisc(transform.position,Vector3.forward,setting.PerceptionRadius);

        Gizmos.color = new Color(1,0,0,1);
        Vector3 endPoint = new Vector3(transform.position.x + 0.5f*boidData.Velocity.x,transform.position.y + 0.5f*boidData.Velocity.y,0);
        Gizmos.DrawLine(transform.position,transform.position + new Vector3 (boidData.Forward.x,boidData.Forward.y,0)*0.5f);

        Gizmos.color = new Color(0,1,1,1);
        Gizmos.DrawLine(transform.position,transform.position + new Vector3(boidData.AllignmentDir.x,boidData.AllignmentDir.y,0)*1.5f);  

        Gizmos.color = new Color(0,0,1,1);
        Gizmos.DrawLine(transform.position,transform.position + new Vector3(boidData.AvoidanceDir.x,boidData.AvoidanceDir.y,0));

        Handles.color = new Color(1,0,1,1);
        Gizmos.DrawLine((Vector3)boidData.Position, (Vector3)boidData.Position + (Vector3)boidData.CohesionDir);

        Handles.color = new Color(0.5f,0.5f,1,1);
        Gizmos.DrawLine((Vector3)boidData.Position, (Vector3)boidData.Position + (Vector3)boidData.ObstacleAvoidanceDir);
    }
}
