using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemberConfig : MonoBehaviour
{
    public float maxFOV = 180;
    public float maxAcceleration;
    public float maxVelocity;

    // Wandering ? 

    public float wanderJitter;
    public float wanderRadius;
    public float wanderDistance;
    public float wanderPriority;

    // Cohesion Variables
    public float cohesionRadius;
    public float cohesionPriority;

    // Alignment

    public float alignmentRadius;
    public float alignmentPriority;

    // Seperation

    public float seperationRadius;
    public float seperationPriority;

    // Avoidance
    public float avoidanceRadius;
    public float avoidancePriority;

    // Player Tracking

    public float trackingPriority;

}
