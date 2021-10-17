using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Explosion : MonoBehaviour {
    public Sprite[] Sprites;
    private Image image;

    // Use this for initialization
    void Start () {
        if (!GetComponent<Image>())
        {
            Debug.LogError("Missing GameObject " + gameObject.name);
            Destroy(this);
            return;
        }
        image = GetComponent<Image>();
        StartCoroutine( Explode());
	}

    private IEnumerator Explode()
    {
        for(int i = 0; i <= Sprites.Length - 1; i++)
        {
            image.sprite = Sprites[i];
            yield return new WaitForSeconds(0.1f);

        }

        Destroy(gameObject);
    }

    
}
