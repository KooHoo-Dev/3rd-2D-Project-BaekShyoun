using System.Collections.Generic;
using Study.Utilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ObservedProject
{
    public class HorseMove : MonoBehaviour
    {
        [Header("Settings")]
        public float minSpeed = 1.0f;
        public float maxSpeed = 3.0f;
        public float currentSpeed;
        public int horseNumber;
        public int horseRank;

        private bool isMove = false;
        public bool isGoal { get; private set; } = false;
        private Animator horseAnimation;
        public RankManager rankManager;
        public MiniGameAManager uiController;

        private void Awake()
        {
            horseAnimation = GetComponentInChildren<Animator>();
            horseAnimation.SetBool("IsRun", false);
            isMove = false;
            currentSpeed = Random.Range(minSpeed, maxSpeed);
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
            transform.position += Vector3.right * currentSpeed * Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("EnterPoint"))
            {
                rankManager.SetRank(this);
            }

            if (collision.CompareTag("GoalPoint"))
            {
                Debug.Log($"{horseNumber}번 말 : {horseRank}위");
                isMove = false;
                isGoal = true;
                horseAnimation.SetBool("IsRun", false);

                uiController.CheckRank();
            }
        }
    }
}