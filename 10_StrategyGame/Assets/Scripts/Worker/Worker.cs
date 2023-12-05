using UnityEngine;

[RequireComponent(typeof(WorkerMover))]
[RequireComponent(typeof(WorkerCollision))]
public class Worker : MonoBehaviour
{
    [SerializeField] private TownCenter _townCenter;
    [SerializeField] private Transform _resourceContainer;

    private WorkerMover _workerMover;
    private WorkerCollision _workerCollision;

    public Resource Resource { get; private set; } = null;

    public bool HasActivity { get; private set; }

    public TownCenter TownCenter => _townCenter;

    private void Awake()
    {
        _workerMover = GetComponent<WorkerMover>();
        _workerCollision = GetComponent<WorkerCollision>();

        if (_townCenter != null)
        {
            _workerMover.Init(_townCenter.transform);
        }
    }

    public void Init(TownCenter townCenter)
    {
        _townCenter = townCenter;
        _workerMover.Init(_townCenter.transform);
    }

    public void MoveTo(Resource resource)
    {
        HasActivity = true;
        _workerCollision.SetTargetResource(resource);
        _workerMover.SetTarget(resource.transform);
    }

    public void MoveTo(Transform targetPosition)
    {
        _workerMover.SetTarget(targetPosition);
    }

    public void GetResource(Resource resource)
    {
        Resource = resource;
        resource.transform.parent = _resourceContainer;
        resource.transform.localPosition = new Vector3(0, 0, 0);
        _workerMover.SetTargetTownCenter();
    }

    public void GiveResourceToTownCenter()
    {
        _townCenter.GetResource();
        Destroy(Resource.gameObject);
        ResetWorker();
    }

    public void ResetWorker()
    {
        _workerMover.ResetTarget();
        HasActivity = false;
    }
}