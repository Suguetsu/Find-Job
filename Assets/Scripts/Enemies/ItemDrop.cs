using Gaminho;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*este scipt vai anexado no objeto que é dropado, ele executa a função quando o jogador encosta nele*/
public class ItemDrop : MonoBehaviour {
    public Statics.TYPE_DROP DropType;
    private void OnTriggerEnter2D(Collider2D objeto)
    {
        if (objeto.gameObject.tag == "Player")
        {
            switch (DropType)
            {
                case Statics.TYPE_DROP.SHIELD:
                    Statics.WithShield = true;
                    break;
                case Statics.TYPE_DROP.SHOT1:
                    Statics.ShootingSelected = 0;
                    break;
                case Statics.TYPE_DROP.SHOT2:
                    Statics.ShootingSelected = 1;
                    break;
                case Statics.TYPE_DROP.SHOT3:
                    Statics.ShootingSelected = 2;
                    break;
                case Statics.TYPE_DROP.LIFEFULL:
                    objeto.gameObject.GetComponent<Life>().life = 10;
                    break;
            }
            Destroy(gameObject);
        }
        
    }
}
