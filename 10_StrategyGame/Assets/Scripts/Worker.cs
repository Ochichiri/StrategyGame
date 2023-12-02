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

    private void Start()
    {
        _workerMover = GetComponent<WorkerMover>();
        _workerCollision = GetComponent<WorkerCollision>();

        if (_townCenter != null)
        {
            Init(_townCenter);
        }
    }

    public void Init(TownCenter townCenter)
    {
        _townCenter = townCenter;
        _workerMover.Init(townCenter.transform);
    }

    public void MoveToResource(Resource resource)
    {
        HasActivity = true;
        _workerCollision.SetTargetResource(resource);
        _workerMover.SetTarget(resource.transform);
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
        _workerMover.ResetTarget();
        HasActivity = false;
    }
}