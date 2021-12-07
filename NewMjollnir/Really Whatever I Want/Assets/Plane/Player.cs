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
    public bool isBoosting;
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

    [SerializeField] ParticleSystem leftEngine;
    [SerializeField] ParticleSystem leftEngineAlpha;
    [SerializeField] ParticleSystem rightEngine;
    [SerializeField] ParticleSystem rightEngineAlpha;

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
        if (direction.y == 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, looker.transform.position.y - transform.position.y, 0);
        }
        if (direction.x == 0)
        {
            rb.velocity = new Vector3(looker.transform.position.x - transform.position.x, rb.velocity.y, 0);
        }
        if (isBoosting)
        {
            stamina -= 1;
            if (stamina <= 0)
            {
                isBoosting = false;
                leftEngine.startLifetime = 3;
                leftEngine.startSpeed = 2;
                rightEngine.startLifetime = 3;
                rightEngine.startSpeed = 2;
                leftEngineAlpha.startLifetime = 2;
                leftEngineAlpha.startSpeed = 1.5f;
                rightEngineAlpha.startLifetime = 2;
                rightEngineAlpha.startSpeed = 1.5f;
            }
            rb.velocity *= 2f;
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
        if (isBoosting)
        {
            leftEngine.startLifetime = 4;
            leftEngine.startSpeed = 5;
            rightEngine.startLifetime = 4;
            rightEngine.startSpeed = 5;
            leftEngineAlpha.startLifetime = 3;
            leftEngineAlpha.startSpeed = 3;
            rightEngineAlpha.startLifetime = 3;
            rightEngineAlpha.startSpeed = 3;
        } else
        {
            leftEngine.startLifetime = 3;
            leftEngine.startSpeed = 2;
            rightEngine.startLifetime = 3;
            rightEngine.startSpeed = 2;
            leftEngineAlpha.startLifetime = 2;
            leftEngineAlpha.startSpeed = 1.5f;
            rightEngineAlpha.startLifetime = 2;
            rightEngineAlpha.startSpeed = 1.5f;
        }
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
}
