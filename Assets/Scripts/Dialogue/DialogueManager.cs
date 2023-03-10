using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Ink.Runtime;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSource))]
public class DialogueManager : MonoBehaviour
{
    private PlayerMove m_PlayerInput;

    [Header("Parameters")]
    [SerializeField] private float typingSpeed = 0.04f;

    [Header("Load Globals JSON")]
    [SerializeField] private TextAsset loadGloblasJSON;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [Space]
    [SerializeField] private TextMeshProUGUI dialogueNameText;
    [SerializeField] private PortraitSetter portraitSetter;
    private Animator layoutAnimator;
    [Space]
    [SerializeField] private GameObject btnContinue;
    [SerializeField] private GameObject continueIcon;
    [SerializeField] private bool canContinue;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    [Header("Audio")]
    [SerializeField] private DialogueAudioInfoSO defaultAudioInfo;
    [SerializeField] private VoicesLibrary voices;
    [SerializeField] private bool makePreditable;
    private DialogueAudioInfoSO currentAudioInfo;
    private AudioSource audioSource;

    private static DialogueManager instance;
    private Story currentStory;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";
    private const string AUDIO_TAG = "audio";

    private DialogueVariables dialogueVariables;

    //Hide
    public bool dialogueIsPlaying { get; private set; }
    private bool canContinueToNextLine = false;
    private Coroutine displayLineCoroutune;
    private Touch myTouch;
    private CameraPanNPinch CameraPanNPinch;

    private void Awake()
    {
        m_PlayerInput = new PlayerMove();

        if (instance != null)
            Debug.LogError("Found more than one Dialogue Manager in the scene");

        instance = this;
        dialogueVariables = new DialogueVariables(loadGloblasJSON);

        //Choices
        choicesText = new TextMeshProUGUI[choices.Length];
        int idx = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[idx] = choice.GetComponentInChildren<TextMeshProUGUI>();
            idx++;
        }

