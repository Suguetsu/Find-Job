using UnityEngine;

namespace Gaminho
{
    [System.Serializable]

    
    public class Shot
    {
        public GameObject Prefab;
        //Weapons are nothing more than the position from which the shot can come out
        public GameObject Weapon;
        public GameObject Weapon2;
        public GameObject Weapon3;
        //Like, it can be 1, 2 or 3 simultaneous shots
        public Statics.TYPE_SHOT TypeShooter;
        public int Damage;
        public float ShootingPeriod;
        public float SpeedShooter;
        
    }
}

