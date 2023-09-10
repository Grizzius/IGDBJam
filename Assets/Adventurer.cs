using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Adventurer : MonoBehaviour
{
    public GameMode gameMode;
    NavMeshAgent agent;
    bool isLeaving = false;
    bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(agent.remainingDistance <= agent.stoppingDistance && isMoving)
        {
            isMoving = false;
            if (isLeaving)
            {
                gameMode.AdventurerLeavesTavern();
            }
            else
            {
                gameMode.AdventurerReachBar();
            }
        }
    }

    public void Leave(Transform destination)
    {
        StartCoroutine(LeaveCoroutine(destination));
    }

    IEnumerator LeaveCoroutine(Transform destination)
    {
        agent.destination = destination.position;
        yield return new WaitForEndOfFrame();
        isMoving = true;
        isLeaving = true;
    }

    public void GoToBar(Transform destination)
    {
        if(agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
        agent.destination = destination.position;
        isMoving = true;
        isLeaving = false;
        
    }
}
