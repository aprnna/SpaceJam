using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RouletteUIController : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform content;
    public float scrollSpeed = 100f;
    public Button stopButton;
    public int stopAtIndex = 3;

    private bool isScrolling = true;
    private float itemWidth;
    private int itemCount;
    public GameObject itemPrefab;

    void Start()
    {

        for (var i = 8; i < 15; i++)
        {
            GameObject gameObject = Instantiate(itemPrefab, content);
            TextMeshProUGUI text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
            text.text = i.ToString();
        }
        itemCount = content.childCount;
        itemWidth = ((RectTransform)content.GetChild(0)).rect.width + 1;
        stopButton.onClick.AddListener(StopScrolling);
    }

    void Update()
    {
        if (!isScrolling) return;

        // Move content to the left
        content.anchoredPosition += Vector2.left * scrollSpeed * Time.deltaTime;

        // Check if first item is out of view and move it to the end
        RectTransform firstItem = content.GetChild(0) as RectTransform;
        float leftEdge = content.anchoredPosition.x + firstItem.anchoredPosition.x + itemWidth;

        if (leftEdge < 0)
        {
            firstItem.SetAsLastSibling();
            content.anchoredPosition += new Vector2(itemWidth, 0);
        }
    }

    void StopScrolling()
    {
        isScrolling = false;
        StartCoroutine(SnapToIndex(stopAtIndex));
    }

    IEnumerator SnapToIndex(int index)
    {
        // Recalculate index based on current order
        RectTransform targetItem = content.GetChild(index) as RectTransform;
        Debug.Log(targetItem.gameObject.GetComponentInChildren<TextMeshProUGUI>().text);
        // Calculate target position to bring item to front (or center)
        float targetX = -targetItem.anchoredPosition.x;
        float duration = 2f;
        float elapsed = 0f;
        float startX = content.anchoredPosition.x;

        while (elapsed < duration)
        {
            float newX = Mathf.Lerp(startX, targetX, elapsed / duration);
            content.anchoredPosition = new Vector2(newX, content.anchoredPosition.y);
            elapsed += Time.deltaTime;
            yield return null;
        }

        content.anchoredPosition = new Vector2(targetX, content.anchoredPosition.y);
    }
}
