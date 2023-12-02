using UnityEngine;

[RequireComponent(typeof(Worker))]
public class WorkerCollision : MonoBehaviour
{
    private Worker _worker;
    private Resource _nesseceryResource;

    private void Start()
    {
        _worker = GetComponent<Worker>();
    }

    public void SetTargetResource(Resource resource)
    {
        if (_nesseceryResource == null)
        {
            _nesseceryResource = resource;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resource resource))
        {
            if (resource == _nesseceryResource)
            {
                _worker.GetResource(resource);
            }
        }

        if (other.TryGetComponent(out TownCenter townCenter))
        {
            if (_worker.Resource != null && _worker.TownCenter == townCenter)
            {
                _worker.GiveResourceToTownCenter();
            }
        }
    }
}