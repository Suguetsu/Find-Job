using Gaminho;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControl : MonoBehaviour
{
    public ControlGame controlGame;
    public AudioClip AudioBoss;
    public Transform LocalToCreateEnemies;// Here is where we create the enemy




    void Start()
    {
        StartCoroutine(Process());

    }
    //Basically the function counts how many enemies you have and creates more randomly if released.
    private IEnumerator Process()
    {
        while (true)
        {
            yield return new WaitForSeconds(controlGame.Levels[Statics.CurrentLevel].TempSpaw);

            if (GameObject.FindGameObjectsWithTag("Enemy").Length < controlGame.Levels[Statics.CurrentLevel].EnemyQty)
            {
                EnemyCreate();
            }
            //If you've already killed all the necessary ones, call Chief
            if (Statics.EnemiesDead >= controlGame.Levels[Statics.CurrentLevel].QtyForBoss)
            {
                StartCoroutine(CallBoss());
                break;
            }
        }
    }


    //Creates the enemy based on the prefab to be drawn
    private void EnemyCreate()
    {
        int listaLength = controlGame.Levels[Statics.CurrentLevel].Enemies.Length;              // pega o tamanho da lista
        int enemyNumber = UnityEngine.Random.Range(0, listaLength);                             // pega o numero sorteado;

        GameObject GoIni = Instantiate(controlGame.Levels[Statics.CurrentLevel].Enemies[enemyNumber], LocalToCreateEnemies);

        if (GoIni.GetComponent<Collider2D>() != null)
            GoIni.GetComponent<Collider2D>().isTrigger = true;






        Vector3 pos = GoIni.transform.localPosition;
        if (UnityEngine.Random.Range(0, 100) > 50)
        {
            //Axis X
            pos.x = UnityEngine.Random.Range(controlGame.ScenarioLimit.xMin, controlGame.ScenarioLimit.xMax);
            if (UnityEngine.Random.Range(0, 100) > 50)
            {
                pos.y = controlGame.ScenarioLimit.yMin - 100;
            }
            else
            {
                pos.y = controlGame.ScenarioLimit.yMax + 100;
            }


        }
        else
        {
            pos.y = UnityEngine.Random.Range(controlGame.ScenarioLimit.yMin, controlGame.ScenarioLimit.yMax);
            if (UnityEngine.Random.Range(0, 100) > 50)
            {
                pos.x = controlGame.ScenarioLimit.xMin - 100;
            }
            else
            {
                pos.x = controlGame.ScenarioLimit.xMax + 100;
            }
        }


        GoIni.transform.localPosition = pos;
        float forca = 100f * GoIni.GetComponent<Rigidbody2D>().mass;
        //If it's a meteor, it will be "pushed" on the screen in a random direction
        if (GoIni.GetComponent<Enemy>().MyType == Statics.TYPE_ENEMY.METEOR)
        {
            if (GoIni.transform.localPosition.x < 0)
            {
                GoIni.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.right * forca, ForceMode2D.Impulse);
            }
            else
            {
                GoIni.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * forca, ForceMode2D.Impulse);
            }
            if (GoIni.transform.localPosition.y < 0)
            {
                GoIni.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * forca, ForceMode2D.Impulse);
            }
            else
            {
                GoIni.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.down * forca, ForceMode2D.Impulse);
            }

        }
        else if (Statics.Player != null)    // pequeno ajuste <<<
        {
            //If it is a Ship, it will be propelled towards the Player.
            Vector3 dir = Statics.Player.localPosition - GoIni.transform.localPosition;

            dir = dir.normalized;
            GoIni.GetComponent<Rigidbody2D>().AddForce(dir * forca, ForceMode2D.Impulse);
        }

    }

    private IEnumerator CallBoss()
    {
        //Change Audio
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().PlayOneShot(AudioBoss);
        yield return new WaitForSeconds(5f);

        GameObject GoIni = Instantiate(controlGame.Levels[Statics.CurrentLevel].Boss, LocalToCreateEnemies);

        // depois vi que não tem necessidade, mas tá aí.
        // adiciona trigger aos game objects filho.

        if (GoIni.transform.childCount > 0 && GoIni.GetComponent<BoxCollider2D>() != null)
        {
            for (int i = 0; i < GoIni.transform.childCount; i++)
            {
                GoIni.transform.GetChild(i).gameObject.AddComponent<BoxCollider2D>();
                GoIni.transform.GetChild(i).GetComponent<BoxCollider2D>().isTrigger = true;
                GoIni.transform.GetChild(i).GetComponent<BoxCollider2D>().size = new Vector2(3, 3);
            }
        }
    }
}
