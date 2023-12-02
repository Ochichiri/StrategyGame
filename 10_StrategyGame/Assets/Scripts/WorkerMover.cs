using UnityEngine;

[RequireComponent(typeof(Worker))]
[RequireComponent(typeof(CharacterController))]
public class WorkerMover : MonoBehaviour
{
    private CharacterController _characterController;
    private Transform _tagetPosition;
    private Transform _townCenterPosition;
    private float _speed = 4f;

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
            Vector3 direction = (_tagetPosition.position - transform.position).normalized;
            _characterController.Move(direction * _speed * Time.fixedDeltaTime);
        }
    }
}