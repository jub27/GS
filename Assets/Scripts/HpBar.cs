using UnityEngine;
using UnityEngine.UI;

public class HpBar : Singleton<HpBar>
{
    [SerializeField] private Image barImage;
    private CharacterStatData characterStatData;
    public void SetCharacterStatData(CharacterStatData characterStatData)
    {
        this.characterStatData = characterStatData;
        characterStatData.OnCurHpChanged += UpdateBar;
        characterStatData.OnCurHpChanged += UpdateBar;
    }

    private void UpdateBar()
    {
        barImage.fillAmount = (float)characterStatData.CurHp / characterStatData.MaxHp;
    }
}