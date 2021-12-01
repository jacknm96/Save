using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    Rigidbody rb;
    Vector2 direction;
    Vector3 startPos;
    Quaternion baseRotation;
    Coroutine rotateSide;
    Coroutine rotateFront;
    [SerializeField] float rotateSpeed;
    bool lost;
    [SerializeField] InputActionReference move;
    [SerializeField] float speed;
    [SerializeField] Obstacle prefab;
    [SerializeField] GameObject loseScreen;
    [SerializeField] ObstacleSpawner spawner;

    [SerializeField] WallCollider topCollider;
    [SerializeField] WallCollider leftCollider;
    [SerializeField] WallCollider rightCollider;
    [SerializeField] WallCollider bottomCollider;

    [SerializeField] GameObject looker;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        baseRotation = transform.rotation;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
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
        if (direction.magnitude > 0.1f)
        {
            rb.velocity = direction * speed;
            //rotateSide = StartCoroutine(RotateSide());
        }
        else
        {
            rb.velocity = Vector2.zero;
            //transform.rotation = baseRotation;
            //rotateFront = StartCoroutine(RotateForward());
        }
    }

    private void OnMove(InputValue value)
    {
        if (!lost)
        {
            transform.rotation = baseRotation;
            direction = value.Get<Vector2>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        loseScreen.SetActive(true);
        lost = true;
        List<Obstacle> obstacles = ObjectPool.GetSpawned<Obstacle>(prefab, null, false);
        foreach (Obstacle obstacle in obstacles)
        {
            obstacle.StopMoving();
        }
        spawner.StopSpawning();
    }

    public void Init()
    {
        transform.position = startPos;
        lost = false;
        topCollider.hitWall = false;
        bottomCollider.hitWall = false;
        leftCollider.hitWall = false;
        rightCollider.hitWall = false;
    }

    public void Restart()
    {
        ObjectPool.RecycleAll(prefab);
        lost = false;
        transform.position = startPos;
        topCollider.hitWall = false;
        bottomCollider.hitWall = false;
        leftCollider.hitWall = false;
        rightCollider.hitWall = false;
    }

    /*IEnumerator RotateSide()
    {
        float startTime = Time.time;
        while (Time.time - startTime < 1)
        {
            transform.Rotate(new Vector3(Mathf.Lerp(baseRotation.x, -direction.y * 10, (Time.time - startTime) / rotateSpeed), Mathf.Lerp(baseRotation.y, direction.x * 10, (Time.time - startTime) / rotateSpeed), 0));
            yield return null;
        }
    }

    IEnumerator RotateForward()
    {
        float startTime = Time.time;
        while (Time.time - startTime < 1)
        {
            transform.Rotate(new Vector3(Mathf.Lerp(-direction.y * 10, baseRotation.x, (Time.time - startTime) / rotateSpeed), Mathf.Lerp(direction.x * 10, baseRotation.y, (Time.time - startTime) / rotateSpeed), 0));
            yield return null;
        }
    }*/
}
