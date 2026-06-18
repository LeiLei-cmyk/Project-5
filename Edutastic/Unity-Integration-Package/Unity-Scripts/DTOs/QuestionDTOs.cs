using System;

namespace Edutastic.API.DTOs
{
    /// <summary>
    /// Data Transfer Objects for Question API responses.
    /// NOTE: correct_answer is NOT included (security - server validates answers).
    /// </summary>
    
    [Serializable]
    public class QuestionDTO
    {
        public string id;
        public string module_id;
        public string question_text;
        public string question_type;
        public string options;
    }
}
