using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : MonoBehaviour
{
    // Start is called before the first frame update
    public float radius = 2.0f;
    public Vector3 offset = new Vector3(0, 0, 0);
    public Vector3 worldTarget;
    NavMeshAgent m_Agent;
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    public void UpdateWander()
    {
        if (!m_Agent) return;

        if (!m_Agent.pathPending && !m_Agent.hasPath)
        {
            WanderFunc();
        }
    }
    void WanderFunc()
    {
        // parameters: float radius, offset;
        //Vector3 localTarget = UnityEngine.Random.insideUnitCircle * radius;
        //localTarget += offset;
        //worldTarget = transform.TransformPoint(localTarget);
        //worldTarget.y = 0f;
        worldTarget = RandomNavSphere(transform.position, radius, -1);
        m_Agent.SetDestination(worldTarget);
    }

    public Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }
}
