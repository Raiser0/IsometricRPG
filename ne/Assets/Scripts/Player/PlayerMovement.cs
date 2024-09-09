using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public Camera mainCamera; // Ýlk kamera
    public Camera newCamera;  // Geçiþ yapýlacak ikinci kamera

    private NavMeshAgent agent;
    private Animator animator;
    private Rigidbody rb;

    public bool EnemyDead;

    private bool isWalking = true;

    [SerializeField] private float moveCount;
    public Transform currentTarget;
    public float attackDistance = 1.5f;
    private float colliderTime;

    public CameraShake CameraShake;

    void Start()
    {
        // Baþlangýçta kameralarý doðru þekilde ayarla
        if (mainCamera == null) 
        {
            mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        }
        mainCamera.enabled = true;

        if (newCamera != null)
        {
            newCamera.enabled = false;
        }

        EnemyDead = false;
        isWalking = true;

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        agent.stoppingDistance = attackDistance;
    }

    void Update()
    {
        colliderTime += Time.deltaTime;

        // Hareket animasyonunu kontrol et
        if (moveCount > 0) 
        {
            animator.SetBool("isWalk", true);
        }
        else 
        {
            animator.SetBool("isWalk", false);
        }

        // Sol mouse týklamasýyla hedef belirleme
        if (Input.GetMouseButtonDown(0)) 
        {
            moveCount++;

            // Etkin kamera ile ýþýn atma iþlemi
            Ray ray = (mainCamera.enabled ? mainCamera : newCamera).ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) 
            {
                if (hit.collider.CompareTag("Enemy") && !EnemyDead) 
                {
                    currentTarget = hit.collider.transform;
                    agent.SetDestination(currentTarget.position);

                    isWalking = true;
                }
                else 
                {
                    currentTarget = null;
                    agent.SetDestination(hit.point);

                    isWalking = false;
                }
            }
        }

        // Hedefe ulaþýnca animasyonu ve sayacý sýfýrla
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending) 
        {
            isWalking = false;
            moveCount = 0;

            if (currentTarget != null && Vector3.Distance(transform.position, currentTarget.position) <= agent.stoppingDistance) 
            {
                StartCoroutine(DestroyTargetAfterDelay());
            }
        }
    }

    private IEnumerator DestroyTargetAfterDelay()
    {
        StartCoroutine(DeadAnim());

        yield return new WaitForSeconds(10);

        if (currentTarget != null) 
        {
            Destroy(currentTarget.gameObject);
            currentTarget = null;
        }
    }

    private IEnumerator DeadAnim()
    {
        yield return new WaitForSeconds(1);
        EnemyDead = true;
    }

    private IEnumerator LAN()
    {
        yield return new WaitForSeconds(1);
        CameraShake.Shake(0.5f, 0.2f);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !EnemyDead) 
        {
            animator.SetTrigger("isAttack");
            StartCoroutine(LAN());
        }

        // Kamera geçiþi yaparken doðru referanslarý güncelle
        if (other.CompareTag("Door") && colliderTime > 1) 
        {
            transform.position = new Vector3(-6f, 0f, 4f);
            isWalking = false;
            colliderTime = 0;

            if (mainCamera.enabled == false) 
            {
                mainCamera.enabled = true;
                newCamera.enabled = false;
            }
            else 
            {
                mainCamera.enabled = false;
                newCamera.enabled = true;
            }
        }
    }
}