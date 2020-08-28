using System;

namespace Models
{
    [Serializable]
    public class QuizQuestionModel
    {
        public string QuestionText;
        public AnswerQuestionModel[] AnswerModels;
    }
}