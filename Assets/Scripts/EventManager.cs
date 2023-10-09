using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.InputSystem;

public class EventManager : MonoBehaviour
{
    UnityEvent m_MyEvent;

    void Start()
    {
        if (m_MyEvent == null)
            m_MyEvent = new UnityEvent();

        m_MyEvent.AddListener(Ping);
    }


    void Update()
    {
        if (Keyboard.current.kKey.isPressed && m_MyEvent != null)
        {
            m_MyEvent.Invoke();
        }
    }

    void Ping()
    {
        Debug.Log("Ping");
    }
}
