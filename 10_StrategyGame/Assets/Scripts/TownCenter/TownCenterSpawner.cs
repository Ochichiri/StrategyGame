using UnityEngine;

public class TownCenterSpawner : MonoBehaviour
{
    [SerializeField] private TownCenter _townCenterTemplate;
    [SerializeField] private Transform _townCenterSpawnPoint;

    [SerializeField] private Worker _workerTemplate;
    [SerializeField] private Transform _workerSpawnPoint;

    private TeamOfWorkers _townCenterWorkers;

    private void Start()
    {
        _townCenterWorkers = GetComponent<TeamOfWorkers>();
    }

    public void CreateWorker(TownCenter townCenter)
    {
        Worker newWorker = Instantiate(_workerTemplate, _workerSpawnPoint.position, Quaternion.identity);
        newWorker.Init(townCenter);
        _townCenterWorkers.AddWorker(newWorker);
    }

    public void BuildTownCenter()
    {

    }
}