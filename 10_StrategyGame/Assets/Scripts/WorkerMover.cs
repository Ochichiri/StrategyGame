using UnityEngine;

[RequireComponent(typeof(Worker))]
[RequireComponent(typeof(CharacterController))]
public class WorkerMover : MonoBehaviour
{
    private CharacterController _characterController;
    private Transform _tagetPosition;
    private Transform _townCenterPosition;
    private float _movementSpeed = 4f;
    private float _rotationSpeed = 10f;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public void Init(Transform townCenterPosition)
    {
        _townCenterPosition = townCenterPosition;
    }

    public void SetTarget(Transform target)
    {
        _tagetPosition = target;
    }

    public void SetTargetTownCenter()
    {
        _tagetPosition = _townCenterPosition;
    }

    public void ResetTarget()
    {
        _tagetPosition = null;
    }

    private void FixedUpdate()
    {
        if (_tagetPosition != null)
        {
            Move();
            RotateToTarget();
        }
    }

    private void Move()
    {
        Vector3 direction = (_tagetPosition.position - transform.position).normalized;
        Vector3 moveDirection = new Vector3(direction.x, 0, direction.z);

        _characterController.Move(moveDirection * _movementSpeed * Time.fixedDeltaTime);
    }

    private void RotateToTarget()
    {
        Vector3 direction = (_tagetPosition.position - transform.position).normalized;
        float step = _rotationSpeed * Time.fixedDeltaTime;
        Vector3 directionForRotation = Vector3.RotateTowards(transform.forward, direction, step, 0.0f);
        float angelForRotation = Quaternion.LookRotation(directionForRotation).eulerAngles.y;

        transform.eulerAngles = new Vector3(0, angelForRotation, 0);
    }
}