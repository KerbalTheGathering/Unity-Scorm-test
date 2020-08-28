using System;
using System.ComponentModel;
using System.Linq;
using Models;
using Newtonsoft.Json;
using Scorm;
using UnityEngine;
using ViewModels.Abstracts;
using ViewModels.Slides;
using Version = Scorm.Version;

namespace Services
{
    public class QuizManager : MonoBehaviour
    {
        public Slide[] slides;
        private QuizQuestionModel[] questions;
        private int _currentSlide;
        private float _points;

        public void CorrectAnswerClicked()
        {
            _points += 5;
            ShowNextSlide();
        }

        public void IncorrectAnswerClicked()
        {
            ShowNextSlide();
        }

        public void ShowNextSlide()
        {
            _currentSlide++;
            if (_currentSlide >= slides.Length)
            {
                ScoreQuiz();
            }
            else
            {
                slides[_currentSlide - 1].gameObject.SetActive(false);
                slides[_currentSlide].gameObject.SetActive(true);
            }
        }
        
        private void Awake()
        {
            _currentSlide = 0;
            if (slides.Length < 1) return;
            
            var data = Resources.Load<TextAsset>("data").text;
            questions = JsonConvert.DeserializeObject<QuizQuestionModel[]>(data);

            if (questions.Length < 1) return;
            foreach (var slide in slides)
            {
                slide.quizManager = this;
                slide.slideContent = questions[slide.slideId];
            }

            foreach (var slide in slides)
            {
                slide.gameObject.SetActive(false);
            }
            
            slides[_currentSlide].gameObject.SetActive(true);
        }

        private void ScoreQuiz()
        {
            var possiblePoints = slides.Sum(slide => slide.points);
            var prcnt = _points / possiblePoints;
            Debug.Log("Total points: " + _points);
            Debug.Log("Percentage: " + prcnt.ToString("P1"));
            Debug.Log("Sending score to LMS");
            IScormService scormService;
#if UNITY_EDITOR
            scormService = new ScormPlayerPrefsService();
#else
            scormService = new ScormService();
#endif

            var res = scormService.Initialize(Version.Scorm_1_2);
            if (!res)
                return;
            scormService.SetRawScore(prcnt * 100);
            scormService.Commit();
            scormService.Finish();
            Debug.Log("Finished sending score to LMS.");
            
            Application.Quit();
        }
    }
}