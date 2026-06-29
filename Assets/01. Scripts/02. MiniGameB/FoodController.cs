using UnityEngine;
using UnityEngine.UI;

namespace ObservedProject
{
    public class FoodController : MonoBehaviour
    {
        public enum Food
        {
            QFood = 0,
            WFood,
            EFood,
            RFood,
        }

        public Food currentFood;
        public Image foodImage;
        public Sprite[] images;

        public void SetFood(Food food)
        {
            currentFood = food;
            foodImage.sprite = images[(int)currentFood];
        }

        public void SetRandomFood()
        {
            currentFood = (Food)Random.Range(0, 4);
            foodImage.sprite = images[(int)currentFood];
        }
    }
}