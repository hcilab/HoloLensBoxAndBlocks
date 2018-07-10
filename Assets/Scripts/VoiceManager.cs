using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;

public class VoiceManager : MonoBehaviour {

    public GameObject controller;

    OffsetFix offsetFix;

    KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    // Use this for initialization
	void Start () {



        keywords.Add("align", () =>
        {
            //OffsetFix.AlignAxes();
        });

        //create the keyword recognizer and tell it what to recognize
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        //register for the OnPhraseRecognized event
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        // if the keyword recognized is in our dictionary, call that Action
        if(keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
}
