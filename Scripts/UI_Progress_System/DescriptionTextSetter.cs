using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DescriptionTextSetter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textField;
    // Start is called before the first frame update
    public void SetText(string text)
    {
        if (text == string.Empty) { gameObject.SetActive(false); return; }
        gameObject.SetActive(true);
        textField.text = text;
    }
}
