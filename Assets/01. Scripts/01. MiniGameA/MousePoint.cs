using Study.Utilities;
using UnityEngine;

namespace ObservedProject
{
    public class MousePoint : MonoBehaviour
    {
        private float ScreenWidth { get; set; }
        private float ScreenHeight { get; set; }

        private void Start()
        {
            ScreenWidth = Screen.width;
            ScreenHeight = Screen.height;
        }

        private void Update()
        {
            Vector2 mousePosition = SimpleInput.GetMousePosition();
            Vector3 worldMousePoint = Camera.main.ScreenToWorldPoint(mousePosition);
            worldMousePoint.z = transform.position.z;

            transform.position = worldMousePoint;
        }
    }
}