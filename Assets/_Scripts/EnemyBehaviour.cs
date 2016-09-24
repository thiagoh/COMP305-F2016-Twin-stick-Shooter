using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

    // How many times should I be hit before I die
    public int health = 2;

    // When the enemy dies, we play an explosion
    public Transform explosion;

    // What sound to play when hit
    public AudioClip hitSound;

    // create an AudioSource variable
    private AudioSource audio;

    private GameController controller;

    // Use this for initialization
    void Start() {
        audio = GetComponent<AudioSource>();
        controller =
            GameObject
                .FindGameObjectWithTag("GameController")
                .GetComponent<GameController>();
    }

    void OnCollisionEnter2D(Collision2D theCollision) {
        // Uncomment this line to check for collision
        //Debug.Log("Hit"+ theCollision.gameObject.name);
        // this line looks for "laser" in the names of
        // anything collided.
        if (theCollision.gameObject.name.Contains("Laser")) {
            LaserBehaviour laser =
                theCollision.gameObject.GetComponent
                ("LaserBehaviour") as LaserBehaviour;
            health -= laser.damage;
            Destroy(theCollision.gameObject);

            // Plays a sound from this object's AudioSource
            audio.PlayOneShot(hitSound, 1.0f);
        }

        if (theCollision.gameObject.name.Contains("Player Ship")) {
            controller.DecreaseLives(1);
        }

        if (health <= 0) {
            // Check if explosion was set
            if (explosion) {
                GameObject exploder = ((Transform)Instantiate(explosion, this.
                    transform.position, this.transform.rotation)).gameObject;
                Destroy(exploder, 2.0f);
            }

            controller.KilledEnemy();
            controller.IncreaseScore(10);


            Destroy(this.gameObject);
        }
    }
}

