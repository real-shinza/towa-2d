using UnityEngine;
using UnityEngine.UI;

namespace Game
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
            UpdateScoreText();
        }

        public void BlockStrikingEnemy()
        {
            AddScore(blockStrikingEnemyScore);
            UpdateScoreText();
        }

        public void KillEnemy()
        {
            AddScore(killEnemyScore);
            UpdateScoreText();
        }
    }
}
