using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DescriptionBlock : MonoBehaviour
{
    [SerializeField] DescriptionTextSetter mainDescriptionBlock, adDescriptionBlock;
    [SerializeField] GameObject alterDescriptionPrefab;
    // Start is called before the first frame update
   
    public void UpdateTextFields(Task step)
    {
        mainDescriptionBlock.SetText(step.GetDescription());
        string[] actionsDescriptions = GetAlterDescriptions(step);
        for(int i = 0; i < actionsDescriptions.Length; i++)
        {
            GameObject newAdPanel = Instantiate(alterDescriptionPrefab, transform);
            DescriptionTextSetter adTextSetter = newAdPanel.GetComponent<DescriptionTextSetter>();
            adTextSetter.SetText(actionsDescriptions[i]);
            newAdPanel.SetActive(true);
        }
    }

    public void UpdateTextOnExistingPanels(string mainText, string adText)
    {
        mainDescriptionBlock.SetText(mainText);
        adDescriptionBlock.SetText(adText);
    }

    string[] GetAlterDescriptions(Task task)
    {
        string[] alterDescs = task.GetActionsDescriptions();
        return alterDescs;
    }

}
