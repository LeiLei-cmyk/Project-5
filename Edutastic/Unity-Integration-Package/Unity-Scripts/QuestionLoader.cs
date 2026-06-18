using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Edutastic.API
{
    /// <summary>
    /// Handles loading questions, courses, and modules from the API.
    /// Questions do NOT include correct answers (security).
    /// </summary>
    public class QuestionLoader
    {
        /// <summary>
        /// Load all questions for a module
        /// </summary>
        /// <param name="moduleId">The module ID from database</param>
        /// <returns>List of questions (without correct answers)</returns>
        public async Task<List<QuestionDTO>> LoadQuestions(string moduleId)
        {
            try
            {
                var questions = await APIClient.Instance.GetAsync<List<QuestionDTO>>(
                    $"api/Question/{moduleId}/module");
                
                Debug.Log($"[QuestionLoader] Loaded {questions.Count} questions");
                return questions;
            }
            catch (Exception e)
            {
                Debug.LogError($"[QuestionLoader] Failed: {e.Message}");
                return new List<QuestionDTO>();
            }
        }
        
        /// <summary>
        /// Load all courses (for course selection screen)
        /// </summary>
        public async Task<List<CourseDTO>> LoadCourses()
        {
            try
            {
                var courses = await APIClient.Instance.GetAsync<List<CourseDTO>>("api/Course");
                Debug.Log($"[QuestionLoader] Loaded {courses.Count} courses");
                return courses;
            }
            catch (Exception e)
            {
                Debug.LogError($"[QuestionLoader] Failed: {e.Message}");
                return new List<CourseDTO>();
            }
        }
        
        /// <summary>
        /// Load modules for a specific course
        /// </summary>
        public async Task<List<ModuleDTO>> LoadModules(string courseId)
        {
            try
            {
                var modules = await APIClient.Instance.GetAsync<List<ModuleDTO>>(
                    $"api/Course/{courseId}/modules");
                
                Debug.Log($"[QuestionLoader] Loaded {modules.Count} modules");
                return modules;
            }
            catch (Exception e)
            {
                Debug.LogError($"[QuestionLoader] Failed: {e.Message}");
                return new List<ModuleDTO>();
            }
        }
    }
    
    [Serializable]
    public class QuestionDTO
    {
        public string id;
        public string module_id;
        public string question_text;
        public string question_type;
        public string options;
    }
    
    [Serializable]
    public class CourseDTO
    {
        public string id;
        public string name;
        public string description;
        public string thumbnail_url;
    }
    
    [Serializable]
    public class ModuleDTO
    {
        public string id;
        public string course_id;
        public string name;
        public int order_index;
    }
}
