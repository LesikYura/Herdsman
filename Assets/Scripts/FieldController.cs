using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using View;

public class FieldController : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    /// Create field
    /// Check click on field
    /// </summary>
    
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private CanvasScaler _canvasScaler;
    [SerializeField] private RectTransform _winZoneRect;

    private Camera _mainCamera;
    private PlayerController _player;
    private Vector2 _fieldSizeHalf = Vector2.zero;

    private readonly int _enemySize = 100;

    public void CreateField(Camera mainCamera, PlayerController player)
    {
        _mainCamera = mainCamera;
        _player = player;

        var fieldSize = _canvasScaler.referenceResolution;
        _boxCollider.size = fieldSize;
        _fieldSizeHalf = new Vector2(fieldSize.x / 2, fieldSize.y / 2);
    }

    public Vector2 GetSpawnPosition()
    {
        return new Vector2(Random.Range(-_fieldSizeHalf.x, _fieldSizeHalf.x - _winZoneRect.rect.width),
            Random.Range(-_fieldSizeHalf.y, _fieldSizeHalf.y));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_mainCamera != null && _player != null)
        {
            var ray = _mainCamera.ScreenPointToRay(eventData.position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                _player.Move(hit.point);
            }
        }
    }
}
