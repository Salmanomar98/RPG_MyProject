using RPG.Combat;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        NavMeshAgent navMeshAgent;
        Health health;
        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }
        void Update()
        {
            navMeshAgent.enabled = !health.IsDead();

            UpdateAnimation();
        }
        public void StartMoveAction(Vector3 hit)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            GetComponent<Fighter>().Cancel();
            GetComponent<NavMeshAgent>().destination = hit;
            GetComponent<NavMeshAgent>().isStopped = false;
        }
        public void MoveTo(Vector3 hit)
        {
            GetComponent<NavMeshAgent>().destination = hit;
            GetComponent<NavMeshAgent>().isStopped = false;
        }

        public void Cancel()
        {
            GetComponent<NavMeshAgent>().isStopped = true;
        }
 
        private void UpdateAnimation()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }
    }

}

