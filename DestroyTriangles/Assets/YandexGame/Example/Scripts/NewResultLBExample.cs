using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace YG.Example
{
    public class NewResultLBExample : MonoBehaviour
    {
        [SerializeField] LeaderboardYG leaderboardYG;
        [SerializeField] TextMeshProUGUI scoreLbInputField;

        public void NewScore()
        {
            YandexGame.NewLeaderboardScores(leaderboardYG.nameLB, int.Parse(scoreLbInputField.text));
            leaderboardYG.NewScore(int.Parse(scoreLbInputField.text));
        }
    }
}