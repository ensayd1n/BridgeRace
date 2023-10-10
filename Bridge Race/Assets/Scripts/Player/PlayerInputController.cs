using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    public Joystick Joystick;
    private Rigidbody _rigidbody;
    public GameObject CurrentStep;
    
    private float _xrot;
    public float MoveSpeed;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }


    private void Move()
    {
        _rigidbody.velocity = new Vector3(Joystick.Horizontal * MoveSpeed, 0,
            Joystick.Vertical * MoveSpeed);
    }
    private void Rotate()
    {
        float _rotationAngle = Mathf.Atan2(-Joystick.Horizontal, -Joystick.Vertical) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, _rotationAngle, 0f);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            CurrentStep = collision.gameObject;
        }
    }

    private void Update()
    {
        Move();
        Rotate();
    }
}
