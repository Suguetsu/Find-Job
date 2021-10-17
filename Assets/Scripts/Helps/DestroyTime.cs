using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTime : MonoBehaviour {

    public float delay = 2f; // in Secounds

    private void Start()
    {
        StartCoroutine(CallDestroy());
    }

    private IEnumerator CallDestroy()
    {
        yield return new WaitForSeconds(delay);
        if(gameObject)
        {
            Destroy(gameObject);
        }
    }
}
