using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Dictionary<string, List<Sprite>> animations;

    private int currentFrame = 0;
    private float frameDelay = 0.2f;
    private float lastFrameTime;

    private Vector3 direction = Vector3.right;
    private string currentDirection = "right";

    public List<Vector3> patrolPoints;
    private Vector3 chasePoint;
    private int currentPoint = 0;
    private float speed = 4f;

    private bool playerInVision = false;
    private bool playerReached = false;
    private float viewAngle = 90f;  
    private float viewDistance = 10f;
    private Transform player;

    private bool isSearching = false;
    private float searchTime = 1f; // Задержка между осмотрами
    private float searchTimer = 0f;
    private int currentSearchDirection = 0;
    private List<Vector3> searchDirections = new List<Vector3>
    {
        Vector3.up, Vector3.right, Vector3.down, Vector3.left
    };

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        spriteRenderer = GetComponent<SpriteRenderer>();
        animations = new Dictionary<string, List<Sprite>>
        {
            { "up", AnimationLoader.LoadAnimation("Animations/Zombie/Animation up") },
            { "down", AnimationLoader.LoadAnimation("Animations/Zombie/Animation down") },
            { "left", AnimationLoader.LoadAnimation("Animations/Zombie/Animation left") },
            { "right", AnimationLoader.LoadAnimation("Animations/Zombie/Animation right") }
        };

        SetPatrolPoints(new List<Vector3> { new Vector3(0, 0, 0), new Vector3(5, 0, 0), new Vector3(5, 5, 0), new Vector3(0, 5, 0) });
    }

    void FixedUpdate()
    {
        if (IsPlayerInFieldOfView())
        {
            LookForPlayer();
        }

        if (playerInVision)
        {
            Chase();
        }
        else if (playerReached && !isSearching)
        {
            // Если игрок был потерян, начни осмотр
            StartCoroutine(SearchForPlayer());
        }
        else if (!playerInVision && !playerReached)
        {
            Patrol();
        }

        UpdateAnimation();
    }

    bool IsPlayerInFieldOfView()
    {
        if (player == null) return false;

        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float angleBetween = Vector3.Angle(direction, directionToPlayer);
        if (angleBetween < viewAngle / 2 && Vector3.Distance(transform.position, player.position) < viewDistance)
        {
            return true;
        }

        return false;
    }

    void LookForPlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        int layerMask = 1 << LayerMask.NameToLayer("Default");

        RaycastHit2D hit2D = Physics2D.Raycast(transform.position + new Vector3(0, -1, 0), directionToPlayer, viewDistance, layerMask);

        //Debug.DrawRay(transform.position + new Vector3(0, -1, 0), directionToPlayer * viewDistance, Color.red);

        if (hit2D.collider != null && hit2D.collider.CompareTag("Player"))
        {

            playerInVision = true;
            chasePoint = player.position; // Зомби достиг точки, в которой он последний раз видел игрока
        }

    }

    public void Patrol()
    {
        if (patrolPoints.Count == 0)
            return;

        Vector3 targetPoint = patrolPoints[currentPoint];
        direction = (targetPoint - transform.position).normalized;


        if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
        {
            currentPoint = (currentPoint + 1) % patrolPoints.Count;
        }

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            currentDirection = direction.x > 0 ? "right" : "left";
        }
        else
        {
            currentDirection = direction.y > 0 ? "up" : "down";
        }
    }

    void Chase()
    {
        direction = (chasePoint - transform.position).normalized;
        
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            currentDirection = direction.x > 0 ? "right" : "left";
        }
        else
        {
            currentDirection = direction.y > 0 ? "up" : "down";
        }
        if ((chasePoint - transform.position).magnitude < 0.1f)
        {
            playerInVision = false;
            playerReached = true;
        }
    }
    IEnumerator SearchForPlayer()
    {
        isSearching = true;
        while (currentSearchDirection < searchDirections.Count)
        {
            // Повернись в новое направление и задержись
            direction = searchDirections[currentSearchDirection];
            currentDirection = GetDirectionName(direction);

            // Ждем 1 секунду
            yield return new WaitForSeconds(searchTime);

            // Проверь, видит ли враг игрока
            if (IsPlayerInFieldOfView())
            {
                playerInVision = true;
                isSearching = false;
                playerReached = false; // Чтобы начать преследование снова
                yield break; // Прерываем цикл осмотра
            }

            // Переход к следующему направлению
            currentSearchDirection++;
        }

        // Если игрок не был найден, возвращаемся к патрулированию
        isSearching = false;
        playerReached = false;
        currentSearchDirection = 0; // Сбрасываем осмотр для следующего раза
    }

    string GetDirectionName(Vector3 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            return direction.x > 0 ? "right" : "left";
        }
        else
        {
            return direction.y > 0 ? "up" : "down";
        }
    }

    void UpdateAnimation()
    {

        transform.Translate(direction * speed * Time.fixedDeltaTime);
        if (Time.time - lastFrameTime >= frameDelay)
        {
            currentFrame++;
            if (currentFrame >= 5)
            {
                currentFrame = 2;
            }

            spriteRenderer.sprite = animations[currentDirection][currentFrame];
            lastFrameTime = Time.time;
        }
    }
    public void SetPatrolPoints(List<Vector3> points)
    {
        patrolPoints = points;
    }
}

public class AnimationLoader
{
    public static List<Sprite> LoadAnimation(string folder)
    {
        List<Sprite> frames = new List<Sprite>();

        frames.AddRange(Resources.LoadAll<Sprite>(folder));

        return frames;
    }
}
