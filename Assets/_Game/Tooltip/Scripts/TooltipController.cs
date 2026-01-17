using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;

public class TooltipController : MonoBehaviour
{
    [SerializeField] private TooltipView view;

    private PointerEventData pointerData;
    private readonly List<RaycastResult> raycastResults = new();

    private IHoverable currentHover;
    private string currentName;
    private string currentDescription;

    public Action<string, string> OnHoverTextChanged;

    private void Awake()
    {
        pointerData = new PointerEventData(EventSystem.current);

        OnHoverTextChanged += HoverTextChanged;

        view.SetHoverText(string.Empty, string.Empty);
    }

    private void HoverTextChanged(string header, string content)
    {
        view.SetHoverText(header, content);
    }

    private void Update()
    {
        pointerData.position = Input.mousePosition;
        raycastResults.Clear();

        EventSystem.current.RaycastAll(pointerData, raycastResults);

        IHoverable newHover = null;
        for (int i = 0; i < raycastResults.Count; i++)
        {
            newHover = raycastResults[i].gameObject.GetComponentInParent<IHoverable>();
            if (newHover != null)
                break;
        }

        if (currentHover != newHover)
        {
            currentHover?.OnHoverExit();
            currentHover = newHover;
            currentHover?.OnHoverEnter();

            currentName = string.Empty;
            currentDescription = string.Empty;

            if (currentHover != null)
            {
                var itemModel = currentHover.GetItemModel();
                currentName = itemModel.ItemName;
                currentDescription = itemModel.ItemDescription;
            }

            OnHoverTextChanged?.Invoke(currentName, currentDescription);
        }
    }

    private void OnDestroy()
    {
        OnHoverTextChanged -= HoverTextChanged;
    }
}
