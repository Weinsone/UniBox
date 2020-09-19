using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotController : MonoBehaviour, IController
{
    private NavMeshAgent navMeshAgent;

    public float Speed { get; set; }
    public float rotationSpeed;

    public Vector3 EyeLevel { get; set; }

    public void ApplySettings(EntitySettings settings) {
        Speed = settings.speed;
        rotationSpeed = settings.rotationSpeed;
        EyeLevel = settings.eyeLevel;
    }

    private void Start()
    {
        navMeshAgent = transform.gameObject.AddComponent<NavMeshAgent>();
    }

    private void Update()
    {
        
    }

    public void SetAnimation(string animationName) {

    }

    public void Goto(Vector3 position, bool immediately) {
        if (immediately) {

        } else {
            navMeshAgent.SetDestination(position);
        }
    }

    public void Look(Vector3 direction) {

    }

    public void Jump() {

    }
}
