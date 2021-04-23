using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    // Declare variables.
    public float speed = 4.0f;
    private float xClamp = 7.5f;
    private float yClamp = 3.5f;
    int direction = 1;

    // Call EnemyVerticalMovement().
    void Update()
    {
        EnemyVerticalMovement();
    }

    // Enemy movement method.
    private void EnemyVerticalMovement()
    {
        // https://forum.unity.com/threads/help-with-script-making-enemy-move-up-and-down.353603/
        // If the position hits the wall (clamp) then move the other way.
        if (transform.position.y >= yClamp)
            direction = -1;

        if (transform.position.y <= -yClamp)
            direction = 1;

        transform.Translate(0, speed * direction * Time.deltaTime, 0);
    }

}
