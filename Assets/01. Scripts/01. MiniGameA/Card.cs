using UnityEngine;

namespace ObservedProejct
{
    public class Card : MonoBehaviour
    {
        public enum Number
        {
            Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Ace,
        }

        public enum State
        {
            Back,
            Front,
        }

        [Header("공용 데이터")]
        public Sprite backImage;
        public Sprite[] numberImages;

        [Header("My Data")]
        public SpriteRenderer cardRenderer;
        public Number number;
        public State state;

        void Awake()
        {
            cardRenderer = gameObject.GetComponent<SpriteRenderer>();
            SetState(State.Back);
        }

         public void SetState(State state)
        {
            if (state == State.Back) cardRenderer.sprite = backImage;
            else cardRenderer.sprite = numberImages[(int)number];

            this.state = state;
        }

        public void Flip()
        {
            if (state == State.Back) SetState(State.Front);
            else SetState(State.Back);
        }
    }
}