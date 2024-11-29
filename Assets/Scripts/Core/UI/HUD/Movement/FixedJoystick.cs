using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.UI.HUD.Movement
{
    public class FixedJoystick : MonoBehaviour, IPointerDownHandler, 
        IPointerUpHandler, IDragHandler
    {
        public Vector2 Input { get; private set; }

        [SerializeField] private RectTransform _background;
        [SerializeField] private RectTransform _handle;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private bool _canResetOnUp = true;

        private Vector2 _backgroundPosition;
        private Vector2 _backgroundRadius;

        private void Awake()
        {
            if (_background == null || _handle == null || _canvas == null)
                Debug.LogError($"{name}: field(-s) is null!");
        }

        private void Start()
        {
            _backgroundPosition = RectTransformUtility
                .WorldToScreenPoint(_canvas.worldCamera, _background.position);
            _backgroundRadius = _background.sizeDelta / 2;
        }

        public void OnPointerDown(PointerEventData eventData) =>
            OnDrag(eventData);

        public void OnPointerUp(PointerEventData eventData)
        {
            _handle.anchoredPosition = Vector2.zero;
            if (_canResetOnUp)
                Input = Vector2.zero;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Input = (eventData.position - _backgroundPosition) /
                (_backgroundRadius * _canvas.scaleFactor);
            if (Input.magnitude > 1)
                Input = Input.normalized;
            _handle.anchoredPosition = Input * _backgroundRadius;
        }
    }
}
