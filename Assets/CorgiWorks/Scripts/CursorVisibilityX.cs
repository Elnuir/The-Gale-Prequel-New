using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine;

public class CursorVisibilityX : MMCursorVisible
{
    public bool AlwaysVisibleInEditor;

    private void Start()
    {
        if (AlwaysVisibleInEditor && Application.platform == RuntimePlatform.WindowsEditor)
            CursorVisibility = CursorVisibilities.Visible;
    }

    public void ShowCursor()
    {
        if (AlwaysVisibleInEditor && Application.platform == RuntimePlatform.WindowsEditor) return;
        CursorVisibility = CursorVisibilities.Visible;
    }

    public void HideCursor()
    {
        if (AlwaysVisibleInEditor && Application.platform == RuntimePlatform.WindowsEditor) return;
        CursorVisibility = CursorVisibilities.Invisible;
    }
}