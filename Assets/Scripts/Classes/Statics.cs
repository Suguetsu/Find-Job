using UnityEngine;
using System.Collections;
namespace Gaminho
{
    public static class Statics
    {

        public enum FacingDirection { Up = 0, Down = 180, Right = 270, Left = 90 }
        public enum TYPE_SHOT { SIMPLE, DOUBLE, TRIPLE };
        public enum TYPE_ENEMY { METEOR, SHIP, PIECES };
        public enum TYPE_DROP { SHOT1, SHOT2, SHOT3, SHIELD, LIFEFULL };


        public const string PREFAB_LEVELUP = "LevelUp";
        public const string PREFAB_CONGRATULATION = "Congratulations";
        public const string PREFAB_GAMEOVER = "GameOver";
        public const string PREFAB_LOAD = "Loading";
        public const string PREFAB_HISTORY = "History";
        public const string PLAYERPREF_NEWRECORD = "NewRecord";
        public const string PLAYERPREF_VALUE = "ValueRecord";

        public const string Pause = "Pause";

        //Which shot is currently in use
        public static int ShootingSelected = 2;
        //Whether it is shielded or not
        public static bool WithShield = false;

        public static int Damage = 0;
        //Level
        public static int CurrentLevel;

        public static Transform Player;

        public static int ShieldLife = 3;


        public static int EnemiesDead = 0;

        public static int Points;

        public static float Life;
        //Do a LookAt for 2D
        public static Quaternion FaceObject(Vector2 myPos, Vector2 targetPos, FacingDirection facing)
        {
            Vector2 direction = targetPos - myPos;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle -= (float)facing;
            return Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
