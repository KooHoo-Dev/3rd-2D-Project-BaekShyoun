using UnityEngine;
using Study.Utilities;
using UnityEngine.InputSystem;

namespace PracticeProject
{
    public class DebugSpawnBeast : MonoBehaviour
    {
        [field: SerializeField] public GameObject GameObject { get; private set; }

        public void Awake()
        {
            GameObject.SetActive(true);
        }

        public void Update()
        {
            if(SimpleInput.GetKeyDown(Key.R))
            {
                GameObject.SetActive(false);
            }
            if(SimpleInput.GetKeyDown(Key.F))
            {
                GameObject.SetActive(true);
            }
        }
    }

}