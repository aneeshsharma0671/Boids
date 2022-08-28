using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class SimulationSetting : ScriptableObject
{
    public int NoOfBoids = 20;
    public Vector2 PlayGroundArea = new Vector2(10,10);
    [Range(0,10)]
    public float PerceptionRadius = 1.5f;
    [Range(0,10)]
    public float ObstacleDetectionRaysLength = 1.5f;
    [Range(1,360)]
    public float PerceptionAngle = 180f;
    [Range(0,360)]
    public int NoOfRays = 10;
    [Header("Weights")]
    [Range(0,20)]
    public float ObstacleAvoidanceWeight = 20f;
    [Range(0,10)]
    public float SeperationWeight = 1f;
    [Range(0,10)]
    public float AllignmentWeight = 1f;
    [Range(0,10)]
    public float CoheisionWeight = 1f;
}
