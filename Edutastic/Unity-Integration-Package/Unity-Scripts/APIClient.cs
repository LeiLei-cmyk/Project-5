using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Edutastic.API
{
    /// <summary>
    /// Singleton API client for Unity to communicate with ASP.NET Core backend.
    /// Add this component to a GameObject in your scene (e.g., "APIClient").
    /// </summary>
    public class APIClient : MonoBehaviour
    {
        public static APIClient Instance { get; private set; }
        
        [Header("API Settings")]
        [Tooltip("Your API server URL (e.g., http://192.168.1.100:5069)")]
        public string baseUrl = "http://localhost:5069";
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        /// <summary>
        /// Send a GET request to the API
        /// </summary>
        public async Task<T> GetAsync<T>(string endpoint)
        {
            string url = $"{baseUrl}/{endpoint}";
            Debug.Log($"[API] GET: {url}");
            
            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Accept", "application/json");
                
                await request.SendWebRequest();
                
                if (request.result == UnityWebRequest.Result.Success)
                {
                    return JsonUtility.FromJson<T>(request.downloadHandler.text);
                }
                else
                {
                    Debug.LogError($"[API] GET Error: {request.error}");
                    Debug.LogError($"[API] Response: {request.downloadHandler.text}");
                    throw new Exception(request.error);
                }
            }
        }
        
        /// <summary>
        /// Send a POST request to the API
        /// </summary>
        public async Task<T> PostAsync<T>(string endpoint, object data)
        {
            string url = $"{baseUrl}/{endpoint}";
            string json = data != null ? JsonUtility.ToJson(data) : "{}";
            Debug.Log($"[API] POST: {url}");
            Debug.Log($"[API] Body: {json}");
            
            using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
            {
                byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Accept", "application/json");
                
                await request.SendWebRequest();
                
                if (request.result == UnityWebRequest.Result.Success)
                {
                    return JsonUtility.FromJson<T>(request.downloadHandler.text);
                }
                else
                {
                    Debug.LogError($"[API] POST Error: {request.error}");
                    Debug.LogError($"[API] Response: {request.downloadHandler.text}");
                    throw new Exception(request.error);
                }
            }
        }
    }
}
