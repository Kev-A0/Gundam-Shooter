using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// We extend from MonoBehaviour which allows us to drag and drop scripts or behaviors onto game objects to control them in Unity
/// 
/// Name: Saksham Bhardwaj
/// StudentNo: A01185352
/// </summary>
public class Powerup : MonoBehaviour {
    // SerializeField allows private variable to appear in Unity so it can be changed while testing
    // This variable controls how fast the game object will move
    [SerializeField]
  private float _speed = 3.0f;
  [SerializeField]
  // 0 = Triple Shot Powerup
  // 1 = Health Powerup
  // 2 = Shield Powerup
  private float powerupID;


    //Updates once a frame  
    void Update () {
        transform.Translate (Vector3.down * _speed * Time.deltaTime);//speed is 3m/s to move down

        //finishing the powerup exit screen                                                             
        if (transform.position.y < -4.5f) {
          Destroy (this.gameObject);
        }

  }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject player = other.gameObject;

            switch (powerupID)
            {
/*                case 0: //case 0 signifies Weapon powerup(Triple shot)
                    if (player.getIsTripleShotActive() == true)
                    {
                        player.setIsAnotherTripleShot();
                    }
                    player.TripleShotActive();
                    break;*/

                case 1://case 1 signifies Health powerup(Health Boost)
                    if (player.GetComponent<GundamHealth>().health < player.GetComponent<GundamHealth>().numOfLives)
                    {
                        player.GetComponent<GundamHealth>().health++;
                    }
                    break;

/*                case 2://case 2 signifies Shield powerup(Shield)
                    player.ShieldsActive();
                    break;*/
            }

            Destroy(this.gameObject);//Destroys this gameobject when it exits the screen
        }
        
    }

}
