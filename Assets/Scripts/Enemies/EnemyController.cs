//using UnityEngine;

//public class EnemyController : MonoBehaviour
//{
//    public float moveSpeed = 2f; // �������� ��������
//    public Transform[] patrolPoints; // ����� ��������������
//    public float detectionRange = 5f; // ������ ����������� ������
//    public LayerMask playerLayer; // ���� ������

//    public Sprite[] downSprites; // ������� ��� �������� ����
//    public Sprite[] upSprites; // ������� ��� �������� �����
//    public Sprite[] leftSprites; // ������� ��� �������� �����
//    public Sprite[] rightSprites; // ������� ��� �������� ������
//    public float animationSpeed = 0.1f; // �������� ����� ��������

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

//        // ����� ������ � �����
//        player = GameObject.FindWithTag("Player")?.transform;

//        if (player == null)
//        {
//            Debug.LogWarning("Player not found in the scene.");
//        }

//        // ������������� ������� �������� (��������, ������� ��� �������� ����)
//        currentSpriteArray = downSprites;
//    }

//    private void Update()
//    {
//        if (player != null)
//        {
//            // ����������� ������
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

//            // ���������� ��������
//            UpdateAnimation();
//        }
//    }

//    private void Patrol()
//    {
//        if (patrolPoints.Length == 0) return;

//        Transform targetPoint = patrolPoints[currentPointIndex];
//        MoveTowards(targetPoint);

//        // �������� ���������� ������� ����� ��������������
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

//        // ����������� ������� � ����������� �� ����������� ��������
//        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
//        {
//            // ��������� �������������
//            if (direction.x > 0)
//                currentSpriteArray = rightSprites;
//            else
//                currentSpriteArray = leftSprites;
//        }
//        else
//        {
//            // ��������� �����������
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
//        // ����������� ������� ����������� � ���������
//        Gizmos.color = Color.red;
//        Gizmos.DrawWireSphere(transform.position, detectionRange);
//    }
//}
