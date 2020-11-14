using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject m_playerObject = null;
    [SerializeField] private float      m_detectionRadius = 4f;

    [SerializeField] private Material m_idleMaterial = null;
    [SerializeField] private Material m_chasingMaterial = null;
    
    private NavMeshAgent m_agent;
    private Vector3      m_initialPosition;

    //this method can be used for this class ("enemycontroller") and for it's deriving classes ("patrollingenemycontroller"), can be overriden in patrolllingenemycontroller script
    protected virtual Vector3 GetNextDestination()
    {
        return m_initialPosition;
    }
    
    // Start is called before the first frame update
    void Start()
    { 
        m_agent = gameObject.GetComponent<NavMeshAgent>();
        m_initialPosition = gameObject.transform.position; 
    }

    // Update is called once per frame
    private void Update()
    {
        //when our player is in this specified area the enemy will chase it, if not go back to it's initial position
        if (Vector3.Distance(m_playerObject.transform.position, gameObject.transform.position) < m_detectionRadius)
        {

            m_agent.GetComponent<Renderer>().material = m_chasingMaterial;            
            
            //the destination of our enemy is the player object's position (in Update so that he chases all the time not just once)
            m_agent.SetDestination(m_playerObject.transform.position);
            return;
        }

        // enemy will go back to idle position
            m_agent.GetComponent<Renderer>().material = m_idleMaterial;
            if (m_agent.remainingDistance < 0.5)
            m_agent.SetDestination(GetNextDestination());
        }
}
