using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMission : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform cible;
    // Start is called before the first frame update
    void Start()
    {
        agent.destination = cible.position;
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = cible.position;
    }
}
