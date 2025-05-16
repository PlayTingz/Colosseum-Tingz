using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

// A helper class to extend MCP functionality with scene management
public class McpSceneHelper : MonoBehaviour
{
    // Add this component to a GameObject in your scene
    [MenuItem("Tools/MCP/Create Scene Helper")]
    public static void CreateSceneHelper()
    {
        GameObject helperObj = new GameObject("McpSceneHelper");
        helperObj.AddComponent<McpSceneHelper>();
        UnityEditor.Selection.activeObject = helperObj;
        Debug.Log("MCP Scene Helper created");
    }

    void OnEnable()
    {
        Debug.Log("MCP Scene Helper enabled - ready to handle scene operations");
    }

    // Call this function from your MCP setup
    public void CreateNewScene(string sceneName = "New Scene")
    {
        #if UNITY_EDITOR
        // Create a new scene with default GameObjects
        UnityEditor.SceneManagement.EditorSceneManager.NewScene(
            UnityEditor.SceneManagement.NewSceneSetup.DefaultGameObjects,
            UnityEditor.SceneManagement.NewSceneMode.Single
        );
        
        // Save the scene
        string path = $"Assets/{sceneName}.unity";
        UnityEditor.SceneManagement.EditorSceneManager.SaveScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene(),
            path
        );
        
        Debug.Log($"New scene created and saved as {path}");
        #endif
    }

    // Call this function from your MCP setup
    public Dictionary<string, object> GetSceneInfo()
    {
        var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        
        // Gather basic scene info
        Dictionary<string, object> sceneInfo = new Dictionary<string, object>();
        sceneInfo["name"] = scene.name;
        sceneInfo["path"] = scene.path;
        sceneInfo["rootObjectCount"] = scene.rootCount;
        
        // Get root objects
        var rootObjs = new List<Dictionary<string, object>>();
        foreach (var obj in scene.GetRootGameObjects())
        {
            rootObjs.Add(new Dictionary<string, object> {
                { "name", obj.name },
                { "tag", obj.tag },
                { "activeInHierarchy", obj.activeInHierarchy }
            });
        }
        sceneInfo["rootObjects"] = rootObjs;
        
        return sceneInfo;
    }
}
