using UnityEngine;
namespace Gaminho
{
    [System.Serializable]
    public class Level
    {
        public Sprite Background;//Change  background
        public int EnemyQty;//Number of Enemies that will be on the screen at the same time
        public int QtyForBoss;//How many have to kill to call the Godfather
        public GameObject Boss;//GO Boss
        public float TempSpaw = 1f;//Waiting time to create more Enemies
        public GameObject[] Enemies;//List of Enemies that will be created in the stage
        public AudioClip AudioLvl;//Background audio of the Lvl

    }
}