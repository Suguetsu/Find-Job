using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using Gaminho;

public class ControlShip : MonoBehaviour
{
    #region Public

    public float Velocity = 10f;
    public float SpeedRotation = 200.0f;

    [Range(0, 2)]
    public float speedPhantom;
    [SerializeField]
    private bool isPlayer;             // << boolena que identifica se é o player que á jogando.
    public ControlGame controlGame;
    public Life life;
    public GameObject MotorAnimation;
    public GameObject Shield;
    public GameObject UIShield;
    public GameObject Explosion;
    private GameObject PhantomPlayer;
    public bool ShieldTeste;
    public bool isGhost;
    public Shot[] Shots;
    private Rigidbody2D _rb;
    #endregion

    #region Private
    private Vector3 startPos;
    private bool shooting = false;
    private int lifeShield;
    private float bagVel = 2;


    #endregion
    private void Awake()
    {
        Statics.Player = gameObject.transform; // << pega as informações do player antes do carregamento


    }

    void Start()
    {
        Statics.WithShield = ShieldTeste;
        lifeShield = Statics.ShieldLife;
        _rb = GetComponent<Rigidbody2D>();



        if (_rb == null)
        {
            Debug.LogError("Component required Rigidbody2D");
            Destroy(this);
            return;
        }

        if (GetComponent<BoxCollider2D>() == null)
        {
            Debug.LogWarning("BoxCollider2D not found, adding ...");
            gameObject.AddComponent<BoxCollider2D>();

        }

        startPos = transform.localPosition;
        _rb.gravityScale = 0.001f;

        StartCoroutine(Shoot());

        if (isPlayer && !isGhost)
        {
            Invoke("Phantom", 1);

        }
        else if (isGhost)  // verifica se o player é um fantasma e diminui a opacidade dos sprites
        {
            Image a = GetComponent<Image>();
            a.color = isGhost ? new Color(1, 1, 1, 0.2f) : Color.white;


            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<Image>() != null && i > 0)
                    transform.GetChild(i).GetComponent<Image>().color = a.color;
                else
                    transform.GetChild(i).gameObject.SetActive(!isGhost);
            }

        }


    }

    void LateUpdate()
    {


        #region Tiro
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            shooting = true;
            isPlayer = true;    //>> identifica o comando externo caracterizando que a nave é do player.
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            shooting = false;
        }



        #endregion

        MovementActions();



        if (!isGhost)
            Statics.Life = life.life;

    }

    private void MovementActions()
    {
        #region Move
        float rotation = Input.GetAxis("Horizontal") * SpeedRotation;
        rotation *= Time.deltaTime;
        transform.Rotate(0, 0, -rotation);

        if (!isGhost)
        {


            if (transform.localPosition.y > controlGame.ScenarioLimit.yMax || transform.localPosition.y < controlGame.ScenarioLimit.yMin || transform.localPosition.x > controlGame.ScenarioLimit.xMax || transform.localPosition.x < controlGame.ScenarioLimit.xMin)
            {

                Vector3 dir = startPos - transform.localPosition;
                dir = dir.normalized;
                float temp = 1;

                temp = temp >= 5 ? temp = 5 : temp += 1 * Time.deltaTime;
                _rb.AddForce(dir * temp * (GetComponent<Rigidbody2D>().mass), ForceMode2D.Impulse);
                Debug.Log("Sai da tela");

            }
            else
            {



                if (Input.GetAxis("Vertical") != 0)
                {

                    bagVel = bagVel > 0.5 ? bagVel -= Time.deltaTime : bagVel = 0.9f;

                    Debug.Log("bgVel " + bagVel);
                    Vector2 translation = Input.GetAxis("Vertical") * new Vector2(0, (Velocity * bagVel) * _rb.mass);
                    translation *= Time.deltaTime;
                    _rb.AddRelativeForce(translation, ForceMode2D.Impulse);
                }
                else
                {
                    if (bagVel < 2)
                    {
                        bagVel++;
                    }

                   // bagVel = bagVel <= 8 ? bagVel++ : 8; // carrega enquanto estiver desapertado


                    _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y) * (1 - Time.deltaTime);


                    Debug.Log("soltei " + bagVel);

                }





                Debug.Log("dentro da tela da tela");
            }

            controlGame.player = this.transform;
        }
        else
        {
            transform.position = Vector2.Lerp(transform.position, controlGame.player.position, speedPhantom * Time.deltaTime);
        }


        AnimateMotor();
        #endregion

    }

    private void AnimateMotor()
    {
        if (MotorAnimation.activeSelf != (Input.GetAxis("Vertical") != 0))
        {
            MotorAnimation.SetActive(Input.GetAxis("Vertical") != 0);
        }
    }


    private IEnumerator Shoot()
    {
        while (true)

        {
            int ShootKind = Statics.ShootingSelected;
            if (isGhost)
            {
                ShootKind = 0;
            }

            yield return new WaitForSeconds(Shots[ShootKind].ShootingPeriod);

            if (shooting)

            {




                Statics.Damage = Shots[ShootKind].Damage;
                GameObject goShoot = Instantiate(Shots[ShootKind].Prefab, Vector3.zero, Quaternion.identity);
                goShoot.transform.parent = transform;
                goShoot.transform.localPosition = Shots[ShootKind].Weapon.transform.localPosition;
                goShoot.GetComponent<Rigidbody2D>().AddForce(transform.up * ((Shots[ShootKind].SpeedShooter * 12000f) * Time.deltaTime), ForceMode2D.Impulse);
                goShoot.AddComponent<BoxCollider2D>();
                goShoot.GetComponent<BoxCollider2D>().isTrigger = true;
                goShoot.GetComponent<BoxCollider2D>().size = new Vector2(5,5);

              goShoot.transform.parent = transform.parent;

                goShoot.gameObject.tag = isPlayer ? "PlayerShot" : "EnemyShot";   // <<<<<<

                if (Shots[ShootKind].TypeShooter == Statics.TYPE_SHOT.DOUBLE)
                {
                    GameObject goShoot2 = Instantiate(Shots[ShootKind].Prefab, Vector3.zero, Quaternion.identity);
                    goShoot2.transform.parent = transform;
                    goShoot2.transform.localPosition = Shots[ShootKind].Weapon2.transform.localPosition;
                    goShoot2.GetComponent<Rigidbody2D>().AddForce(transform.up * ((Shots[ShootKind].SpeedShooter * 12000f) * Time.deltaTime), ForceMode2D.Impulse);
                    goShoot2.AddComponent<BoxCollider2D>();
                    goShoot2.GetComponent<BoxCollider2D>().isTrigger = true;
                    goShoot.GetComponent<BoxCollider2D>().size = new Vector2(5, 5);
                    goShoot2.transform.parent = transform.parent;

                    goShoot2.gameObject.tag = isPlayer ? "PlayerShot" : "EnemyShot";  // <<<<<<
                }

                if (Shots[ShootKind].TypeShooter == Statics.TYPE_SHOT.TRIPLE)
                {
                    GameObject goShoot2 = Instantiate(Shots[ShootKind].Prefab, Vector3.zero, Quaternion.identity);
                    goShoot2.transform.parent = transform;
                    goShoot2.transform.localPosition = Shots[ShootKind].Weapon2.transform.localPosition;
                    goShoot2.GetComponent<Rigidbody2D>().AddForce(transform.up * ((Shots[ShootKind].SpeedShooter * 12000f) * Time.deltaTime), ForceMode2D.Impulse);
                    goShoot2.AddComponent<BoxCollider2D>();
                    goShoot2.GetComponent<BoxCollider2D>().isTrigger = true;
                    goShoot.GetComponent<BoxCollider2D>().size = new Vector2(5, 5);
                    goShoot2.transform.parent = transform.parent;

                    goShoot2.gameObject.tag = isPlayer ? "PlayerShot" : "EnemyShot";   // <<<<<<

                    GameObject goTiro3 = Instantiate(Shots[ShootKind].Prefab, Vector3.zero, Quaternion.identity);
                    goTiro3.transform.parent = transform;
                    goTiro3.transform.localPosition = Shots[ShootKind].Weapon3.transform.localPosition;
                    goTiro3.GetComponent<Rigidbody2D>().AddForce(transform.up * ((Shots[ShootKind].SpeedShooter * 12000f) * Time.deltaTime), ForceMode2D.Impulse);
                    goTiro3.AddComponent<BoxCollider2D>();
                    goTiro3.GetComponent<BoxCollider2D>().isTrigger = true;
                    goShoot.GetComponent<BoxCollider2D>().size = new Vector2(5, 5);
                    goTiro3.transform.parent = transform.parent;

                    goTiro3.gameObject.tag = isPlayer ? "PlayerShot" : "EnemyShot";    // <<<<<<

                }
            }

            CallShield();
        }
    }

    private void CallShield()
    {
        float temp = 0;
        if (Shield.activeSelf != Statics.WithShield)
        {
            Shield.SetActive(Statics.WithShield);
            UIShield.gameObject.SetActive(Statics.WithShield);

        }
        else if (Statics.WithShield)
        {
            temp = (float)lifeShield / 10;
            Debug.Log(temp);
            UIShield.transform.GetChild(0).localScale = new Vector3(temp, 1, 1);
        }


    }

    private void OnTriggerEnter2D(Collider2D obj)
    {

        if (obj.gameObject.tag == "Enemy")
        {
            Instantiate(Explosion, transform);

            if (Statics.WithShield)
            {
                lifeShield--;

            }
            else
            {

                life.TakesLife(obj.gameObject.GetComponent<Enemy>().Damage);
            }

        }

        if (obj.gameObject.tag == "Shot" || obj.gameObject.tag == "EnemyShot")
        {
            Instantiate(Explosion, transform);

            if (Statics.WithShield)
            {
                lifeShield--;
            }
            else
            {

                life.TakesLife(1);
            }
            Destroy(obj.gameObject);
        }

        if (lifeShield <= 0)
        {
            Statics.WithShield = false;
            lifeShield = 10;
        }

        Statics.ShieldLife = lifeShield;
    }

    public void Phantom()
    {

        PhantomPlayer = isPlayer ? Instantiate(gameObject, transform.parent) : null;
        ControlShip a = PhantomPlayer.GetComponent<ControlShip>();

        a.isGhost = true;
        a.isPlayer = a.isGhost;
        a.GetComponent<BoxCollider2D>().enabled = false;



        Debug.Log("Chamei fantasm");
    }

}


