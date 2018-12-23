using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlienEnglishDictionaryManager : MonoBehaviour {

    public Dictionary<string, string> AlienEnglishDictionary = new Dictionary<string, string>();

    private void Start()
    {
        DictionaryData myDictionaryData = Resources.Load<DictionaryData>("myDictionaryData");

        if (myDictionaryData != null)
        {
            for (int i = 0; i < myDictionaryData.englishWord.Count; i++)
            {
                AlienEnglishDictionary.Add(myDictionaryData.englishWord[i], myDictionaryData.alienWord[i]);
            }
        }
    }
}
