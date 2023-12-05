using System.Collections.Generic;
using UnityEngine;

enum Item
{
    Worker,
    TownCenter
}

[RequireComponent(typeof(TownCenterSpawner))]
[RequireComponent(typeof(TeamOfWorkers))]
public class TownCenter : MonoBehaviour
{
    [SerializeField] private GameObject _flag;
    [SerializeField] private TownCenterBody _townCenterBody;

    private Dictionary<Item, int> _costsOfItems = new Dictionary<Item, int>()
    {
        {Item.Worker, 3 },
        {Item.TownCenter, 5 }
    };

    private int _resourcesCounter = 0;
    private Item _creatingTarget = Item.Worker;
    private TownCenterSpawner _townCenterSpawner;

    private TeamOfWorkers _teamOfWorkers;

    public bool NeedWorker { get; private set; } = false;

    private void Awake()
    {
        _townCenterSpawner = GetComponent<TownCenterSpawner>();
    }

    public void GetResource()
    {
        _resourcesCounter++;
        if (_creatingTarget == Item.Worker)
        {
            TryToCreateWorker();
        }
    }

    private void TryToCreateWorker()
    {
        if (TryBuyItem(Item.Worker))
        {
            _townCenterSpawner.CreateWorker();
        }
    }

    private bool TryBuyItem(Item item)
    {
        if (_resourcesCounter >= _costsOfItems[item])
        {
            _resourcesCounter -= _costsOfItems[item];
            return true;
        }
        else
        {
            return false;
        }
    }

    public void TryToCreateTownCenter()
    {
        if (TryBuyItem(Item.TownCenter))
        {
            _townCenterSpawner.MoveWorkerToBuildPoint(_flag.transform);
            _creatingTarget = Item.Worker;
            NeedWorker = false;
        }
    }

    public void SetFlag(Vector3 position)
    {
        _creatingTarget = Item.TownCenter;
        NeedWorker = true;
        _flag.SetActive(true);
        _flag.transform.position = position;
        _townCenterBody.ReturnColor();
    }

    public void SelectTownCenter()
    {
        _townCenterBody.ChangeColor();
    }

    public void UnselectTownCenter()
    {
        _townCenterBody.ReturnColor();
    }

    public void RemoveFlag()
    {
        _flag.SetActive(false);
    }
}