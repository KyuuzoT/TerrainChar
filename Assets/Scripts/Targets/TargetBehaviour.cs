using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Targets
{
    public class TargetBehaviour : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if(collision.collider.tag.Equals("Bullet"))
            {
                Debug.Log(gameObject);
                Destroy(gameObject, 2.0f);
                FirstPersonController.destroyedTargets++;
                Debug.Log($"Destroyed: {FirstPersonController.destroyedTargets}");
            }
        }
    }
}
