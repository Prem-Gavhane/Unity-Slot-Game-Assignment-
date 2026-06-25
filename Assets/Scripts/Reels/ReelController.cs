using System.Collections;
using UnityEngine;

public class ReelController : MonoBehaviour
{
    [Header("References")]
    public RectTransform content;

    [Header("Settings")]
    public float spinSpeed = 1200f;
    public float symbolHeight = 85f;

    private bool isSpinning;

    private void Update()
    {
        if (!isSpinning)
            return;

        content.anchoredPosition += Vector2.down * spinSpeed * Time.deltaTime;

        RecycleSymbols();
    }

    private void RecycleSymbols()
    {
        if (content.childCount == 0)
            return;

        RectTransform firstSymbol =
            content.GetChild(0).GetComponent<RectTransform>();

        float worldY =
            firstSymbol.anchoredPosition.y +
            content.anchoredPosition.y;

        if (worldY < -symbolHeight)
        {
            firstSymbol.SetAsLastSibling();

            content.anchoredPosition +=
                new Vector2(0, symbolHeight);
        }
    }

    public void StartSpin()
    {
        isSpinning = true;
    }

    public void StopAtSymbol(SymbolType targetSymbol)
    {
        StartCoroutine(StopRoutine(targetSymbol));
    }

    private IEnumerator StopRoutine(SymbolType targetSymbol)
    {
        isSpinning = false;

        yield return new WaitForSeconds(0.05f);

        int targetIndex = -1;

        for (int i = 1; i < content.childCount - 1; i++)
        {
            ReelSymbol symbol =
                content.GetChild(i).GetComponent<ReelSymbol>();

            if (symbol.symbolType == targetSymbol)
            {
                targetIndex = i;
                break;
            }
        }

        if (targetIndex == -1)
            yield break;

        float targetY = targetIndex * symbolHeight;

        content.anchoredPosition =
            new Vector2(
                content.anchoredPosition.x,
                targetY
            );
    }

    public SymbolType GetMiddleSymbol()
    {
        int middleIndex = 1;

        ReelSymbol symbol =
            content.GetChild(middleIndex)
            .GetComponent<ReelSymbol>();

        return symbol.symbolType;
    }
}