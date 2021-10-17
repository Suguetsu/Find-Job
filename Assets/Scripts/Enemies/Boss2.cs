using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour {
    public GameObject[] Parts;
    public GameObject ShotPrefab;
    private int Side = 1;
    private int MyChilds;

    // Use this for initialization
    void Start () {
        MyChilds = Parts.Length;
        StartCoroutine( ShowParts());
	}

    private IEnumerator ShowParts()
    {
        for(int i = 0; i <= Parts.Length - 1; i++)
        {
            yield return new WaitForSeconds(0.3f);
            Parts[i].SetActive(true);
        }
        StartCoroutine(Shot());
    }


    void Update () {
		if(Side == 1)
        {
            transform.localPosition = new Vector3(transform.localPosition.x + 3f, transform.localPosition.y, 0);
        }
        else
        {
            transform.localPosition = new Vector3(transform.localPosition.x - 3f, transform.localPosition.y, 0);
        }

        if(transform.localPosition.x > 1500)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            Side = 2;
        }
        if (transform.localPosition.x < -1200)
        {
            transform.localScale = new Vector3(1, 1, 1);
            Side = 1;
        }
    }

    private IEnumerator Shot()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.8f);
            GameObject goTiro = Instantiate(ShotPrefab, Vector3.zero, Quaternion.identity);
            goTiro.tag = "EnemyShot";
            goTiro.transform.parent = transform;
            goTiro.transform.localPosition = GetOneActive().transform.localPosition;
            goTiro.GetComponent<Rigidbody2D>().AddForce(Vector2.down* 14000f * Time.deltaTime, ForceMode2D.Impulse);
            goTiro.AddComponent<BoxCollider2D>();
            goTiro.AddComponent<BoxCollider2D>().isTrigger = true;
            goTiro.transform.localScale = new Vector3(3, 3, 3);
            goTiro.transform.parent = transform.parent;
        }
    }
    // Here you get one of the active parts (which hasn't been killed yet)
    private GameObject GetOneActive()
    {
        List<GameObject> actives = new List<GameObject>();
        for(int i = 0; i <= Parts.Length - 1; i++)
        {
            if (Parts[i] != null)
            {
                actives.Add(Parts[i]);
            }
        }
        return actives[UnityEngine.Random.Range(0, actives.Count-1)];
    }

    public void KillMe(GameObject my)
    {
        Destroy(my);
        MyChilds--;
        if(MyChilds <= 0)
        {
            //End
            GameObject.Find("Control").GetComponent<ControlGame>().LevelPassed();
            Destroy(gameObject);
        }
    }
}