        layoutAnimator = dialoguePanel.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        currentAudioInfo = defaultAudioInfo;
        CameraPanNPinch = FindObjectOfType<CameraPanNPinch>();
    }

    private void Start()
    {
        ExitDialogueMode();
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }


    private void SetCurrentAudioInfo(string id)
    {
        DialogueAudioInfoSO targetVoice = voices.voices.Find(voice => voice.id == id);

        if (!targetVoice)
        {
			Debug.LogError("Failed to find audio info for id: " + id);
            return;
        }

        currentAudioInfo = targetVoice;
       
    }

    public void SetDialogue(TextAsset inkJSON)
    {
		currentStory = new Story(inkJSON.text);
		dialogueVariables.StartListening(currentStory);
		//Reset Dialogue Panel
		dialogueNameText.text = "No name";
		portraitSetter.SetPortrait("incognito");
		//layoutAnimator.Play("right");
	}

    public void StartDialogue()
    {
		dialogueIsPlaying = true;
		dialoguePanel.SetActive(true);
		ContinueStory();
        CameraPanNPinch.canMove = false;
	}

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        SetDialogue(inkJSON);
        StartDialogue();
        ContinueStory();
    }

    public void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        CameraPanNPinch.canMove = true;
        canContinue = false;
    }

    public void ContinueStory()
    {       
        if (currentStory.canContinue)
        {
            //Set text for the current dialogue line
            if (displayLineCoroutune != null)
                StopCoroutine(displayLineCoroutune);

            string nextLine = currentStory.Continue();
            HandleTags(currentStory.currentTags);
            displayLineCoroutune = StartCoroutine(DisplayLine(nextLine));

            btnContinue.GetComponentInChildren<TextMeshProUGUI>().text = "Continue";
            if (!currentStory.canContinue)
                btnContinue.GetComponentInChildren<TextMeshProUGUI>().text = "Close";
        }
        else
        {
            ExitDialogueMode();
            dialogueVariables.StopListening(currentStory);
            SetCurrentAudioInfo(defaultAudioInfo.id);
        }
    }

	private IEnumerator DisplayLine(string line)
    {
        //Empty Dialogue text
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;

        //Hide items
        continueIcon.SetActive(false);
        btnContinue.SetActive(false);
        canContinue = false;
        HideChoices();

        canContinueToNextLine = false;

        bool isAddingRichTextTag = false;

        //Display each letter one at a time
        foreach (char letter in line.ToCharArray())
        {
            //Check for rich text tag, if found add it without waiting
            if(letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;
                //dialogueText.text += letter;

                if (letter == '>')
                    isAddingRichTextTag = false;
            }
            //If not rich text, 
            else
            {
                PlayDialogueSound(dialogueText.maxVisibleCharacters, dialogueText.text[dialogueText.maxVisibleCharacters]);
                dialogueText.maxVisibleCharacters++;
                yield return new WaitForSeconds(typingSpeed);
            }
            
        }

        //After the entire line has finished displayins
        continueIcon.SetActive(true);
        btnContinue.SetActive(true);
        canContinue = true;
        DisplayChoices();

        canContinueToNextLine = true;
    }    

    private void PlayDialogueSound(int currentDisplayedCharacterCount, char currentCharacter)
    {
        //Set variables for the beloww based on our config
        AudioClip[] dialogueTypingSoundClips = currentAudioInfo.dialogueTypingSoundClips;
        int frequencyLevel = currentAudioInfo.frequencyLevel;
        float minPitch = currentAudioInfo.minPitch;
        float maxPitch = currentAudioInfo.maxPitch;
        bool stopAudioSource = currentAudioInfo.stopAudioSource;

        //Play the sound
        if(currentDisplayedCharacterCount % frequencyLevel == 0)
        {
            if (stopAudioSource)
            {
                audioSource.Stop();
            }

            AudioClip soundclip = null;
            //Create preditable audio from hashing
            if (makePreditable)
            {
                int hashCode = currentCharacter.GetHashCode();
                
                //Sound clip
                int preditableIdx = hashCode % dialogueTypingSoundClips.Length;
                soundclip = dialogueTypingSoundClips[preditableIdx];
                
                //Pitch
                int minPitchInt = (int)(minPitch * 100);
                int maxPitchInt = (int)(maxPitch * 100);
                int pitchRangeInt = maxPitchInt - minPitchInt;
                
                //Cannot divide by 0
                if(pitchRangeInt != 0)
                {
                    int predictablePitchInt = (hashCode % pitchRangeInt) + minPitchInt;
                    float predictablePitch = predictablePitchInt / 100f;
                    audioSource.pitch = predictablePitch;
                }
                else
                {
                    audioSource.pitch = minPitch;
                }
            }
            //otherwise, random the audio
            else
            {
                //Sound clip
                int randomIdx = Random.Range(0, dialogueTypingSoundClips.Length);
                soundclip = dialogueTypingSoundClips[randomIdx];

                //Random pitch
                audioSource.pitch = Random.Range(minPitch, maxPitch);
            }           

            //Play sound
            audioSource.PlayOneShot(soundclip);
        }
    }

    private void HideChoices()
    {
        foreach (GameObject choicesButton in choices)
        {
            choicesButton.SetActive(false); 
        }
    }
    
    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
            Debug.LogError("More choices were given than the UI can Support. Number of choices given: " + currentChoices.Count);

        int idx = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[idx].gameObject.SetActive(true);
            choicesText[idx].text = choice.text;
            idx++;
        }

        for (int i = idx; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIdx)
    {
        currentStory.ChooseChoiceIndex(choiceIdx);
        ContinueStory();
    }

    private void HandleTags(List<string> currentTags)
    {
        //Loop through each tag and handle it accordingly
        foreach (string tag in currentTags)
        {
            //Parse the tag
            string[] splitTag = tag.Split(':');
            if(splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be appropriately parse: " + tag);
            }    
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            //Handle the tag
            switch (tagKey)
            {
                case SPEAKER_TAG:
                    dialogueNameText.text = tagValue;
                    break;
                case PORTRAIT_TAG:
					portraitSetter.SetPortrait(tagValue);
                    break;
                case LAYOUT_TAG:
                    layoutAnimator.Rebind();
                    layoutAnimator.Play(tagValue);
                    break;
                case AUDIO_TAG:
                    SetCurrentAudioInfo(tagValue);
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                    break;
            }
        }
    }

    public Ink.Runtime.Object GetVariable(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        
        if(variableValue != null)
        {
            Debug.LogError("Ink Variable was found to be null: " + variableName);
        }

        return variableValue;
    }

	public void InvokeStoryFunction(string functionName, object value)
	{
        currentStory.EvaluateFunction(functionName, value);
	}

    //This method will called anytime the aplication exits.
    public void OnApplicationQuit()
    {
        if(dialogueVariables != null)
            dialogueVariables.SaveVariables();
    }

    private void FixedUpdate()
    {
        if(canContinue)
        {
            if (m_PlayerInput.CameraMove.ContinueDialogues.IsPressed())
                ContinueStory();
        }
    }

    #region Input Enable / Disable
    private void OnEnable()
    {
        m_PlayerInput.Enable();
    }
    private void OnDisable()
    {
        m_PlayerInput.Disable();
    }
    #endregion
}
