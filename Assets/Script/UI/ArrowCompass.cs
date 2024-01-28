using UnityEngine;

namespace UI
{
    public class ArrowCompass : MonoBehaviour
    {
        public Transform target;

        private void Update()
        {
            if (target == null) return;

            transform.LookAt(target);
        }

    }

}
