using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class SimulationSetting : ScriptableObject
{
    public int NoOfBoids = 20;
    [Range(0,10)]
    public float PerceptionRadius = 1.5f;
    [Header("Weights")]
    [Range(0,10)]
    public float SeperationWeight = 1f;
    [Range(0,10)]
    public float AllignmentWeight = 1f;
    [Range(0,10)]
    public float CoheisionWeight = 1f;
}
