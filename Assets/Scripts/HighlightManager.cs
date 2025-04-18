using UnityEngine;
using UnityEngine.EventSystems;

public class HighlightManager : MonoBehaviour
{
    private Transform highlightedObj;
    private Transform selectedObj;
    public LayerMask selectableLayer;
    
    private Outline highlightOutline;
    private RaycastHit hit;

    void Update()
    {
        HoverHighlight();
    }

    public void HoverHighlight()
    {
        if (highlightedObj != null)
        {
            highlightOutline.enabled = false;
            highlightedObj = null;
        }
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out hit, selectableLayer))
        {
            highlightedObj = hit.transform;
            
            if (highlightedObj.CompareTag("Enemy") && highlightedObj != selectedObj)
            {
                highlightOutline = highlightedObj.GetComponent<Outline>();
                highlightOutline.enabled = true;
            }
            else
            {
                highlightedObj = null;
            }
        }
    }

    public void SelectedHighlight()
    {
        if (highlightedObj.CompareTag("Enemy"))
        {
            if (selectedObj != null)
            {
                selectedObj.GetComponent<Outline>().enabled = false;
            }
            
            selectedObj = hit.transform;
            selectedObj.GetComponent<Outline>().enabled = true;
            
            highlightOutline.enabled = true;
            highlightedObj = null;
        }
    }

    public void DeselectHighlight()
    {
        selectedObj.GetComponent<Outline>().enabled = false;
        selectedObj = null;
    }
}
