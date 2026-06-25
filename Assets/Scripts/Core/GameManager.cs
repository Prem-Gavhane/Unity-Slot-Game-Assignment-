using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Reels")]
    public ReelController reel1;
    public ReelController reel2;
    public ReelController reel3;

    [Header("UI")]
    public TMP_Text resultText;

    private bool isSpinning;

    public void Spin()
    {
        if (isSpinning)
            return;

        StartCoroutine(SpinRoutine());
    }

    private IEnumerator SpinRoutine()
    {
        isSpinning = true;

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

        isSpinning = false;
    }

    private void CheckWin(
        SymbolType r1,
        SymbolType r2,
        SymbolType r3)
    {
        if (r1 == r2 && r2 == r3)
        {
            resultText.text = "JACKPOT!";
        }
        else
        {
            resultText.text = "TRY AGAIN";
        }
    }
}