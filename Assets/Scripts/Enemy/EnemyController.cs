using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float minPosX;
    public float maxPosX;

    public float moveDistance = 1f;

    bool isMovingRight = true;
    bool moveDown = false;

    public float timeStep = 1f;
    public float countdown;
	
	// I added a switch to try both methods
	public bool isUsingCountdown = true;

    public GameObject gameController;

    // Use this for initialization
    void Start()
    {
		if (isUsingCountdown)
		{
			countdown = timeStep;
		}
		else
		{	
			// Invoke repeating will be called once after timeStep (2nd parameter) amount,
			// and then repeatedly every timeStep (3rd parameter) amount
			//InvokeRepeating("Move", timeStep, timeStep);
			InvokeRepeating("Move", timeStep, timeStep);

            // Same for moving down
            InvokeRepeating("MoveDown", timeStep, timeStep);
        }
    }

    // Update is called once per frame
    void Update()
    {
		if (isUsingCountdown)
		{
			countdown -= Time.deltaTime;

			if (countdown <= 0)
			{
				Move();
                countdown = timeStep;
			}
		}
        
        if (transform.position.y < -3f)
        {
            int enemyCount = gameObject.GetComponentsInChildren<Transform>().Length - 1;
            if (enemyCount > 0)
            {
                gameController.GetComponent<GameController>().GameOver();
            }
        }
    }

    void Move()
    {
        if (moveDown)
        {
            // Moving down
            Vector3 currentPos = transform.position;
            Vector3 newPos = currentPos - new Vector3(0f, moveDistance);
            transform.position = newPos;

            moveDown = false;
        }
        else if (isMovingRight)
        {
            // Moving right
            Vector3 currentPos = transform.position;
            Vector3 newPos = currentPos + new Vector3(moveDistance, 0f);
            transform.position = newPos;

            // If aliens group reached the right-most edge, flip their direction
            if (transform.position.x >= maxPosX)
            {
                moveDown = true;
                isMovingRight = false;
            }
        }
        else
        {
            // Moving left
            Vector3 currentPos = transform.position;
            Vector3 newPos = currentPos - new Vector3(moveDistance, 0f);
            transform.position = newPos;

            // If aliens group reached the left-most edge, flip their direction
            if (transform.position.x <= minPosX)
            {
                moveDown = true;
                isMovingRight = true;
            }
        }
    }
}