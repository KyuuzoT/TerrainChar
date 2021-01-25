using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float timeBeforeDestruction = 2.0f;
    private void OnEnable()
    {
        StartCoroutine(DestroyBulletInTime());
    }

    private IEnumerator DestroyBulletInTime()
    {
        yield return new WaitForSeconds(timeBeforeDestruction);
        Destroy(gameObject);
    }
}
