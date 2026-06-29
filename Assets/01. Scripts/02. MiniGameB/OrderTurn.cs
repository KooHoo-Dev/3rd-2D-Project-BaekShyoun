using ObservedProject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderTurn : MonoBehaviour
{
    public GameObject[] orders;
    public GameObject timer;
    public GameObject pressedFood;

    private FoodChoice foodChoice;
    private TimeController timeController;
    public FoodController[] orderedFoods { get; private set; }
    private CustomerFood customerFood;

    public void Awake()
    {
        orderedFoods = new FoodController[orders.Length];
        for (int i = 0; i < orders.Length; ++i)
        {
            orders[i].SetActive(false);
            orderedFoods[i] = orders[i].GetComponentInChildren<FoodController>(true);
        }
        timeController = timer.GetComponent<TimeController>();
        foodChoice = pressedFood.GetComponent<FoodChoice>();
    }

    public void MakeOrder()
    {
        StartCoroutine(OrderCoroutine());
        StartCoroutine(foodChoice.SetFoodCoroutine());
    }

    private IEnumerator OrderCoroutine()
    {
        for (int i = 0; i < orders.Length; ++i)
        {
            customerFood = orders[i].GetComponent<CustomerFood>();
            orders[i].gameObject.SetActive(true);
            customerFood.SetCustomerFood();
            yield return new WaitForSeconds(0.5f);
        }
        StartCoroutine(timeController.ThreeSecCoroutine());
    }

    public void ResetOrderTurn()
    {
        for (int i = 0; i < orders.Length; ++i)
        {
            orders[i].SetActive(false);
        }
        StopAllCoroutines();
    }
}
