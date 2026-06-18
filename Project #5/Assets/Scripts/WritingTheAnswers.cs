using UnityEngine;
using UnityEngine.UI;

public class WritingTheAnswers : MonoBehaviour
{
    public string answer;
    public Button yourbutton;
    public GameObject NextButton;
    public TMPro.TMP_InputField inputField;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Button button = yourbutton.GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void TaskOnClick()
    {
        string guess = this.inputField.text;
        string guess1 = guess.ToLower();

        if (guess1 == answer)
        {
            this.inputField.text = "Correct!";
            NextButton.SetActive(true);
        }
        else
        {
            this.inputField.text = "Try Again!";
            NextButton.SetActive(false);
        }
    }
}
