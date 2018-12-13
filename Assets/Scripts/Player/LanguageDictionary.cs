using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageDictionary : MonoBehaviour {

    public Dictionary<string, string> AlienEnglishDictionary = new Dictionary<string, string>();

	public void Initialize() {
        AlienEnglishDictionary.Add("Prism", "/|7");
        AlienEnglishDictionary.Add("Donut", "+-+");
    }
}
