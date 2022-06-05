using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boids : MonoBehaviour
{
    public Vector2 Area = new Vector2(2,2);
    Vector2 forward = new Vector2(0,0);
    Vector2 Velocity = new Vector2(0,0);
    public void Initialize(Vector2 area)
    {
        Area = area;
        Vector2 A = Area;
        Velocity = new Vector2(Random.Range(-(A.x/2),(A.x/2)),Random.Range(-(A.y/2),(A.y/2)));
        Velocity.Normalize();
    }
    public void BoidUpdate()
    {
        WrapinArea();
        transform.position += new Vector3(Velocity.x,Velocity.y,0)*Time.deltaTime;
    }

    void WrapinArea()
    {
        Vector2 newPos = transform.position;
        if(transform.position.x > Area.x/2 || transform.position.x < -Area.x/2)
        {
            newPos.x = -newPos.x;
        }
        if(transform.position.y > Area.y/2 || transform.position.y < -Area.y/2)
        {
            newPos.y = -newPos.y;
        }

        transform.position = new Vector3(newPos.x,newPos.y,0);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1,0,0,1);
        Vector3 endPoint = new Vector3(transform.position.x + Velocity.x,transform.position.y + Velocity.y,0);
        Gizmos.DrawLine(transform.position,endPoint);
    }
}
