//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Playables;

//[RequireComponent(typeof(PlayableDirector), typeof(Course))]
//public class TimelineSceneSetter : MonoBehaviour, ISceneStateSetter
//{
//    [SerializeField] private PlayableDirector dir;
//    [SerializeField] private Course course;

//    private void Start()
//    {
//        LinkWithCourse();
//    }
//    // Start is called before the first frame update
//    public void SetSceneState()
//    {
//        dir.Play();
//    }

//    public void LinkWithCourse()
//    {
//        dir = GetComponent<PlayableDirector>();
//        course = GetComponent<Course>();
//        course._courseStartedEvent.AddListener(SetSceneState);
//    }
//}
