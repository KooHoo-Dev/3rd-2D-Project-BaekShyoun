using ObservedProject;
using System.Collections;
using UnityEngine;

public class OrderTurn : MonoBehaviour
{
    public GameObject[] orders;
    public GameObject timer;
    public GameObject pressedFood;
    private FoodChoice foodChoice;
    private TimeController timeController;
    private IEnumerator nextStartCoroutine;

    public void Awake()
    {
        for (int i = 0; i < orders.Length; ++i)
            orders[i].SetActive(false);
        timeController = timer.GetComponent<TimeController>();
        foodChoice = pressedFood.GetComponent<FoodChoice>();
    }

    public void Start()
    {
        nextStartCoroutine = foodChoice.SetFoodCoroutine();
        MakeOrder();
        StartCoroutine(nextStartCoroutine);
    }

    private void MakeOrder()
    {
        StartCoroutine(OrderCoroutine());
    }

    private IEnumerator OrderCoroutine()
    {
        for (int i = 0; i < orders.Length; ++i)
        {
            orders[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
        StartCoroutine(timeController.ThreeSecCoroutine());
    }
}
