using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ObservedProject
{
    public class MiniGameBManager : MonoBehaviour
    {
        #region UI
        public enum HeartState
        {
            Empty,
            Full
        }
        [Header("UI Settings")]
        public TextMeshProUGUI ScoreText;
        public HeartState heartState;
        public GameObject[] hearts;
        public Image heartImage;
        public Sprite[] heartImages;


        #endregion

        #region GameManager


        #endregion
    }
}