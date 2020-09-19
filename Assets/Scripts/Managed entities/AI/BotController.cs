using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotController : MonoBehaviour, IController
{
    private NavMeshAgent navMeshAgent;
    private AnimationManager animationManager;

    public float Speed { get; set; }
    public float rotationSpeed;

    public Vector3 EyeLevel { get; set; }
    private Vector3 footOffset;

    public void ApplySettings(EntitySettings settings) {
        Speed = settings.speed;
        rotationSpeed = settings.rotationSpeed;
        EyeLevel = settings.eyeLevel;
        footOffset = settings.footOffset;

        navMeshAgent = transform.gameObject.AddComponent<NavMeshAgent>();
        navMeshAgent.radius = settings.colliderRadius;
        navMeshAgent.height = settings.colliderHeigh;

        animationManager = new AnimationManager(GetComponent<Animator>(), string.Empty);
    }

    private void Update()
    {
        
    }

    public void Goto(Vector3 position, bool immediately) {
        if (immediately) {

        } else {
            navMeshAgent.SetDestination(position);
            AnimateMovement(0, 1);
        }
    }

    private void AnimateMovement(float x, float y) {
        animationManager.SetMovementValues(x, y);
    }

    public void SetAnimation(string animationName) {

    }

    private void OnAnimatorIK() {
        animationManager.AnimateIK(transform.forward, transform.rotation, footOffset);
    }

    public void Look(Vector3 direction) {

    }

    public void Jump() {

    }
}
