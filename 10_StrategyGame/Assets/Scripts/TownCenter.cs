using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TownCenter : MonoBehaviour
{
    [SerializeField] private Transform _workersContainer;
    [SerializeField] private Scanner _scanner;
    [SerializeField] private int _resourcesCounter = 0;

    private List<Worker> _workers;

    private void Start()
    {
        _workers = new List<Worker>();

        for (int i = 0; i < _workersContainer.childCount; i++)
        {
            if (_workersContainer.GetChild(i).gameObject.TryGetComponent(out Worker worker))
                _workers.Add(worker);
        }
    }

    private void FixedUpdate()
    {
        if (_scanner.Resources != null)
        {
            OrderToCollect();
        }
    }

    private void OrderToCollect()
    {
        List<Resource> resources = _scanner.Resources;

        foreach(Resource resource in resources.ToList())
        {
            if(TryGetWorker(out Worker worker))
            {
                worker.MoveToResource(resource);
                resources.Remove(resource);
            }
            else
            {
                return;
            }
        }
    }

    private bool TryGetWorker(out Worker worker)
    {
        worker = _workers.FirstOrDefault(worker => worker.HasActivity == false);

        return worker != null;
    }

    public void GetResource()
    {
        _resourcesCounter++;
    }
}