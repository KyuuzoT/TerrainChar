using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.Targets
{
    public class ChildrenWatcher : MonoBehaviour
    {
        private void FixedUpdate()
        {
            var children = transform.childCount;
            if(children <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
