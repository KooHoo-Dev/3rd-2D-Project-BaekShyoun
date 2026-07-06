using Study.Utilities;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ObservedProject
{
    public class FoodChoice : MonoBehaviour
    {
        public GameObject timer;
        public GameObject[] foodImages;
        private TimeController timeController;
        public FoodController[] pickedFoods { get; private set; }
        private MiniGameBManager gameManager;
        private int currentIndex;
        private bool isCheck = false;

        private void Awake()
        {
            timeController = timer.GetComponent<TimeController>();
            gameManager = GetComponentInParent<MiniGameBManager>();
            pickedFoods = new FoodController[foodImages.Length];
            SetAllFoodsInactive();
        }


        public IEnumerator SetFoodCoroutine()
        {
            while(timeController.isDecreased == false)
            {
                yield return null;
            }
            while(true)
            {
                if (currentIndex >= foodImages.Length) break;
                if (timeController.timeBar.value <= 0)
                {
                    gameManager.EraseHeart();
                    yield break;
                }
                SetPressedFood();
                yield return null;
            }
        }

        private void SetPressedFood()
        {
            if (SimpleInput.GetKeyDown(Key.Q))
            {
                AddFood(FoodController.Food.QFood);
                Debug.Log("Q");
            }
            else if (SimpleInput.GetKeyDown(Key.W))
            {
                AddFood(FoodController.Food.WFood);
                Debug.Log("W");
            }
            else if (SimpleInput.GetKeyDown(Key.E))
            {
                AddFood(FoodController.Food.EFood);
                Debug.Log("E");
            }
            else if (SimpleInput.GetKeyDown(Key.R))
            {
                AddFood(FoodController.Food.RFood);
                Debug.Log("R");
            }
        }

        public void SetAllFoodsInactive()
        {
            for (int i = 0; i < foodImages.Length; ++i)
            {
                foodImages[i].SetActive(false);
            }
        }

        private void AddFood(FoodController.Food food)
        {
            if (currentIndex >= foodImages.Length) return;

            pickedFoods[currentIndex] = foodImages[currentIndex].GetComponent<FoodController>();

            foodImages[currentIndex].SetActive(true);
            pickedFoods[currentIndex].SetFood(food);
            currentIndex++;

            if (currentIndex >= foodImages.Length && isCheck == false)
            {
                isCheck = true;
                StartCoroutine(CheckAnswersAfterDelay());
            }
        }

        private IEnumerator CheckAnswersAfterDelay()
        {
            yield return new WaitForSeconds(0.1f);
            gameManager.CheckAnswers();
        }


        public void ResetFoodChoice()
        {
            isCheck = false;
            currentIndex = 0;
            SetAllFoodsInactive();
            StopAllCoroutines();
        }
    }
}