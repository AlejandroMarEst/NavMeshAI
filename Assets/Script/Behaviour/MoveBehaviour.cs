using UnityEngine;

public class MoveBehaviour : MonoBehaviour
{
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform _camera = null;
    [SerializeField] private float speed;
    private CharacterController _cC;
    private Vector3 _velocity;

    private void Awake()
    {
        _cC = GetComponent<CharacterController>();
    }
    private void FixedUpdate()
    {
        Quaternion rotation = new Quaternion(0, _camera.rotation.y, 0, _camera.rotation.w);
        transform.rotation = rotation;
        if (_cC.isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
        _velocity.y += gravity * Time.deltaTime;
        _cC.Move(_velocity * Time.deltaTime);
    }
    public void MoveFirstPerson(Vector3 direction)
    {
        Vector3 movement = direction.x * transform.right + direction.z * transform.forward;
        _cC.Move(movement * speed * Time.deltaTime);
    }
}