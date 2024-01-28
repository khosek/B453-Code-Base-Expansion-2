using UnityEngine;

namespace Units
{
    [CreateAssetMenu(fileName = "New Enemy Data", menuName = "Enemy Data")]
    public class EnemyData : ScriptableObject
    {
        public new string name = "Enemy";

        public int attackPoints = 1;

        public float attackRate = 1f;

        public float knockbackForce = 100f;

        public float speed = 100f;

    }
}
