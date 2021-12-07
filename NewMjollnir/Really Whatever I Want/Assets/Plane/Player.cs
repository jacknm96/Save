using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    Rigidbody rb;
    Vector2 direction;
    Vector3 startPos;
    [SerializeField] float rotateSpeed;
    [SerializeField] Image boost;
    float stamina;
    float maxStamina = 100;
    public bool lost;
    bool isBoosting;
    public int score;
    public TMP_Text text;
    [SerializeField] float speed;
    [SerializeField] Obstacle prefab;
    [SerializeField] Fuel fuel;
    [SerializeField] GameObject loseScreen;
    [SerializeField] ObstacleSpawner spawner;

    [SerializeField] WallCollider topCollider;
    [SerializeField] WallCollider leftCollider;
    [SerializeField] WallCollider rightCollider;
    [SerializeField] WallCollider bottomCollider;

    [SerializeField] Looker looker;

    static public Player instance;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.localPosition;
        stamina = 100;
        text.text = score.ToString();
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation((looker.transform.position - transform.position).normalized);
        if (IsMoving())
        {
            StopAllCoroutines();
        }
        else
        {
            StartCoroutine(Reposition());
        }
        boost.fillAmount = stamina / maxStamina;
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
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        if (isBoosting)
        {
            stamina -= 1;
            if (stamina <= 0)
            {
                isBoosting = false;
            }
            rb.velocity *= 1.5f;
        }
    }

    public bool IsMoving()
    {
        return rb.velocity.magnitude > 0;
    }

    private void OnMove(InputValue value)
    {
        if (!lost)
        {
            direction = value.Get<Vector2>();
        }
    }

    private void OnJump(InputValue button)
    {
        isBoosting = button.Get<float>() > 0.5 && stamina > 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            ReFuel();
        } else
        {
            loseScreen.SetActive(true);
            lost = true;
            List<Obstacle> obstacles = ObjectPool.GetSpawned<Obstacle>(prefab, null, false);
            foreach (Obstacle obstacle in obstacles)
            {
                obstacle.StopMoving();
            }
            List<Fuel> fuelCells = ObjectPool.GetSpawned<Fuel>(fuel, null, false);
            foreach (Fuel fuelCell in fuelCells)
            {
                fuelCell.StopMoving();
            }
            spawner.StopSpawning();
        }
    }

    public void Init()
    {
        transform.localPosition = startPos;
        lost = false;
        topCollider.hitWall = false;
        bottomCollider.hitWall = false;
        leftCollider.hitWall = false;
        rightCollider.hitWall = false;
    }

    void ReFuel()
    {
        stamina += 20;
        if (stamina > 100) stamina = 100;
    }

    public void Restart()
    {
        ObjectPool.RecycleAll(prefab);
        ObjectPool.RecycleAll(fuel);
        lost = false;
        transform.localPosition = startPos;
        topCollider.hitWall = false;
        bottomCollider.hitWall = false;
        leftCollider.hitWall = false;
        rightCollider.hitWall = false;
        looker.Restart();
        stamina = 100;
    }

    public void AdjustScore()
    {
        score++;
        text.text = score.ToString();
    }

    IEnumerator Reposition()
    {
        float startTime = Time.time;
        while (true)
        {
            float delta = (Time.time - startTime) * Time.deltaTime * 0.1f;
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, looker.transform.position.x, delta), Mathf.Lerp(transform.position.y, looker.transform.position.y, delta), transform.position.z);
            yield return null;
        }
    }
}
