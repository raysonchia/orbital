using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tooltip : Singleton<Tooltip>
{
    [SerializeField]
    private RectTransform canvasRectTransform;
    private TextMeshProUGUI tooltipText;
    private RectTransform backgroundRectTransform;
    private RectTransform rectTransform;
    private float showTimer;

    protected override void Awake()
    {
        base.Awake();
        backgroundRectTransform = transform.Find("toolTipBackground").GetComponent<RectTransform>();
        tooltipText = transform.Find("toolTipText").GetComponent<TextMeshProUGUI>();
        rectTransform = transform.GetComponent<RectTransform>();
        Hide();
    }

    private void Update()
    {
        Vector2 anchoredPostion = Input.mousePosition / canvasRectTransform.localScale.x;

        if (anchoredPostion.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width)
        {
            anchoredPostion.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
        }

        if (anchoredPostion.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height)
        {
            anchoredPostion.y = canvasRectTransform.rect.height - backgroundRectTransform.rect.height;
        }

        rectTransform.anchoredPosition = anchoredPostion;

        showTimer -= Time.deltaTime;
        if (showTimer <= 0f)
        {
            Hide();
        }
        
    }

    public void ShowToolTip(string toolTipString)
    {
        gameObject.SetActive(true);
        tooltipText.SetText(toolTipString);
        tooltipText.ForceMeshUpdate();

        Vector2 textSize = tooltipText.GetRenderedValues(false) / 3;
        Vector2 paddingSize = new Vector2(24, 24);
        backgroundRectTransform.sizeDelta = textSize + paddingSize;

        showTimer = 2f;
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
