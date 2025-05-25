using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float moveSpeed = 20f;
    public float boostMultiplier = 3f;
    public float verticalSpeed = 7f;
    public float rotationSpeed = 70f;
    public float rollAngle = 30f;
    public float rollSpeed = 5f;
    public float normalVolume = 0.5f;
    public float boostedVolume = 1f;
    public float volumeChangeSpeed = 2f;

    private float currentRoll = 0f;
    private Rigidbody rb;
    public AudioSource engineSound;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;              // Desactiva gravedad si quieres que la nave flote
        rb.isKinematic = false;             // Asegúrate de que use física
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void Update()
    {
        HandleRotation();
        HandleVisualRoll();
        HandleSound();
    }

    void HandleMovement()
    {
        float speed = Input.GetKey(KeyCode.LeftShift) ? moveSpeed * boostMultiplier : moveSpeed;
        float moveForward = Input.GetAxis("Vertical");
        float moveSide = Input.GetAxis("Horizontal");
        float moveUp = 0f;

        if (Input.GetKey(KeyCode.Space)) moveUp += 1f;
        if (Input.GetKey(KeyCode.LeftControl)) moveUp -= 1f;

        Vector3 direction = transform.forward * moveForward + transform.right * moveSide + transform.up * moveUp;
        rb.velocity = direction * speed;  // Aplica movimiento directamente con física
    }

    void HandleRotation()
    {
        float pitch = 0f;
        float yaw = 0f;

        if (Input.GetKey(KeyCode.UpArrow)) pitch = 1f;
        if (Input.GetKey(KeyCode.DownArrow)) pitch = -1f;
        if (Input.GetKey(KeyCode.LeftArrow)) yaw = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) yaw = 1f;

        Vector3 rotation = new Vector3(pitch, yaw, 0f) * rotationSpeed * Time.deltaTime;
        transform.Rotate(rotation, Space.Self);
    }

    void HandleVisualRoll()
    {
        float input = Input.GetAxis("Horizontal");
        float targetRoll = -input * rollAngle;
        currentRoll = Mathf.Lerp(currentRoll, targetRoll, Time.deltaTime * rollSpeed);

        Quaternion targetRotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, currentRoll);
        transform.rotation = targetRotation;
    }

    void HandleSound()
    {
        float moveForward = Input.GetAxis("Vertical");

        if (Mathf.Abs(moveForward) > 0.1f)
        {
            if (!engineSound.isPlaying)
                engineSound.Play();

            float targetVolume = Input.GetKey(KeyCode.LeftShift) ? boostedVolume : normalVolume;
            engineSound.volume = Mathf.Lerp(engineSound.volume, targetVolume, Time.deltaTime * volumeChangeSpeed);
        }
        else
        {
            engineSound.volume = Mathf.Lerp(engineSound.volume, 0f, Time.deltaTime * volumeChangeSpeed);
            if (engineSound.volume < 0.05f && engineSound.isPlaying)
                engineSound.Stop();
        }
    }
}
