using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LogController : MonoBehaviour
{
    public GameObject newLogMessage;

    List<LogMessage> logMessages = new List<LogMessage>();

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(300, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
        for (int i = 0; i < logMessages.Count;i++)
        {
            logMessages[i].msg.GetComponent<RectTransform>().localPosition = new Vector3(-300, GetComponent<RectTransform>().sizeDelta.y - 100*(i+1),0);

            if (logMessages[i].time + 10 < Time.time)
            {
                GetComponent<RectTransform>().sizeDelta -= new Vector2(0, 100);
                logMessages[i].msg.SetActive(false);
            }
        }

        while (CheckRemove()) ;
    }
    public bool CheckRemove()
    {
        for (int i = 0; i < logMessages.Count; i++)
        {
            if (!logMessages[i].msg.activeSelf)
            {
                logMessages.RemoveAt(i);
                return true;
            }
        }
        return false;
    }
    public void AddLogMessage(string message, bool succeed)
    {
        GetComponent<RectTransform>().sizeDelta += new Vector2(0, 100);

        LogMessage newLog = new LogMessage();
        newLog.msg = GameObject.Instantiate(newLogMessage, transform);
        
        newLog.msg.GetComponent<TextMeshProUGUI>().outlineColor = succeed ? Color.green : Color.red;
        newLog.time = Time.time;
        logMessages.Add(newLog);
    }

    public struct LogMessage
    {
        public float time;
        public GameObject msg;
    }
}
