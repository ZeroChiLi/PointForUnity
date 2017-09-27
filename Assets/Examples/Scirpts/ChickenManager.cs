using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChickenManager : MonoBehaviour 
{
    public Animator animator;
    public NavMeshAgent navMesh;

    public void SetNextWayPoint(Vector3 pos)
    {
        navMesh.destination = pos;
    }

    private void Update()
    {
        if (navMesh != null)
            animator.SetBool("Moving", Mathf.Abs(navMesh.remainingDistance) > navMesh.stoppingDistance); 
    }

}
