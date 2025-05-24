using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int powerupID; //0 = triple shot, 1 = speed boost, 2 = shield
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with: "+other.name);
        if(other.tag == "Player")
        {
            //acces the player
            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                //turn the triple shot bool to true
                if(powerupID == 0)
                {
                    player.TripleShotPowerOn();
                }
                else if ( powerupID == 1)
                {
                    //enable speed boost here
                    player.SpeedBoostOn();

                }
                else if (powerupID == 2)
                {
                    //enable shields
                    player.enableShield();

                }
            }

            
            //destroy out selves
            Destroy(this.gameObject);
        }
        


    }
}
