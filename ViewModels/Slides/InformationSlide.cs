using System;
using TMPro;
using UnityEngine.UI;
using ViewModels.Abstracts;

namespace ViewModels.Slides
{
    public class InformationSlide : Slide
    {
        public TMP_Text slideText;
        public Button nextButton;

        private void OnEnable()
        {
            slideText.text = slideContent.QuestionText;
            nextButton.onClick.AddListener(OnNextButtonClicked);
        }

        private void OnDisable()
        {
            nextButton.onClick.RemoveAllListeners();
        }

        private void OnNextButtonClicked()
        {
            quizManager.ShowNextSlide();
        }
    }
}