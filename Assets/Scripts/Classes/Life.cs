using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;

public class Life : MonoBehaviour
{
    public int life = 10;
    public UnityEvent RunWhenDies;
    public bool ControlCollision = false;
    public GameObject Explosion;
    private ControlGame _control;


    public void Start()
    {
        _control = FindObjectOfType(typeof(ControlGame)) as ControlGame;

        if (transform.gameObject.tag == "Player")
        {
            PlayerPrefs.SetInt("LifesCount", 1);
        }



    }
    public void TakesLife(int Qtd)
    {

        life -= Qtd;




        if (life <= 0)
        {

            if (RunWhenDies != null)
            {
                RunWhenDies.Invoke();//Chama o Evento anexado se ele existir
            }
            else
            {


                Destroy(gameObject);
            }

            if (transform.gameObject.tag == "Player")
                PlayerPrefs.SetInt("LifesCount", 0);
        }
        else if (transform.gameObject.tag == "Player")
        {
            PlayerPrefs.SetInt("LifesCount", 1);
        }
       




        _control.Save(); // <<< salva toda vez que toma um hit.





    }


    //Colisão
    private void OnTriggerEnter2D(Collider2D _object)
    {
        if (!ControlCollision) return;

        if (_object.gameObject.tag == "Shot")
        {
            Instantiate(Explosion, transform).transform.parent = transform.parent;
            TakesLife(1);
            Destroy(_object.gameObject);
        }
        if (_object.gameObject.tag == "Player")
        {
            _object.GetComponent<Life>().TakesLife(1);

        }
    }




}
