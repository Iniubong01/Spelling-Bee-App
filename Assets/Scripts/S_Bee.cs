using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class S_Bee : MonoBehaviour
{
    public Image objectImage;              // Image to display the current object
    public TMP_InputField inputField;      // Input field where the player types the answer
    public Text feedbackText;              // Text to show "Correct" or "Wrong" message
    public Text userInputText;
    public Text pointText;
    public Text questionNumber;
    public AudioSource audioSource;        // Audio source to play object sounds
    public AudioClip correctSound;         // Sound to play when answer is correct
    public AudioClip wrongSound;           // Sound to play when answer is wrong

    public Sprite[] objectImages;          // Array of object images
    public string[] correctWords;          // Array of correct words for each image
    public AudioClip[] objectSounds;       // Array of sounds for each object

    private int currentQuestionIndex = 0;
    private int point;

    // New UI elements
    public GameObject feedbackUI;          // The parent UI containing correct/wrong answer UI and next button
    public GameObject correctAnswerUI;     // UI that shows when the answer is correct
    public GameObject wrongAnswerUI;       // UI that shows when the answer is wrong
    public Image imageHolder;              // Image holder for displaying the current object image
    public GameObject mainUI;              // Main UI to deactivate when feedback UI is active
    public Button nextButton;              // Reference to the next button

    void Start()
    {
        LoadNextQuestion();

        // Add listener to the Next button
        nextButton.onClick.AddListener(OnNextButtonClicked);
    }

    void Update()
    {
        pointText.text = point.ToString();
        questionNumber.text = (currentQuestionIndex) + "/15";
    }

    // This method is called when a letter button is clicked
    public void OnLetterButtonClicked(string letter)
    {
        inputField.text += letter;  // Append the clicked letter to the input field
    }

    // This method checks the input and provides feedback
    public void CheckAnswer()
    {
        string playerInput = inputField.text.ToUpper();  // Convert input to lowercase
        string correctAnswer = correctWords[currentQuestionIndex].ToUpper();

        // Deactivate the main UI and activate the feedback UI
        mainUI.SetActive(false);
        feedbackUI.SetActive(true);

        if (playerInput == correctAnswer)
        {
            // Show correct UI and hide wrong UI
            correctAnswerUI.SetActive(true);
            wrongAnswerUI.SetActive(false);

            // ************************************
            // Show the correct answer image
            imageHolder.sprite = objectImages[currentQuestionIndex - 1];

             // Show user answer and colour
            userInputText.text = AddSpace(playerInput);
            userInputText.color = Color.green;

            feedbackText.text = AddSpacing(correctAnswer);
            feedbackText.color = Color.white;

           
            audioSource.PlayOneShot(correctSound);
            point += 2;
        }
        else
        {
            // Show wrong UI and hide correct UI
            wrongAnswerUI.SetActive(true);
            correctAnswerUI.SetActive(false);

            imageHolder.sprite = objectImages[currentQuestionIndex - 1];

            // Show user answer and colour
            userInputText.text = AddSpace(playerInput);
            userInputText.color = Color.red;

            // Display the correct answer in the feedback
            feedbackText.text = AddSpacing(correctAnswer);
            feedbackText.color = Color.white;
            audioSource.PlayOneShot(wrongSound);
            point -= 1;
        }
    }

    // This method is called when the Next button is clicked
    public void OnNextButtonClicked()
    {
        // Deactivate the feedback UI and reactivate the main UI
        feedbackUI.SetActive(false);
        mainUI.SetActive(true);

        // Load the next question
       // LoadNextQuestion();
    }

    // This method loads the next image, correct word, and sound
    public void LoadNextQuestion()
    {
        if (currentQuestionIndex < objectImages.Length)
        {
            // Update the object image
            objectImage.sprite = objectImages[currentQuestionIndex];
            
            // Clear input field and feedback text
            inputField.text = "";
            feedbackText.text = "";

            currentQuestionIndex++;
        }
        else
        {
            feedbackText.text = "You've completed all questions!";
        }
    }

    // Method to add spacing between characters
    private string AddSpacing(string input)
    {
        return string.Join(" ", input.ToCharArray());
    }

    private string AddSpace(string input)
    {
       return string.Join("      ", input.ToCharArray()); 
    }

    public void playObjectAudio()
    {
        audioSource.PlayOneShot(objectSounds[currentQuestionIndex]);  // Play the object's sound
    }
}
