using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private LayerMask _layerGround;
    [SerializeField] private LayerMask _layerTownCenter;
    [SerializeField] private Camera _camera;
    [SerializeField] private TownCenter _selectedTownCenter;

    private int _rayDistance = 100;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseClick();
        }
    }

    private void MouseClick()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (_selectedTownCenter != null)
        {
            if (Physics.Raycast(ray, out RaycastHit groundHit, _rayDistance, _layerGround))
            {
                _selectedTownCenter.SetFlag(groundHit.point);
                _selectedTownCenter = null;
            }
        }
        else
        {
            if (Physics.Raycast(ray, out RaycastHit townCenterHit, _rayDistance, _layerTownCenter))
            {
                if (townCenterHit.collider.TryGetComponent(out TownCenter townCenter))
                {
                    if (_selectedTownCenter != null)
                    {
                        townCenter.UnselectTownCenter();
                    }

                    _selectedTownCenter = townCenter;
                    townCenter.SelectTownCenter();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        Gizmos.DrawRay(ray);
    }
}