using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private List<Transform> path;

    private Queue<Transform> curentPath;

    public event Action OnPathEmpty;

    private bool isPathEndFired = true;
    
    public void SetPath(Transform[] pathTransforms)
    {
        curentPath = new Queue<Transform>(pathTransforms);
    }
    
    private void Update()
    {
        if (Mathf.Approximately(agent.remainingDistance, 0) && curentPath != null && curentPath.TryDequeue(out var nextPoint))
        {
            isPathEndFired = false;
            agent.SetDestination(nextPoint.position);
        }
        else if(curentPath != null && !isPathEndFired && Mathf.Approximately(agent.remainingDistance, 0))
        {
            OnPathEmpty?.Invoke();
            isPathEndFired = true;
        }
    }
}