using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIController : MonoBehaviour
{
    protected virtual void Awake()
    {
        GameManager.Instance.CurrentUI = this;
    }
}
