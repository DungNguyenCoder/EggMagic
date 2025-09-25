using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{
    public void OnClickBtn()
    {
        EventManager.onclick?.Invoke(5);
    }
}
