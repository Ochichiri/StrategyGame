using System.Collections;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private Resource _template;
    [SerializeField] private Transform _container;

    private int _spawnRange = 20;
    private float _rotationRange = 360f;
    private float _rayDistance = 10f;
    private Vector3 _rayOffset = new Vector3(0, 5f, 0);

    private WaitForSeconds _waitDelay;
    private float _delay = 1f;

    private void Start()
    {
        _waitDelay = new WaitForSeconds(_delay);

        StartCoroutine(SpawnResources());
    }

    private IEnumerator SpawnResources()
    {
        while (true)
        {
            int randomX = Random.Range(-_spawnRange, _spawnRange);
            int randomZ = Random.Range(-_spawnRange, _spawnRange);
            Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, _rotationRange), 0);

            Vector3 position = new Vector3(randomX, 0, randomZ);

            if (Physics.Raycast(position + _rayOffset, Vector3.down, out RaycastHit hit, _rayDistance))
            {
                if (hit.collider.gameObject.GetComponent<Resource>() == false &&
                    hit.collider.gameObject.GetComponent<TownCenterCollider>() == false)
                {
                    Resource resource = Instantiate(_template, position, randomRotation, _container);
                }
            }

            yield return _waitDelay;
        }
    }
}