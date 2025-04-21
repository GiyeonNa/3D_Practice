using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public PlayerStatsSO Stats;
    [SerializeField]
    private float currentStamina;
    [SerializeField]
    private float moveSpeed;

    private const float GRAVITY = -9.8f;
    private float _yVelocity = 0f;
    private CharacterController _characterController;

    private bool isDoubleJump = false;
    private bool isClimbing = false;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        currentStamina = Stats.MaxStamina; 
    } 

    private void Update()
    {
        HandleMovement();
        HandleClimbing();
        HandleStaminaAndSpeed();
        Jump();
    }

    #region Move
    private void HandleMovement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(h, 0, v).normalized;
        dir = Camera.main.transform.TransformDirection(dir);

        if (!isClimbing)
            _yVelocity += GRAVITY * Time.deltaTime;

        dir.y = _yVelocity;
        _characterController.Move(dir * moveSpeed * Time.deltaTime);

        if (_characterController.isGrounded)
            isDoubleJump = false;

        if (Input.GetKeyDown(KeyCode.E) && currentStamina > 0)
            Roll();
    }
    #endregion

    #region Climb
    private void HandleClimbing()
    {
        if (Input.GetKey(KeyCode.W) && IsNearWall() && currentStamina > 0)
            StartClimbing();

        if (isClimbing)
            Climb();

        if (isClimbing && (currentStamina <= 0 || !IsNearWall()))
            StopClimbing();
    }

    private void StartClimbing()
    {
        isClimbing = true;
        _yVelocity = 0;
    }

    private void Climb()
    {
        if(currentStamina <= 0)
        {
            StopClimbing();
            return;
        }

        Vector3 climbDirection = Vector3.up;
        _characterController.Move(climbDirection * moveSpeed * Time.deltaTime);

        AdjustStamina(-Time.deltaTime * Stats.ClimbStaminaCost);
        PlayerUI.Instance.SetStamina(currentStamina, Stats.MaxStamina);
    }

    private void StopClimbing()
    {
        isClimbing = false;
        _yVelocity = 0;
    }
    #endregion

    #region Dash
    private void HandleStaminaAndSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0)
        {
            moveSpeed = Stats.DashSpeed;
            AdjustStamina(-Time.deltaTime * Stats.DashStaminaCost);
        }
        else
        {
            moveSpeed = Stats.MoveSpeed; 
            AdjustStamina(Time.deltaTime * Stats.StaminaRecoveryRate);
        }
        PlayerUI.Instance.SetStamina(currentStamina, Stats.MaxStamina);
    }
    #endregion

    #region Jump
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && _characterController.isGrounded)
        {
            _yVelocity = Stats.JumpPower;
        }
        else if (Input.GetButtonDown("Jump") && !isDoubleJump)
        {
            _yVelocity = Stats.JumpPower;
            isDoubleJump = true;
        }
    }
    #endregion

    #region Roll
    private void Roll()
    {
        Vector3 rollDir = transform.forward;
        rollDir.y = 0;
        rollDir.Normalize();
        AdjustStamina(-Stats.RollStaminaCost); 
        PlayerUI.Instance.SetStamina(currentStamina, Stats.MaxStamina);
        _characterController.Move(rollDir * Stats.MoveSpeed * Stats.RollSpeed * Time.deltaTime);
    }
    #endregion

    #region Check
    private bool IsNearWall()
    {
        return _characterController.collisionFlags == CollisionFlags.Sides;
    }
    #endregion

    private void AdjustStamina(float amount)
    {
        currentStamina += amount;

        if (currentStamina > Stats.MaxStamina)
        {
            currentStamina = Stats.MaxStamina;
        }
        else if (currentStamina < 0.01f)
        {
            currentStamina = 0;
        }
    }
}
