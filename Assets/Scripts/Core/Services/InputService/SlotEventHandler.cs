using UnityEngine;
using UnityEngine.EventSystems;
using Core.Inventory;

namespace Core.Services.InputService
{
    public class SlotEventHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public PointerEventData DragEventData { get; private set; }

        [field: SerializeField] public int ID { get; private set; }

        private SlotEventDispatcher _dispatcher;

        public void Construct(SlotEventDispatcher dispatcher) =>
            _dispatcher = dispatcher;

        public void OnBeginDrag(PointerEventData eventData)
        {
            DragEventData = eventData;
            _dispatcher.StartDrag(this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _dispatcher.StopDrag(this);
            DragEventData = null;
        }

        public void OnDrag(PointerEventData eventData) =>
            DragEventData = eventData;
    }
}
