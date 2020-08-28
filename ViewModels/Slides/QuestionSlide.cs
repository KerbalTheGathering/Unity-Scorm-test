using UnityEngine;
using ViewModels.Abstracts;
using TMPro;
using ViewModels.Components;

namespace ViewModels.Slides
{
    public class QuestionSlide : Slide
    {
        public TMP_Text questionText;
        public AnswerPrefab answerPrefab;
        public Transform answerContainer;

        private void OnEnable()
        {
            questionText.text = slideContent.QuestionText;
            char buttonLabel = 'A';
            for (var i = 0; i < slideContent.AnswerModels.Length; i++)
            {
                var answer = slideContent.AnswerModels[i];
                var go = Instantiate(answerPrefab, answerContainer);
                go.Configure(quizManager
                    , answer.AnswerText
                    , answer.IsCorrect
                    , (char)(buttonLabel + i));
                go.gameObject.SetActive(true);
            }
        }
    }
}