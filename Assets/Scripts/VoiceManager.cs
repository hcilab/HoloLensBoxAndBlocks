using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;

public class VoiceManager : MonoBehaviour {


    KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    // Use this for initialization
	void Start () {
        keywords.Add("align", () =>
        {
            //OffsetFix.AlignAxes();
        });

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
