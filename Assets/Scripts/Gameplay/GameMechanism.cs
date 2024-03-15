using UnityEngine;
using UnityEngine.Pool;

public class GameMechanism : MonoBehaviour
{
    public static GameMechanism Instance;

    [Header("Setup")]
    public Transform leftAnchor; // Left anchor of the slingshot
    public Transform rightAnchor; // Right anchor of the slingshot
    public Rigidbody projectile; // The projectile to be launched
    public LineRenderer band; // Visual representation of the slingshot band
    public GameObject shotWood; // Reference to the shot wood object
    public GameObject prefab; // Prefab to be used with the object pool

    [Header("Physics Settings")]
    [SerializeField] private float springConstant = 10f; // Spring force constant
    [SerializeField] private float dragDistanceLimit = 4f; // Max stretch distance of the slingshot band
    [SerializeField] private float upwardFactor = 0.72f; // Upward adjustment factor for projectile launch

    private Vector3 startPosition; // Starting position of the projectile
    private bool isDragging = false; // Is the projectile being dragged
    //public int levelNum;
    public int score;

    // Object pooling
    [SerializeField] private ObjectPool<GameObject> pool;

    private void Awake()
    {
        Instance = this;

        // Initialize the object pool
        pool = new ObjectPool<GameObject>(
            createFunc: CreatePooledItem,
            actionOnGet: OnTakeFromPool,
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 20);
    }

    private void Start()
    {
        // Set the starting position of the projectile and update the band's position
        startPosition = projectile.position;
        UpdateBand();
    }

    private void Update()
    {
        HandleDragging();
        HandleLaunch();
    }

    private void HandleDragging()
    {
        // Begin dragging
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            projectile.isKinematic = true;
        }

        if (isDragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 dragPosition = hit.point;
                Vector3 direction = (startPosition - dragPosition).normalized;
                float dragDistance = Mathf.Min(Vector3.Distance(dragPosition, startPosition), dragDistanceLimit);
                dragPosition = startPosition - direction * dragDistance;

                // Constrain dragPosition within defined limits behind the shotWood
                float minX = shotWood.transform.position.x - 5.0f;
                float maxX = shotWood.transform.position.x + 5.0f;
                float minY = shotWood.transform.position.y - 5.0f;
                float maxY = shotWood.transform.position.y + 5.0f;
                float minZ = shotWood.transform.position.z - 5.0f; // Ensure dragPosition is behind shotWood
                float maxZ = shotWood.transform.position.z + 5.0f; // Ensure dragPosition doesn't go too far

                // Apply clamping
                dragPosition.x = Mathf.Clamp(dragPosition.x, minX, maxX);
                dragPosition.y = Mathf.Clamp(dragPosition.y, minY, maxY);
                dragPosition.z = Mathf.Clamp(dragPosition.z, minZ, maxZ); // Adjusted to use z for depth

                // Update projectile position to be behind the shotWood within the defined limits
                projectile.position = dragPosition;
                UpdateBand();
            }
        }
    }

    private void HandleLaunch()
    {
        // Launch the projectile
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            projectile.isKinematic = false;

            Vector3 forceDirection = (startPosition - projectile.position).normalized;
            Vector3 adjustedForceDirection = (forceDirection + Vector3.up * upwardFactor).normalized;
            float forceMagnitude = Vector3.Distance(startPosition, projectile.position) * springConstant;

            projectile.AddForce(adjustedForceDirection * forceMagnitude, ForceMode.Impulse);
            projectile.useGravity = true;
        }
    }

    private void UpdateBand()
    {
        // Updates the position of the slingshot band to follow the projectile and anchors
        band.SetPosition(0, leftAnchor.position);
        band.SetPosition(1, projectile.position);
        band.SetPosition(2, rightAnchor.position);
    }

    private GameObject CreatePooledItem()
    {
        // Object pool item creation logic
        return Instantiate(prefab);
    }

    private void OnTakeFromPool(GameObject obj)
    {
        // Action to perform when an object is taken from the pool
        obj.SetActive(true);
    }
}
