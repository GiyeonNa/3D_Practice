using UnityEngine;

public class Item : MonoBehaviour
{
    // Reference to the player
    private GameObject player;

    // Configurable attraction range and speed
    [SerializeField] private float attractionRange = 5f;
    [SerializeField] private float attractionSpeed = 2f;

    // Animation properties
    [Header("Rotation Settings")]
    [SerializeField] private bool isRotating = false;
    [SerializeField] private bool rotateX = false;
    [SerializeField] private bool rotateY = true;
    [SerializeField] private bool rotateZ = false;
    [SerializeField] private float rotationSpeed = 90f; // Degrees per second

    [Header("Floating Settings")]
    [SerializeField] private bool isFloating = false;
    [SerializeField] private bool useEasingForFloating = false;
    [SerializeField] private float floatHeight = 1f;
    [SerializeField] private float floatSpeed = 1f;
    private Vector3 initialPosition;
    private float floatTimer;

    [Header("Scaling Settings")]
    [SerializeField] private bool isScaling = false;
    [SerializeField] private bool useEasingForScaling = false;
    [SerializeField] private Vector3 startScale = Vector3.one;
    [SerializeField] private Vector3 endScale = Vector3.one * 1.5f;
    [SerializeField] private float scaleLerpSpeed = 1f;
    private float scaleTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Find the player object by tag
        player = GameObject.FindGameObjectWithTag("Player");

        // Initialize animation properties
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Attraction logic
        if (player != null && Vector3.Distance(transform.position, player.transform.position) <= attractionRange)
        {
            // Move the item towards the player
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, attractionSpeed * Time.deltaTime);
        }
        else
        {
            // Rotation logic
            if (isRotating)
            {
                Vector3 rotationVector = new Vector3(
                    rotateX ? 1 : 0,
                    rotateY ? 1 : 0,
                    rotateZ ? 1 : 0
                );
                transform.Rotate(rotationVector * rotationSpeed * Time.deltaTime);
            }

            // Floating logic
            if (isFloating)
            {
                floatTimer += Time.deltaTime * floatSpeed;
                float t = Mathf.PingPong(floatTimer, 1f);
                if (useEasingForFloating) t = EaseInOutQuad(t);

                // Apply floating offset to the current position instead of resetting to the initial position
                transform.position += new Vector3(0, t * floatHeight * Time.deltaTime, 0);
            }
        }

        // Scaling logic
        if (isScaling)
        {
            scaleTimer += Time.deltaTime * scaleLerpSpeed;
            float t = Mathf.PingPong(scaleTimer, 1f);
            if (useEasingForScaling) t = EaseInOutQuad(t);

            transform.localScale = Vector3.Lerp(startScale, endScale, t);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            // Destroy the item to simulate collection
            Destroy(gameObject);
        }
    }

    private float EaseInOutQuad(float t)
    {
        return t < 0.5f ? 2 * t * t : 1 - Mathf.Pow(-2 * t + 2, 2) / 2;
    }
}
