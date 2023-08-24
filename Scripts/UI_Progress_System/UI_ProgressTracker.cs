using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ProgressTracker : MonoBehaviour
{
    [SerializeField] private TasksHub hub;
    [SerializeField] GameObject descriptionBlockPrefab;
    private Dictionary<Task, GameObject> panelsMap = new Dictionary<Task, GameObject>();

    [SerializeField] GameObject standbyScreen, courseScreen;

    Canvas canvas;

    [SerializeField] private string defaultText = "Для выбора курса перейдите в меню", defaultAdText = string.Empty;
    // Start is called before the first frame update

    private void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        hub.activeTaskAdded.AddListener(ShowActualDescription);
        hub.activeTaskRemoved.AddListener(HideObsoleteDescription);
        hub.activaTaskListChanged.AddListener(CheckIfCourseIsActive);
        CheckIfCourseIsActive();
    }

    void ShowMainScreen()
    {
        standbyScreen.SetActive(false);
        courseScreen.SetActive(true);
    }

    void ShowStandbyScreen(string mainText, string adText)
    {
        courseScreen.SetActive(false);
        standbyScreen.SetActive(true);
        standbyScreen.transform.GetChild(0).GetComponent<DescriptionBlock>().UpdateTextOnExistingPanels(mainText, adText);
    }
    void CheckIfCourseIsActive()
    {
        if(hub.activeTasks.Count == 0)
        {
            ShowStandbyScreen(defaultText, defaultAdText);
        }
        else
        {
            ShowMainScreen();
        }
    }

    public void HideTaskbar()
    {
        canvas.enabled = false;
    }

    public void ShowTaskbar()
    {
        canvas.enabled = true;
    }

    private void OnDestroy()
    {
        hub.activeTaskAdded.RemoveListener(ShowActualDescription);
        hub.activeTaskRemoved.RemoveListener(HideObsoleteDescription);
        hub.activaTaskListChanged.RemoveListener(CheckIfCourseIsActive);
    }

    public void ShowActualDescription(Task step)
    {
        GameObject newPanel = Instantiate(descriptionBlockPrefab, transform);
        DescriptionBlock blockHandler = newPanel.GetComponent<DescriptionBlock>();
        blockHandler.UpdateTextFields(step);
        panelsMap.Add(step, newPanel);
        newPanel.SetActive(true);
    }

    public void HideObsoleteDescription(Task step)
    {
        if (!panelsMap.ContainsKey(step)) return; 
        GameObject panel = panelsMap[step];
        panelsMap.Remove(step);
        Destroy(panel);
    }
}
