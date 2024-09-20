//using UnityEngine;

//public class EnemyController : MonoBehaviour
//{
//    public float moveSpeed = 2f; // Скорость движения
//    public Transform[] patrolPoints; // Точки патрулирования
//    public float detectionRange = 5f; // Радиус обнаружения игрока
//    public LayerMask playerLayer; // Слой игрока

//    public Sprite[] downSprites; // Спрайты для движения вниз
//    public Sprite[] upSprites; // Спрайты для движения вверх
//    public Sprite[] leftSprites; // Спрайты для движения влево
//    public Sprite[] rightSprites; // Спрайты для движения вправо
//    public float animationSpeed = 0.1f; // Скорость смены спрайтов

//    private SpriteRenderer spriteRenderer;
//    private Transform player;
//    private int currentPointIndex = 0;
//    private bool isPatrolling = true;
//    private float animationTimer;
//    private int currentSpriteIndex = 0;
//    private Sprite[] currentSpriteArray;

//    private void Start()
//    {
//        spriteRenderer = GetComponent<SpriteRenderer>();

//        // Поиск игрока в сцене
//        player = GameObject.FindWithTag("Player")?.transform;

//        if (player == null)
//        {
//            Debug.LogWarning("Player not found in the scene.");
//        }

//        // Инициализация текущих спрайтов (например, спрайты для движения вниз)
//        currentSpriteArray = downSprites;
//    }

//    private void Update()
//    {
//        if (player != null)
//        {
//            // Обнаружение игрока
//            if (Vector2.Distance(transform.position, player.position) < detectionRange)
//            {
//                isPatrolling = false;
//                MoveTowardsPlayer();
//            }
//            else
//            {
//                isPatrolling = true;
//                Patrol();
//            }

//            // Обновление анимации
//            UpdateAnimation();
//        }
//    }

//    private void Patrol()
//    {
//        if (patrolPoints.Length == 0) return;

//        Transform targetPoint = patrolPoints[currentPointIndex];
//        MoveTowards(targetPoint);

//        // Проверка достижения текущей точки патрулирования
//        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
//        {
//            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
//        }
//    }

//    private void MoveTowardsPlayer()
//    {
//        if (player == null) return;

//        MoveTowards(player);
//    }

//    private void MoveTowards(Transform target)
//    {
//        Vector2 direction = (target.position - transform.position).normalized;
//        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

//        // Определение спрайта в зависимости от направления движения
//        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
//        {
//            // Двигается горизонтально
//            if (direction.x > 0)
//                currentSpriteArray = rightSprites;
//            else
//                currentSpriteArray = leftSprites;
//        }
//        else
//        {
//            // Двигается вертикально
//            if (direction.y > 0)
//                currentSpriteArray = upSprites;
//            else
//                currentSpriteArray = downSprites;
//        }
//    }

//    private void UpdateAnimation()
//    {
//        animationTimer += Time.deltaTime;
//        if (animationTimer >= animationSpeed)
//        {
//            animationTimer = 0f;
//            currentSpriteIndex = (currentSpriteIndex + 1) % currentSpriteArray.Length;
//            spriteRenderer.sprite = currentSpriteArray[currentSpriteIndex];
//        }
//    }

//    private void OnDrawGizmos()
//    {
//        // Отображение радиуса обнаружения в редакторе
//        Gizmos.color = Color.red;
//        Gizmos.DrawWireSphere(transform.position, detectionRange);
//    }
//}
