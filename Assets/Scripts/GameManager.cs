using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Character[] characters;

    private Character currentCharacter;

    void Awake()
    {
        currentCharacter = characters[0];
    }

    void Start()
    {
        foreach(Character character in characters)
            character.Hide();
        currentCharacter.Show();
    }

    private void OnSelectFirstCharacter()
    {
        ChangeCharacter(0);
    }

    private void OnSelectSecondCharacter()
    {
        ChangeCharacter(1);
    }

    private void OnSelectThirdCharacter()
    {
        ChangeCharacter(2);
    }
    private void OnSelectFourthCharacter()
    {
        ChangeCharacter(3);
    }

    private void ChangeCharacter(int characterIndex)
    {
        if(currentCharacter == characters[characterIndex])
            return;
        currentCharacter.Hide();
        characters[characterIndex].Show();
        currentCharacter = characters[characterIndex];
    }
}
