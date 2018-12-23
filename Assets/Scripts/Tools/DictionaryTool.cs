using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DictionaryTool : EditorWindow {

    AlienEnglishDictionaryManager alienEnglishDictionaryManager;
    public List<string> englishWord = new List<string>();
    public List<string> alienWord = new List<string>();
    public int wordIndex;

    protected void OnEnable()
    {
        DictionaryData myDictionaryData = Resources.Load<DictionaryData>("myDictionaryData");

        if (myDictionaryData != null)
        {
            englishWord = myDictionaryData.englishWord;
            alienWord = myDictionaryData.alienWord;          
        }
    }

    protected void OnDisable()
    {
        SaveDictionaryData();
    }

    [MenuItem("Tools/Dictionary Tool")]
    public static void ShowWindow()
    {
        GetWindow<DictionaryTool>("Dictionary Tool");
    }

    private void OnGUI()
    {
        GUILayout.Label("AGTFC Dictionary", EditorStyles.boldLabel);

        for (wordIndex = 0; wordIndex < englishWord.Count; wordIndex++)
        {
            GUILayout.Label("Words", EditorStyles.boldLabel);
            englishWord[wordIndex] = EditorGUILayout.TextField("English", englishWord[wordIndex]);
            alienWord[wordIndex] = EditorGUILayout.TextField("Alien", alienWord[wordIndex]);
            GUILayout.Space(2);
        }

        GUILayout.Space(2);

        if (GUILayout.Button("Add Row"))
        {
            englishWord.Add("");
            alienWord.Add("");
        }

        if (GUILayout.Button("Remove Row"))
        {
            if (wordIndex > 0)
            {
                englishWord.Remove(englishWord[wordIndex - 1]);
                alienWord.Remove(alienWord[wordIndex - 1]);
                wordIndex--;
            }
        }

        if (GUILayout.Button("Submit"))
        {
           SaveDictionaryData();

           Debug.Log("submit btn pressed"); 
        }

        GUILayout.Space(15);

        if (GUILayout.Button("Clear Entire Dictionary"))
        {
            englishWord.Clear();
            alienWord.Clear();
            wordIndex = 0;
        }
    }

    void SaveDictionaryData()
    {
        DictionaryData myDictionaryData = Resources.Load<DictionaryData>("myDictionaryData");

        myDictionaryData = CreateInstance<DictionaryData>();
        myDictionaryData.englishWord = englishWord;
        myDictionaryData.alienWord = alienWord;

        AssetDatabase.CreateAsset(myDictionaryData, "Assets/Resources/myDictionaryData.asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
