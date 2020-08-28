using Models;
using Services;
using UnityEngine;

namespace ViewModels.Abstracts
{
    public abstract class Slide : MonoBehaviour
    {
        public int slideId;
        public float points;
        public QuizQuestionModel slideContent;
        public QuizManager quizManager;
    }
}