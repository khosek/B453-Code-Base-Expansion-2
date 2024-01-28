using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Unity.VisualScripting;


namespace Units
{
    public class Enemy : MonoBehaviour
    {
        public EnemyData enemyData;

        public NavMeshAgent navMeshAgent;

        public Transform[] waypoints;

        private int destinationPointIndex = 0;

        private float attackCooldown = 0f;

        private void OnCollisionEnter(Collision other)
        {
            if (Time.time < attackCooldown) return;

            attackCooldown = Time.time + enemyData.attackRate;

            if (!other.gameObject.CompareTag("Player")) return;

            Rigidbody playerRb = other.gameObject.GetComponent<Rigidbody>();

            if (playerRb == null) return;

            Vector3 knockbackDirection = other.transform.position - transform.position;

            playerRb.AddForce(knockbackDirection.normalized + other.transform.up * enemyData.knockbackForce, ForceMode.Impulse);

            Health health = other.gameObject.GetComponent<Health>();

            if (health == null || health.player.cargo == null) return;

            health.DamagePlayer(enemyData.attackPoints);

        }

        void Start()
        {
            GoToNextPoint();

            navMeshAgent.speed = enemyData.speed;
        }

        private void Update()
        {
            if (waypoints.Length == 0 || !navMeshAgent.isActiveAndEnabled) return;

            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f) GoToNextPoint();
        }

        private void GoToNextPoint()
        {
            if (waypoints.Length == 0 || !navMeshAgent.isActiveAndEnabled) return;

            navMeshAgent.destination = waypoints[destinationPointIndex].position;

            destinationPointIndex = (destinationPointIndex + 1) % waypoints.Length;
        }
    }
}
