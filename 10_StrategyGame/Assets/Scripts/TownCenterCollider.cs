using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class TownCenterCollider : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<BoxCollider>().isTrigger = true;
    }
}