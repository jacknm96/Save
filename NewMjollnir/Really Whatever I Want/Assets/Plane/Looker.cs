using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Looker : MonoBehaviour
{
    Vector2 direction;
    Rigidbody rb;
    [SerializeField] float speed;

    [SerializeField] Player player;

    [SerializeField] WallCollider topCollider;
    [SerializeField] WallCollider leftCollider;
    [SerializeField] WallCollider rightCollider;
    [SerializeField] WallCollider bottomCollider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
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
        if (player.IsMoving())
        {
            StopAllCoroutines();
        } else
        {
            StartCoroutine(Reposition());
        }
        rb.velocity = direction * speed;
    }

    private void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
    }

    IEnumerator Reposition()
    {
        float startTime = Time.time;
        while (true)
        {
            float delta = (Time.time - startTime) * Time.deltaTime * 0.1f;
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, player.transform.position.x, delta), Mathf.Lerp(transform.position.y, player.transform.position.y, delta), transform.position.z);
            yield return null;
        }
    }
}
