using System;
using UnityEngine;
using UnityEditor;

[Serializable]
public class TexelDensity : EditorWindow
{
    public bool initialized = false;

    public bool mode = false;
    public Shader showTexelsShader;

    [MenuItem("UTools/Texel Density")]
    public static void ShowWindow()
    {
        var window = EditorWindow.GetWindow(typeof(TexelDensity)) as TexelDensity;
        window.Init();
        window.Show();
    }

    public void Init()
    {
        if (initialized)
            return;

        initialized = true;

        minSize = new Vector2(200, 200);

        showTexelsShader = Shader.Find("UTools/TexelDensity");
    }

    public void OnGUI()
    {
        EditorGUILayout.HelpBox("UTools texel density viewer visualize texel density in your scene. Maintain even density to elevating your game visual fidelity and minimizing frame stagger.", MessageType.Info);
        EditorGUILayout.Space();

        GUILayout.Space(3);

        if (GUILayout.Button(mode ? "Deactivate" : "Activate"))
        {
            mode = !mode;
            SetCameraMode(mode);
        }
    }

    public void SetCameraMode(bool showTexel)
    {
        if (showTexel && showTexelsShader != null && showTexelsShader.isSupported)
        {
            SceneView.lastActiveSceneView.SetSceneViewShaderReplace(showTexelsShader, "");
            SceneView.lastActiveSceneView.Repaint();
        }
        else
        {
            SceneView.lastActiveSceneView.SetSceneViewShaderReplace(null, "");
            SceneView.lastActiveSceneView.Repaint();
        }
    }
}
