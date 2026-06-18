# 🔧 API Configuration Guide

## Find Your Computer's IP Address

### Windows
1. Open Command Prompt
2. Type: `ipconfig`
3. Look for **IPv4 Address** (e.g., `192.168.1.100`)

### Mac/Linux
1. Open Terminal
2. Type: `ifconfig` or `ip addr`
3. Look for your network IP (e.g., `192.168.1.100`)

## Update API URL in Unity

1. Select the `APIClient` GameObject in Hierarchy
2. In Inspector, find **Base URL** field
3. Change from `http://localhost:5069` to:
http://192.168.1.100:5069

text

(Replace with YOUR IP address)

## Start the API Server

On the host computer (where API is running):

```bash
cd C:\Users\jerem\source\repos\Edutastic\Edutastic.API
dotnet run --urls=http://0.0.0.0:5069
```
Important: 0.0.0.0 allows connections from other devices!

Firewall Setup (Windows)
If connection fails, allow the port:

Open Windows Defender Firewall
Click Advanced Settings
Click Inbound Rules → New Rule
Select Port → TCP
Enter port: 5069
Select Allow the connection
Name it: Edutastic API
Test Connection
In Unity, create a test script:

csharp

using UnityEngine;
using Edutastic.API;

public class ConnectionTest : MonoBehaviour
{
    async void Start()
    {
        await System.Threading.Tasks.Task.Delay(1000);
        
        try
        {
            var loader = new QuestionLoader();
            var questions = await loader.LoadQuestions("YOUR-MODULE-ID");
            Debug.Log($"✅ Connection successful! Loaded {questions.Count} questions");
        }
        catch
        {
            Debug.LogError("❌ Connection failed! Check IP address and firewall");
        }
    }
}
Common Issues
Issue	Solution
Connection refused	API not running or wrong IP
Timeout	Firewall blocking port 5069
404 Not Found	Wrong module ID or endpoint
CORS error	API should allow all origins by default
text
