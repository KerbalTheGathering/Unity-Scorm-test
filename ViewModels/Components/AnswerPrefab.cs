using System;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ViewModels.Components
{
    public class AnswerPrefab : MonoBehaviour
    {
        public bool isCorrect;
        public TMP_Text answerText;
        public Button button;
        private QuizManager _quizManager;

        public void Configure(QuizManager quizManager, string answerText, bool isCorrect, char buttonLabel)
        {
            _quizManager = quizManager;
            this.isCorrect = isCorrect;
            this.answerText.text = answerText;
            button.GetComponentInChildren<TMP_Text>(true).text = buttonLabel.ToString();
            button.onClick.RemoveAllListeners();
            if (isCorrect)
            {
                button.onClick.AddListener(IsCorrectOnClick);
            }
            else
            {
                button.onClick.AddListener(NotCorrectOnClick);
            }
        }
        
        private void IsCorrectOnClick()
        {
            Debug.Log("Correct Answer clicked");
            _quizManager.CorrectAnswerClicked();
        }

        private void NotCorrectOnClick()
        {
            Debug.Log("Incorrect Answer clicked");
            _quizManager.IncorrectAnswerClicked();
        }
    }
}