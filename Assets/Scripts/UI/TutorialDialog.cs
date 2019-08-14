using RTools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDialog : PopupDialog
{
    public Action onClose;

    public override void Hide()
    {
        base.Hide();
        onClose?.Invoke();
    }
}
