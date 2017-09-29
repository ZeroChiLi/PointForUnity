using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChickenManager : MonoBehaviour 
{
    public bool autoMoveForward;
    public float speed = 0.02f;
    public Animator animator;
    public NavMeshAgent navMesh;

    public void SetNextWayPoint(Vector3 pos)
    {
        navMesh.enabled = true;
        navMesh.destination = pos;
    }

    private void Update()
    {
        if (autoMoveForward)
        {
            transform.position += transform.forward * speed;
            animator.SetBool("Moving", true);
        }
        else if (navMesh != null && navMesh.isActiveAndEnabled)
            animator.SetBool("Moving", Mathf.Abs(navMesh.remainingDistance) > navMesh.stoppingDistance);
        else
            animator.SetBool("Moving", false);
    }

}
