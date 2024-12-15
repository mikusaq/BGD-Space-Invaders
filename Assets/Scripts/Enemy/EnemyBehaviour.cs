using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public AudioClip destructionSFX;

    // physical simulation hits. For Unity to call this function, at least one of the colliding objects
    // needs to have their RigidBody component set to "Dynamic" for Body Type
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("I Collided!");
    }

    // Unity calls this function if the Collider on the game object has "Is Trigger" checked.
	// Then it doesn't physically react to hits but still detects them
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("I was triggered!");

		// Check the other colliding object's tag to know if it's
		// indeed a player projectile
        if (collision.tag == "Laser")
        {
            // Destroy the alien game object
            Destroy(gameObject);
			
            // Destroy the projectile game object
            Destroy(collision.gameObject);
			
			// Play an audio clip in the scene and not attached to the alien
			// so the sound keeps playing even after it's destroyed
            AudioSource.PlayClipAtPoint(destructionSFX, transform.position);
        }
    }
}
