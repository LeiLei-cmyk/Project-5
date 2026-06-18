# 🎮 Edutastic Unity API Integration

## Quick Start (5 Minutes)

### 1. Copy Scripts to Unity
Copy all files from `Unity-Scripts/` into your Unity project:
Assets/Scripts/API/

text


### 2. Create API Client GameObject
1. In Unity Hierarchy, right-click → **Create Empty**
2. Name it: `APIClient`
3. Add Component: `APIClient`
4. Set **Base URL** to: `http://YOUR-IP-ADDRESS:5069`

### 3. Load Questions
```csharp
// In your existing question manager script
using Edutastic.API;

public class QuestionManager : MonoBehaviour
{
    private QuestionLoader questionLoader;
    
    void Start()
    {
        questionLoader = new QuestionLoader();
        LoadQuestionsForModule("YOUR-MODULE-ID");
    }
    
    async void LoadQuestionsForModule(string moduleId)
    {
        var questions = await questionLoader.LoadQuestions(moduleId);
        // Use questions in your game
    }
}
```
4. Test Connection
Press Play in Unity
Check Console for [API] logs
If you see "Questions loaded: X", it works!
API Endpoints Available
Endpoint	Method	Purpose
/api/Course	GET	Get all courses
/api/Course/{id}/modules	GET	Get modules for course
/api/Question/{moduleId}/module	GET	Get questions (NO answers!)
/api/Game/start	POST	Start game session
/api/Game/{id}/answer	POST	Submit answer
/api/Game/{id}/end	POST	End game & save score
Troubleshooting
Problem	Solution
Connection refused	Check API is running (dotnet run)
Wrong IP address	Use your computer's local IP, not localhost
CORS errors	API allows all origins by default
Null reference	Check APIClient GameObject exists in scene
Contact
For issues, contact your team backend developer.

text
