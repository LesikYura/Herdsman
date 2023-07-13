using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FieldController : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    /// Create field
    /// Check click on field
    /// Set bounds
    /// Get spawn position
    /// </summary>
    
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private CanvasScaler _canvasScaler;
    [SerializeField] private RectTransform _winZoneRect;

    private Camera _mainCamera;
    private PlayerController _player;
    private Vector2 _fieldSizeHalf = Vector2.zero;
    private Bounds _bounds;

    private readonly int _bordedShift = 50;

    public void CreateField(Camera mainCamera, PlayerController player)
    {
        _mainCamera = mainCamera;
        _player = player;

        SetBounds();
    }

    public Vector2 GetSpawnPosition()
    {
        return new Vector2(Random.Range(_bounds.min.x, _bounds.max.x), Random.Range(_bounds.min.y, _bounds.max.y));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_mainCamera != null && _player != null)
        {
            var ray = _mainCamera.ScreenPointToRay(eventData.position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                _player.SetMoveData(hit.point);
            }
        }
    }
    
    private void SetBounds()
    {
        var fieldSize = _canvasScaler.referenceResolution;
        _boxCollider.size = fieldSize;
        _fieldSizeHalf = new Vector2(fieldSize.x / 2, fieldSize.y / 2);

        var minX = -_fieldSizeHalf.x + _bordedShift;
        var maxX = _fieldSizeHalf.x - _winZoneRect.rect.width - _bordedShift;
        var minY = -_fieldSizeHalf.y + _bordedShift;
        var maxY = _fieldSizeHalf.y - _bordedShift;
        _bounds = new Bounds();
        _bounds.SetMinMax(new Vector2(minX, minY), new Vector2(maxX, maxY));
    }
}
