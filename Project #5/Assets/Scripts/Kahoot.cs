using UnityEngine;

public class Kahoot : MonoBehaviour
{
public GameObject button;
public void check(bool value) 
{
    if (value)
    {
        Debug.Log("Correct answer!");
        button.SetActive(true);
    }
    else
    {
        Debug.Log("Wrong answer!");
        button.SetActive(false);
    }

    }

}
