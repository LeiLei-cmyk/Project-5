using System;

namespace Edutastic.API.DTOs
{
    /// <summary>
    /// Data Transfer Objects for Course and Module API responses.
    /// </summary>
    
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
