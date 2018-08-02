using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;

public class VoiceManager : MonoBehaviour {

    public GameObject controller;
    public GameObject controllerObject;
    public GameObject counter;
    public GameObject textManagerObject;

    private Renderer controllerMesh;
    TextManager textManager;
    OffsetFix offsetFix;
    BoxCounter boxCounter;
    KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    // Use this for initialization
	void Start () {

        textManager = textManagerObject.GetComponent<TextManager>();
        offsetFix = controller.GetComponent<OffsetFix>();
        boxCounter = counter.GetComponent<BoxCounter>();
        controllerMesh = controllerObject.GetComponent<Renderer>();
        controllerMesh.enabled = true;

        keywords.Add("align", () =>
        {
            if(textManager.gameState == GameState.AlignArm)
            {
                offsetFix.AlignAxes();
                textManager.gameState = GameState.ArmAligned;
                controllerMesh.enabled = false;
            }
        });

        keywords.Add("start", () =>
        {
            if(textManager.gameState == GameState.ArmAligned)
            {
                textManager.gameState = GameState.TimerStarted;
            }
        });

        keywords.Add("left", () =>
        {
            if (textManager.gameState == GameState.StartMenu)
            {
                textManager.saidLeft = true;
            }
        });

        keywords.Add("right", () =>
        {
            if (textManager.gameState == GameState.StartMenu)
            {
                textManager.saidRight = true;
            }
        });

        keywords.Add("again", () =>
        {
            if (textManager.gameState == GameState.TimerEnded)
            {
                textManager.saidRestart = true;
            }
        });

        //create the keyword recognizer and tell it what to recognize
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        //register for the OnPhraseRecognized event
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
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
