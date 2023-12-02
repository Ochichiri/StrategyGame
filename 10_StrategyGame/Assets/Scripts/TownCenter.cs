using System.Collections.Generic;
using UnityEngine;

enum Item
{
    Worker,
    TownCenter
}

[RequireComponent(typeof(TownCenterSpawner))]
[RequireComponent(typeof(TownCenterWorkers))]
public class TownCenter : MonoBehaviour
{
    private Dictionary<Item, int> _costsOfItems = new Dictionary<Item, int>()
    {
        {Item.Worker, 3 },
        {Item.TownCenter, 5 }
    };

    [SerializeField] private int _resourcesCounter = 0;

    private Item _creatingTarget = Item.Worker;

    private TownCenterSpawner _townCenterSpawner;

    private void Start()
    {
        _townCenterSpawner = GetComponent<TownCenterSpawner>();
    }

    public void GetResource()
    {
        _resourcesCounter++;
        TryToCreate();
    }

    private void TryToCreate()
    {
        switch (_creatingTarget)
        {
            case Item.Worker:
                if (TryBuyItem(Item.Worker))
                {
                    _townCenterSpawner.CreateWorker(this);
                }
                break;

            case Item.TownCenter:
                if (TryBuyItem(Item.TownCenter))
                {
                    _townCenterSpawner.BuildTownCenter();
                }
                break;

            default:
                Debug.Log("Item not found");
                break;
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

    public void SetFlag()
    {
        _creatingTarget = Item.TownCenter;
    }
}