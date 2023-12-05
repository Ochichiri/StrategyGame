using UnityEngine;

[RequireComponent(typeof(TownCenter))]
public class TownCenterSpawner : MonoBehaviour
{
    [SerializeField] private TownCenter _townCenterTemplate;

    [SerializeField] private Worker _workerTemplate;
    [SerializeField] private Transform _workerSpawnPoint;

    private Transform _townCenterBuildPoint;
    private TeamOfWorkers _teamOfWorkers;
    private TownCenter TownCenter => GetComponent<TownCenter>();

    private void Start()
    {
        _teamOfWorkers = GetComponent<TeamOfWorkers>();
    }

    public void CreateWorker()
    {
        Worker newWorker = Instantiate(_workerTemplate, _workerSpawnPoint.position, Quaternion.identity);
        newWorker.Init(GetComponent<TownCenter>());
        _teamOfWorkers.AddWorker(newWorker);
    }

    public void MoveWorkerToBuildPoint(Transform transform)
    {
        _townCenterBuildPoint = transform;
        _teamOfWorkers.TryGetWorker(out Worker worker);
        _teamOfWorkers.DisconnectWorkerFromTownCenter(worker);
        worker.MoveTo(_townCenterBuildPoint);
        worker.GetComponent<WorkerMover>().TargetPositionReached += BuildTownCenter;
    }

    public void BuildTownCenter(WorkerMover workerMover)
    {
        workerMover.TargetPositionReached -= BuildTownCenter;
        TownCenter newTownCenter = Instantiate(_townCenterTemplate, _townCenterBuildPoint.position, Quaternion.identity);
        newTownCenter.GetComponent<TeamOfWorkers>().AddWorker(workerMover.GetComponent<Worker>());
        TownCenter.RemoveFlag();
    }
}