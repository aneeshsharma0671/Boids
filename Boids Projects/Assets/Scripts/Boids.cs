using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Boids : MonoBehaviour
{
    public BoidData boidData;
    public Vector2 Area = new Vector2(2,2);
    public void Initialize(Vector2 area,BoidData data)
    {
        Area = area;
        boidData = data;
        transform.position = new Vector3(boidData.Position.x,boidData.Position.y,0);
    }
    public void BoidUpdate()
    {
        WrapinArea();

        Vector2 Acceleration = Vector2.zero;
        Vector2 Velocity = boidData.Velocity;

        Vector2 SeperationForce = boidData.AvoidanceDir*5f;
        Acceleration += SeperationForce;

        Velocity += Acceleration*Time.deltaTime;
        float Speed = Velocity.magnitude;
        Vector2 dir = Velocity/Speed;
        Speed = Mathf.Clamp(Speed,1f,1.5f);

        Velocity = dir*Speed;

        boidData.Velocity = Velocity;
        transform.position += new Vector3(boidData.Velocity.x,boidData.Velocity.y,0)*Time.deltaTime;
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
        Gizmos.color = new Color(1,0,0,1);
        Vector3 endPoint = new Vector3(transform.position.x + 0.5f*boidData.Velocity.x,transform.position.y + 0.5f*boidData.Velocity.y,0);
        Gizmos.DrawLine(transform.position,endPoint);
    }

    void OnDrawGizmosSelected()
    {
        // Gizmos.color = new Color(0,1,0,1);
        // Gizmos.DrawWireSphere(transform.position,1.0f);
        Handles.color = new Color(0,1,0,1);
        Handles.DrawWireDisc(transform.position,Vector3.forward,1.0f);

        Gizmos.color = new Color(0,0,1,1);
        Gizmos.DrawLine(transform.position,transform.position + new Vector3(boidData.AvoidanceDir.x,boidData.AvoidanceDir.y,0));
    }
}
