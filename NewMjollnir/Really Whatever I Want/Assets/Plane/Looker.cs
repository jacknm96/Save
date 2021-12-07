using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Looker : MonoBehaviour
{
    Vector2 direction;
    Rigidbody rb;
    Vector3 startPos;
    [SerializeField] float speed;

    [SerializeField] Player player;

    Coroutine xReposition;
    Coroutine yReposition;

    bool xDrift;
    bool yDrift;

    [SerializeField] WallCollider topCollider;
    [SerializeField] WallCollider leftCollider;
    [SerializeField] WallCollider rightCollider;
    [SerializeField] WallCollider bottomCollider;

    // Start is called before the first frame update
    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.lost)
        {
            if (topCollider.hitWall && direction.y > 0)
            {
                direction.y = 0;
            }
            if (bottomCollider.hitWall && direction.y < 0)
            {
                direction.y = 0;
            }
            if (leftCollider.hitWall && direction.x < 0)
            {
                direction.x = 0;
            }
            if (rightCollider.hitWall && direction.x > 0)
            {
                direction.x = 0;
            }
            /*if (player.IsMoving())
            {
                StopAllCoroutines();
            } else
            {
                StartCoroutine(RepositionX());
                StartCoroutine(RepositionY());
            }
            if (direction.y == 0 && !yDrift)
            {
                yReposition = StartCoroutine(RepositionY());
            } else
            {
                StopCoroutine(yReposition);
                yDrift = false;
            }
            if (direction.x == 0 && !xDrift)
            {
                xReposition = StartCoroutine(RepositionX());
            } else
            {
                StopCoroutine(xReposition);
                xDrift = false;
            }*/
            rb.velocity = direction * speed;
        }
    }

    private void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
    }

    public void Restart()
    {
        transform.localPosition = startPos;
        topCollider.hitWall = false;
        bottomCollider.hitWall = false;
        leftCollider.hitWall = false;
        rightCollider.hitWall = false;
    }

    IEnumerator RepositionX()
    {
        xDrift = true;
        float startTime = Time.time;
        while (true)
        {
            float delta = (Time.time - startTime) * Time.deltaTime * 0.1f;
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, player.transform.position.x, delta), transform.position.y, transform.position.z);
            yield return null;
        }
    }

    IEnumerator RepositionY()
    {
        yDrift = true;
        float startTime = Time.time;
        while (true)
        {
            float delta = (Time.time - startTime) * Time.deltaTime * 0.1f;
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, player.transform.position.y, delta), transform.position.z);
            yield return null;
        }
    }
}
