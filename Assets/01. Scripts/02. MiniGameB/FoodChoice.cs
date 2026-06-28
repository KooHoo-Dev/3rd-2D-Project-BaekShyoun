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
        private int currentIndex;

        private void Awake()
        {
            timeController = timer.GetComponent<TimeController>();
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
                for (int i = 0; i < foodImages.Length; ++i)
                {
                    SetPressedFood();
                    yield return null;
                }
            }
        }

        private void SetPressedFood()
        {
            if (SimpleInput.GetKeyDown(Key.Q))
            {
                AddFood(FoodController.Food.QFood);
            }
            else if (SimpleInput.GetKeyDown(Key.W))
            {
                AddFood(FoodController.Food.WFood);
            }
            else if (SimpleInput.GetKeyDown(Key.E))
            {
                AddFood(FoodController.Food.EFood);
            }
            else if (SimpleInput.GetKeyDown(Key.R))
            {
                AddFood(FoodController.Food.RFood);
            }
        }

        private void SetAllFoodsInactive()
        {
            for (int i = 0; i < foodImages.Length; ++i)
            {
                foodImages[i].SetActive(false);
            }
        }

        private void AddFood(FoodController.Food food)
        {
            if (currentIndex >= foodImages.Length) return;

            FoodController slotFood = foodImages[currentIndex].GetComponent<FoodController>();

            foodImages[currentIndex].SetActive(true);
            slotFood.SetFood(food);

            currentIndex++;
        }
    }
}