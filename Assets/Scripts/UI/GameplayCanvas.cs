using DG.Tweening;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class GameplayCanvas : MonoBehaviour
{
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text waveNumber;
    [SerializeField] private Slider attackCharge;

    private float timeCharge;

    public void InitCanvas(CharacterGameStats stats)
    {
        hpText.text = $"{stats.Hp.GetCalculatedValue()}/{stats.Hp.GetCalculatedValue()}";
        waveNumber.text = "Wave 1";
        timeCharge = 1f / stats.AttackSpeed.GetCalculatedValue();
    }

    public void ChargeAttack()
    {
        attackCharge.value = 0;
        attackCharge.DOValue(1f, timeCharge).SetEase(Ease.Linear);
    }

    public void UpdateHeath(int Maxhp, int currentHp)
    {
        hpText.text = $"{Maxhp}/{currentHp}";
    }

    public void UpdateWave(int wave)
    {
        waveNumber.text = "Wave " + wave;
    }
}