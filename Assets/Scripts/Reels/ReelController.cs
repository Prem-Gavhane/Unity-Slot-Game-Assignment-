using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ReelController : MonoBehaviour
{
    public enum ReelState
    {
        Idle,
        Spinning,
        Stopping
    }

    [Header("References")]
    [SerializeField] private RectTransform content;

    [Header("Settings")]
    [SerializeField] private float spinSpeed = 1200f;
    [SerializeField] private float symbolHeight = 44.01f;

    private ReelState currentState = ReelState.Idle;

    private float accumulatedMove;

    private SymbolType targetResult;

    private void Update()
    {
        if (currentState != ReelState.Spinning)
            return;

        float move = spinSpeed * Time.deltaTime;

        content.anchoredPosition -= new Vector2(0, move);

        accumulatedMove += move;

        if (accumulatedMove >= symbolHeight)
        {
            accumulatedMove -= symbolHeight;

            RecycleSymbol();
        }
    }

    public void StartSpin()
    {
        accumulatedMove = 0;
        currentState = ReelState.Spinning;
    }

    public void StopSpin(SymbolType result)
    {
        targetResult = result;

        currentState = ReelState.Stopping;

        AlignToResult();
    }

    private void RecycleSymbol()
    {
        Transform first = content.GetChild(0);

        first.SetAsLastSibling();

        LayoutRebuilder.ForceRebuildLayoutImmediate(content);
    }

    private void AlignToResult()
    {
        ReelSymbol targetSymbol = null;

        float closestDistance = float.MaxValue;

        foreach (Transform child in content)
        {
            ReelSymbol reelSymbol =
                child.GetComponent<ReelSymbol>();

            if (reelSymbol == null)
                continue;

            if (reelSymbol.symbolType != targetResult)
                continue;

            RectTransform rect =
                child.GetComponent<RectTransform>();

            float distance =
                Mathf.Abs(rect.anchoredPosition.y);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                targetSymbol = reelSymbol;
            }
        }

        if (targetSymbol == null)
        {
            currentState = ReelState.Idle;
            return;
        }

        RectTransform targetRect =
            targetSymbol.GetComponent<RectTransform>();

        float finalY =
            content.anchoredPosition.y
            - targetRect.anchoredPosition.y;

        content.DOAnchorPosY(finalY, 0.5f)
            .SetEase(Ease.OutExpo)
            .OnComplete(() =>
            {
                currentState = ReelState.Idle;
            });
    }

    public ReelState GetState()
    {
        return currentState;
    }
}