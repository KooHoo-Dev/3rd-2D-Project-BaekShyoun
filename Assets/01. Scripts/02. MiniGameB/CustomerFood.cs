using UnityEngine;
using static ObservedProject.FoodController;

namespace ObservedProject
{
    public class CustomerFood : MonoBehaviour
    {
        public GameObject customerFood;
        private FoodController foodController;

        private void Awake()
        {
            foodController = GetComponentInChildren<FoodController>();
        }
        
        private void Start()
        {
            customerFood.SetActive(false);
            SetCustomerFood();
        }

        public void SetCustomerFood()
        {
            customerFood.SetActive(true);
            foodController.SetRandomFood();
        }
    }
}