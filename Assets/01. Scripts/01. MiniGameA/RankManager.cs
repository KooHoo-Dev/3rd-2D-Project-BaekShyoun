using UnityEngine;

namespace ObservedProject
{
    public class RankManager : MonoBehaviour
    {
        private int currentRank = 1;

        public void SetRank(HorseMove horse)
        {
            horse.horseRank = currentRank;
            currentRank++;
        }
    }
}
