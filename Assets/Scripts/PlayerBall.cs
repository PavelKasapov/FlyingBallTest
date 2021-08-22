using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    private const float StartVerticalSpeed = 2f;
    private const float AdditionalSpeed = 1f;

    private float _curentSpeedModifier;
    private Dictionary<Difficulty, float> speedModifier;

    public event Action OnHitWall = delegate { };
    public int verticalDirection = -1;
    public float verticalSpeed;

    private void Start()
    {
        speedModifier = new Dictionary<Difficulty, float>
        {
            [Difficulty.Easy] = 1f,
            [Difficulty.Medium] = 2f,
            [Difficulty.Hard] = 3f,
        };
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            OnHitWall.Invoke();
        }
    }
    public void ResetBall()
    {
        transform.position = new Vector2(0, 7f);
    }

    public void StartMoving(Difficulty difficulty)
    {
        _curentSpeedModifier = speedModifier[difficulty];
        verticalSpeed = StartVerticalSpeed * _curentSpeedModifier;
        StartCoroutine(BallMoving());
        StartCoroutine(SpeedingUp());
    }

    public void StopMoving()
    {
        StopAllCoroutines();
    }

    IEnumerator BallMoving()
    {
        var ballTransform = transform;
        while (true)
        {
            ballTransform.Translate(new Vector2(0, verticalDirection * verticalSpeed * Time.fixedDeltaTime));
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator SpeedingUp()
    {
        while (true)
        {
            verticalSpeed += AdditionalSpeed * _curentSpeedModifier;
            yield return new WaitForSeconds(15f);
        }
    }
}
