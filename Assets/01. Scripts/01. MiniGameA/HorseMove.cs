using Study.Utilities;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ObservedProject
{
    public class HorseMove : MonoBehaviour
    {
        public enum HorseState
        {
            Perfect = 0,
            Good,
            NotBad,
            Bad
        }

        [Header("Settings")]
        public float minSpeed = 1.5f;
        public float maxSpeed = 3.0f;
        public float currentSpeed;
        public int horseNumber;
        public HorseState state;
        public int horseRank;

        private bool isMove = false;
        public bool isGoal { get; private set; } = false;
        private Animator horseAnimation;
        public RankManager rankManager;
        public MiniGameAManager gameManager;

        private void Awake()
        {
            horseAnimation = GetComponentInChildren<Animator>();
            horseAnimation.SetBool("IsRun", false);
            isMove = false;
            currentSpeed = Random.Range(minSpeed, maxSpeed);
            SetHorseState();
        }

        private void Update()
        {
            if (isMove) MoveHorse();
        }

        public void StartRace()
        {
            if (isGoal == false) isMove = true;
        }

        private void MoveHorse()
        {
            horseAnimation.SetBool("IsRun", true);
            transform.position += Vector3.right * currentSpeed * Time.deltaTime * SetBuffedSpeed();
        }

        private void SetHorseState()
        {
            int randNum = Random.Range((int)HorseState.Perfect, (int)HorseState.Bad + 1);
            state = (HorseState)randNum;
        }

        private float SetBuffedSpeed()
        {
            switch (state)
            {
                case HorseState.Perfect:
                    return 1.2f;
                case HorseState.Good:
                    return 1.02f;
                case HorseState.NotBad:
                    return 1.0f;
                case HorseState.Bad:
                    return 0.8f;
            }
            return 1.0f;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("EnterPoint"))
            {
                rankManager.SetRank(this);
            }

            if (collision.CompareTag("GoalPoint"))
            {
                Debug.Log($"{horseNumber}번 말 : {horseRank}위");
                isMove = false;
                isGoal = true;
                horseAnimation.SetBool("IsRun", false);

                gameManager.CheckRank();
            }
        }
    }
}