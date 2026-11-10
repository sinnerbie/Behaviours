using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IncompletoBot : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject target; //target sera el cop que lo pondremos en Unity
    
    public enum State { Hide, Pursue, Evade, Wander, Complex };
    public State currentState = State.Hide;

    void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    void Seek(Vector3 location)
    {
        //TODO
    }
    //En seek ponemos como destino la posicion del target, en Flee (huir) ponemos como
    //destino la posicion del target en negativo para que vaya huyendo de esa posicion
    void Flee(Vector3 location)
    {
       //TODO
    }

    void Pursue()
    {
        agent.speed = 7;
        float targetSpeed = target.GetComponent<Drive>().currentSpeed;
        if (targetSpeed == 0)
        {
            Vector3 direction = target.transform.position - transform.position;
            agent.SetDestination(target.transform.position + direction);
        }
        else
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            float newT = distance / targetSpeed;
            Vector3 targetDirection = targetSpeed * target.transform.forward;
            Vector3 futurePos = target.transform.position + (targetDirection * newT);
            agent.SetDestination(futurePos);
        }
        agent.isStopped = false;
    }

    //Evade es practicamente igual a Pursue solo que se debe ir en la direccion contraria
    void Evade()
    {
        agent.speed = 5.25f;
        float targetSpeed = target.GetComponent<Drive>().currentSpeed;
        if (targetSpeed == 0)
        {
            Vector3 direction = transform.position - target.transform.position;
            agent.SetDestination(target.transform.position + direction);
        }
        else
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            float newT = distance / targetSpeed;
            Vector3 targetDirection = targetSpeed * target.transform.forward;
            Vector3 futurePos = target.transform.position + (targetDirection * newT);
            Vector3 evasivePos = transform.position - futurePos;
            agent.SetDestination(evasivePos);
        }
        agent.isStopped = false;
    }


	//Para Wander necesitamos una var. que recordemos en cada llamada y por lo
	//tanto no puede ser local al metodo.
    Vector3 wanderTarget = Vector3.zero; //Se actualiza cada vez que el agente
										 //tiene un nuevo valor en su posicion
    void Wander()
    {
        //TODO
    }

    void Hide()
    {
        agent.speed = 3.5f;
        float closestDis = Mathf.Infinity;
        Transform closestPos = null;
        GameObject[] spots = World.Instance.GetHidingSpots();
        if (spots.Length == 0) { Debug.LogWarning("Failed to find hiding spots");  return; }
        foreach (GameObject ob in spots)
        {
            float dist = Vector3.Distance(transform.position, target.transform.position);
            if (dist < closestDis)
            {
                closestDis = dist;
                closestPos = ob.transform;
            }
        }
        Vector3 direction = target.transform.position - closestPos.position;
        Vector3 hidingPlace = closestPos.position - direction;
        agent.SetDestination(hidingPlace);
        agent.isStopped = false;
    }

    void CleverHide()
    {
        //TODO
    }


    bool CanSeeTarget()
    {
        //TODO
        return false;
    }

    //En el siguiente comportamiento el robber se acerca al cop mientras este no le mira pero, cuando esto sucede, el robber se esconde.
    //Tenemos que decidir cuando el cop ve al rubber, digamos que esto sucede si esta en su angulo de vision (p.e. 60�)
    bool TargetCanSeeMe()
    {
        //TODO
        return false;
    }


    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.Hide:
                Hide();
                break;
            case State.Pursue:
                Pursue();
                break;
            case State.Evade:
                Evade();
                break;
            case State.Wander:

                break;
            case State.Complex:

                break;
            default:
                Debug.LogError(gameObject.name + "'s Current State is not a valid state");
                break;
        }
    }
}