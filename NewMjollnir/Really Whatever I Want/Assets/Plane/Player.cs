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
    Vector3 cameraPosition;

    float stamina;
    float maxStamina = 100;
    public bool lost;
    public bool isBoosting;
    public int score;
    public TMP_Text text;
    static public Player instance;

    [SerializeField] float speed;
    [SerializeField] Image boost;

    [SerializeField] Obstacle prefab;
    [SerializeField] Fuel fuel;
    [SerializeField] ObstacleSpawner spawner;

    [SerializeField] GameObject loseScreen;
    
    [SerializeField] WallCollider topCollider;
    [SerializeField] WallCollider leftCollider;
    [SerializeField] WallCollider rightCollider;
    [SerializeField] WallCollider bottomCollider;

    [SerializeField] Looker looker;

    [SerializeField] ParticleSystem leftEngine;
    [SerializeField] ParticleSystem leftEngineAlpha;
    [SerializeField] ParticleSystem rightEngine;
    [SerializeField] ParticleSystem rightEngineAlpha;

    [SerializeField] Camera camera;

    [SerializeField] AudioSource loseSound;
    [SerializeField] AudioSource engineSound;

    

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.localPosition;
        stamina = 100;
        text.text = score.ToString();
        instance = this;
        cameraPosition = camera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation((looker.transform.position - transform.position).normalized); // look at looker
    }

    private void FixedUpdate()
    {
        DetectWalls();
        Move();
        Drift();
        if (isBoosting)
        {
            Boost();
        }
    }

    void DetectWalls()
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
    }

    private void Move()
    {
        if (direction.magnitude > 0.1f)
        {
            rb.velocity = direction * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    void Drift()
    {
        if (direction.y == 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, looker.transform.position.y - transform.position.y, 0);
        }
        if (direction.x == 0)
        {
            rb.velocity = new Vector3(looker.transform.position.x - transform.position.x, rb.velocity.y, 0);
        }
    }

    void Boost()
    {
        stamina -= 0.3f;
        boost.fillAmount = stamina / maxStamina;
        if (stamina <= 0)
        {
            isBoosting = false;
            EndBoost();
        }
        rb.velocity *= 2f;
    }

    public bool IsMoving()
    {
        return rb.velocity.magnitude > 0;
    }
    
    //starts game
    public void Init()
    {
        transform.localPosition = startPos;
        lost = false;
        topCollider.hitWall = false;
        bottomCollider.hitWall = false;
        leftCollider.hitWall = false;
        rightCollider.hitWall = false;
        engineSound.Play();
    }
    
    //restart level
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
        boost.fillAmount = stamina / maxStamina;
        score = 0;
        text.text = score.ToString();
        engineSound.Play();
    }

    public void AdjustScore()
    {
        score++;
        text.text = score.ToString();
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
            StartBoost();
        } else
        {
            EndBoost();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Refuel")) // fuel powerup
        {
            ReFuel();
        } else // die
        {
            if (!loseScreen.activeSelf) loseSound.Play();
            loseScreen.SetActive(true);
            lost = true;

            //pause all obstacles/fuel cells
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
            engineSound.Stop();
        }
    }

    //increases particle effect and moves camera away to create zoom effect
    private void StartBoost()
    {
        leftEngine.startLifetime = 4;
        leftEngine.startSpeed = 5;
        rightEngine.startLifetime = 4;
        rightEngine.startSpeed = 5;
        leftEngineAlpha.startLifetime = 3;
        leftEngineAlpha.startSpeed = 3;
        rightEngineAlpha.startLifetime = 3;
        rightEngineAlpha.startSpeed = 3;
        StopAllCoroutines();
        StartCoroutine(CameraBoostZoomOut());
    }

    //decreases particle effect and moves camera forward to create zoom effect
    private void EndBoost()
    {
        leftEngine.startLifetime = 3;
        leftEngine.startSpeed = 2;
        rightEngine.startLifetime = 3;
        rightEngine.startSpeed = 2;
        leftEngineAlpha.startLifetime = 2;
        leftEngineAlpha.startSpeed = 1.5f;
        rightEngineAlpha.startLifetime = 2;
        rightEngineAlpha.startSpeed = 1.5f;
        StopAllCoroutines();
        StartCoroutine(CameraBoostZoomIn());
    }

    //gain stamina
    void ReFuel()
    {
        stamina += 30;
        if (stamina > maxStamina) stamina = maxStamina;
        boost.fillAmount = stamina / maxStamina;
    }

    IEnumerator CameraBoostZoomOut()
    {
        float startTime = Time.time;
        while(true)
        {
            camera.transform.position = new Vector3(cameraPosition.x, cameraPosition.y, Mathf.Lerp(camera.transform.position.z, cameraPosition.z - 2, (Time.time - startTime) * Time.deltaTime * 5));
            yield return null;
        }
    }

    IEnumerator CameraBoostZoomIn()
    {
        float startTime = Time.time;
        while (true)
        {
            camera.transform.position = new Vector3(cameraPosition.x, cameraPosition.y, Mathf.Lerp(camera.transform.position.z, cameraPosition.z, (Time.time - startTime) * Time.deltaTime * 2));
            yield return null;
        }
    }
}
