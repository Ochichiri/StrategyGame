using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Scanner : MonoBehaviour
{
    public List<Resource> Resources { get; private set; }

    private void Start()
    {
        Resources = new List<Resource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resource resource))
        {
            Resources.Add(resource);
        }
    }
}