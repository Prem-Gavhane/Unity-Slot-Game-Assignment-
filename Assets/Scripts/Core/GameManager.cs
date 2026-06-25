using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public ReelController reel1;
    public ReelController reel2;
    public ReelController reel3;

    public void Spin()
    {
        StartCoroutine(SpinRoutine());
    }

    IEnumerator SpinRoutine()
    {
        reel1.StartSpin();
        reel2.StartSpin();
        reel3.StartSpin();

        yield return new WaitForSeconds(2f);

        SymbolType result1 =
            (SymbolType)Random.Range(0,4);

        SymbolType result2 =
            (SymbolType)Random.Range(0,4);

        SymbolType result3 =
            (SymbolType)Random.Range(0,4);

        reel1.StopSpin(result1);

        yield return new WaitForSeconds(0.3f);

        reel2.StopSpin(result2);

        yield return new WaitForSeconds(0.3f);

        reel3.StopSpin(result3);
    }
}