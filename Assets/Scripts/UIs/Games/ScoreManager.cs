using UnityEngine;
using UnityEngine.UI;

namespace Towa.UI.Game
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField]
        private int totalScore;
        [SerializeField]
        private Text scoreText;
        [SerializeField]
        private int blockIblastScore;
        [SerializeField]
        private int blockStrikingEnemyScore;
        [SerializeField]
        private int killEnemyScore;
        [SerializeField]
        private int goalScore;

        public int TotalScore { get { return totalScore; } }

        private void AddScore(int score)
        {
            totalScore += score;
            UpdateScoreText();
        }

        private void UpdateScoreText()
        {
            scoreText.text = $"{totalScore:D8}";
        }

        public void BlockIblast()
        {
            AddScore(blockIblastScore);
        }

        public void BlockStrikingEnemy()
        {
            AddScore(blockStrikingEnemyScore);
        }

        public void KillEnemy()
        {
            AddScore(killEnemyScore);
        }

        public void Goal(int value)
        {
            AddScore(goalScore * value);
        }
    }
}
