using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Reels")]
    public ReelController reel1;
    public ReelController reel2;
    public ReelController reel3;

    public HandleController handleController;

    [Header("UI")]
    [Header("UI")]
    public TMP_Text resultText;
    public TMP_Text rewardText;

    [Header("Balance System")]
    public TMP_Text balanceText;

    public int balance = 0;
    public int spinCost = 50;


    private bool isSpinning;


    void Start()
    {
        UpdateUI();
    }
    public void Spin()
    {
                SoundManager.Instance.Play("Click");

        if (isSpinning)
            return;

        if (balance < spinCost)
        {
            resultText.text = "NOT ENOUGH COINS!";
            return;
        }

        balance -= spinCost;

        UpdateUI();

        StartCoroutine(SpinRoutine());
    }

    private IEnumerator SpinRoutine()
    {
        
        SoundManager.Instance.StopAll();
        
        SoundManager.Instance.Play("Spin");
        isSpinning = true;

        handleController.PlayHandle();

        resultText.text = "";

        reel1.StartSpin();
        reel2.StartSpin();
        reel3.StartSpin();

        yield return new WaitForSeconds(2f);

        SymbolType result1 =
            (SymbolType)Random.Range(0, System.Enum.GetValues(typeof(SymbolType)).Length);

        SymbolType result2 =
            (SymbolType)Random.Range(0, System.Enum.GetValues(typeof(SymbolType)).Length);

        SymbolType result3 =
            (SymbolType)Random.Range(0, System.Enum.GetValues(typeof(SymbolType)).Length);

        reel1.StopAtSymbol(result1);

        yield return new WaitForSeconds(0.5f);

        reel2.StopAtSymbol(result2);

        yield return new WaitForSeconds(0.5f);

        reel3.StopAtSymbol(result3);

        yield return new WaitForSeconds(0.5f);

        CheckWin(result1, result2, result3);

        handleController.StopHandle();

        isSpinning = false;
    }

    private int GetSymbolValue(SymbolType symbol)
    {
        switch (symbol)
        {
            case SymbolType.Seven:
                return 1000;

            case SymbolType.Bar:
                return 500;

            case SymbolType.Bell:
                return 250;

            case SymbolType.Cherry:
                return 100;

            default:
                return 0;
        }
    }

    private void CheckWin(
       SymbolType r1,
       SymbolType r2,
       SymbolType r3)
    {
        if (r1 == r2 && r2 == r3)
        {
        SoundManager.Instance.Play("Win");

            int reward = GetSymbolValue(r1);

            balance += reward;

            resultText.text = "JACKPOT!";
            rewardText.text = "Rs:" + reward;

            UpdateUI();
        }
        else
        {
            resultText.text = "TRY AGAIN";
            rewardText.text = "Rs:0";
        }
    }

    private void UpdateUI()
    {
        balanceText.text = balance.ToString();
    }
}