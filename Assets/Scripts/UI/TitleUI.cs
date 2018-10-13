using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : UIController {

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            GameManager.Instance.ChangeState(GameState.Playing);
        }
    }
}
