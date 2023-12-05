using UnityEngine;

public class TownCenterBody : MonoBehaviour
{
    [SerializeField] private Material _selectedMaterial;
    
    private Material _defaultMaterial;
    private MeshRenderer _currentMaterial;

    private void Awake()
    {
        _currentMaterial = GetComponent<MeshRenderer>();
        _defaultMaterial = _currentMaterial.material;
    }

    public void ChangeColor()
    {
        _currentMaterial.material = _selectedMaterial;
    }
    
    public void ReturnColor()
    {
        _currentMaterial.material = _defaultMaterial;
    }
}