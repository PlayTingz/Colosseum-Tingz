using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement; // Added this line for EditorSceneManager
using System.Collections.Generic;
using System;
using System.IO;

namespace UnityMcpBridge.Extensions
{
    // This class extends MCP with custom functionality for scene management
    public class CustomMcpExtensions : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Initialize()
        {
            Debug.Log("CustomMcpExtensions initialized");
            // This would be where we register functions if we had direct access to the MCP API
        }

        // Function to create a new scene
        public static void CreateNewScene(string sceneName = "New Scene", string savePath = "Assets/")
        {
            // Save current scene if needed
            if (SceneManager.GetActiveScene().isDirty)
            {
                EditorSceneManager.SaveOpenScenes();
            }
            
            // Create new scene
            Scene newScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
            
            // Save the new scene
            string fullPath = Path.Combine(savePath, sceneName + ".unity");
            EditorSceneManager.SaveScene(newScene, fullPath);
            
            Debug.Log($"New scene created at {fullPath}");
        }

        // Function to get scene setup information
        public static void PrintSceneSetup()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            
            Debug.Log($"Scene Name: {currentScene.name}");
            Debug.Log($"Scene Path: {currentScene.path}");
            Debug.Log($"Build Index: {currentScene.buildIndex}");
            
            GameObject[] rootObjects = currentScene.GetRootGameObjects();
            Debug.Log($"Root Objects: {rootObjects.Length}");
            
            foreach (var obj in rootObjects)
            {
                Debug.Log($"- {obj.name} [{obj.tag}] (Components: {obj.GetComponents<Component>().Length})");
            }
        }
    }
}