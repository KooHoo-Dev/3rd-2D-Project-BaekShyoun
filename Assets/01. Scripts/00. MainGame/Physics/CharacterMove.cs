using Study.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ObservedProject
{
    public class CharacterMove : MonoBehaviour
    {
        public enum State
        {
            Idle = 0,
            Left,
            Right,
            Up,
        }

        [Header("Settings")]
        public float speed = 2.0f;
        public float skinWidth = 0.02f;
        public GameObject[] PlayerState;


        private State currentState = State.Idle;
        private Vector3 externalDelta;

        private Rigidbody2D rBody;
        private Collider2D col;

        private void Awake()
        {
            rBody = GetComponent<Rigidbody2D>();
            col = GetComponent<Collider2D>();

            rBody.bodyType = RigidbodyType2D.Kinematic;

            for (int i = 0; i < PlayerState.Length; ++i)
            {
                PlayerState[i].SetActive(false);
            }
            PlayerState[0].SetActive(true);
        }


        private void FixedUpdate()
        {
            float horizontal = 0.0f;
            float vertical = 0.0f;

            // 수평
            if (SimpleInput.GetKey(Key.A))
            {
                SetState(State.Left);
                horizontal = -1.0f;
            }
            else if (SimpleInput.GetKey(Key.D))
            {
                SetState(State.Right);
                horizontal = 1.0f;
            }
            
            // 수직
            else if (SimpleInput.GetKey(Key.S))
            {
                SetState(State.Idle);
                vertical = -1.0f;
            }
            else if (SimpleInput.GetKey(Key.W))
            {
                SetState(State.Up);
                vertical = 1.0f;
            }
            else
            {
                horizontal = 0.0f;
                vertical = 0.0f;
            }

            // 이번 프레임에 움직이고 싶은 이동량
            Vector3 desireHorizontalMove =
                new Vector3(horizontal * speed, 0) * Time.fixedDeltaTime + externalDelta;
            Vector3 desireVerticalMove =
                new Vector3(0, vertical * speed) * Time.fixedDeltaTime + externalDelta;

            Vector3 moveVector = Vector3.zero;
            moveVector.x = ResolveAxisMovement(desireHorizontalMove.x, Vector3.right);
            moveVector.y = ResolveAxisMovement(desireVerticalMove.y, Vector3.up);
            rBody.MovePosition(transform.position + moveVector);
        }

        public LayerMask collisionLayer;

        private void SetState(State state)
        {
            if (currentState == state) return;

            PlayerState[(int)currentState].SetActive(false);
            PlayerState[(int)state].SetActive(true);
            currentState = state;
        }


        private float ResolveAxisMovement(float move, Vector3 axis)
        {
            if (move == 0.0f) return 0.0f;

            Vector3 dir = move > 0.0f ? axis : -axis;
            float distance = Mathf.Abs(move);

            Bounds box = GetCastBox();
            RaycastHit2D hit =
                Physics2D.BoxCast(box.center, box.size, 0.0f, dir, distance, collisionLayer);

            if (hit.collider == null) return move;
            float allowed = Mathf.Max(0, hit.distance - skinWidth);

            return Mathf.Sign(move) * allowed;
        }

        private Bounds GetCastBox()
        {
            Bounds box = col.bounds;
            Vector3 size = box.size;
            size.x -= skinWidth * 2;
            box.size = size;

            return box;
        }
        public void AddExternalMovement(Vector3 delta)
        {
            externalDelta += delta;
        }
    }

}
    