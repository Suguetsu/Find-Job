using Gaminho;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*The idea of this boss is that he is slow and tiring to kill, he can appear on top of the player and take a lot of damage*/
public class Boss3 : MonoBehaviour
{
    public GameObject[] Parts;
    public GameObject ShotPrefab;
    private int Side = 1;
    private int MyChilds;

    // Use this for initialization
    void Start()
    {
        MyChilds = Parts.Length;
        StartCoroutine(ShowParts(false));
    }

    private IEnumerator ShowParts(bool yes)
    {
        for (int i = 0; i <= Parts.Length - 1; i++)
        {
            yield return new WaitForSeconds(0.3f);
            Parts[i].SetActive(yes);

        }
        StartCoroutine(AttackNow());
    }


    //O ataque é sumir e aparecer perto do jogador
    private IEnumerator AttackNow()
    {
        ScenarioLimits limitesCenario = GameObject.Find("Control").GetComponent<ControlGame>().ScenarioLimit;
        while (true)
        {
            yield return new WaitForSeconds(4f);
            ShowParts(false);

            GameObject _this = GetOneActive();
            _this.SetActive(true);
            Color color = Color.white;
            color.a = 0.1f;
            _this.GetComponent<Image>().color = color;
            Vector3 pos = _this.transform.localPosition;
            pos.x = UnityEngine.Random.Range(limitesCenario.xMin, limitesCenario.xMax);
            pos.y = UnityEngine.Random.Range(limitesCenario.yMin, limitesCenario.yMax);
            _this.transform.localPosition = pos;
            StartCoroutine(Attack(_this.GetComponent<Image>()));

        }
    }

    private IEnumerator Attack(Image image)
    {
        yield return new WaitForSeconds(2f);
        Color color = Color.white;
        color.a = 1f;
        image.color = color;
    }

    private GameObject GetOneActive()
    {
        List<GameObject> activeds = new List<GameObject>();
        for (int i = 0; i <= Parts.Length - 1; i++)
        {
            if (Parts[i] != null)
            {
                activeds.Add(Parts[i]);
            }
        }
        return activeds[UnityEngine.Random.Range(0, activeds.Count - 1)];
    }

    public void KillMe(GameObject my)
    {

        MyChilds--;
        if (MyChilds <= 0)
        {
            //End
            GameObject.Find("Control").GetComponent<ControlGame>().LevelPassed();
            Destroy(gameObject);
        }
        Destroy(my, 0.01f);
    }
}
