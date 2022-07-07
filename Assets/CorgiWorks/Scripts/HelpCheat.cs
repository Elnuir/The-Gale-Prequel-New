using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class HelpCheat : CheatBase
{
    public Text Output;
    private string _helpText;

    private void Start()
    {
        var sb = new StringBuilder();
        var cheats = GameObject.FindObjectsOfType<CheatBase>();
        Output.text = "";

        foreach (var c in cheats)
        {
            sb.Append(c.CheatCodeString);
            sb.Append(" - ");
            sb.Append(c.GetType().ToString());
            sb.AppendLine();
        }

        _helpText = sb.ToString();
    }


    protected override void ActivateCheat()
    {
        base.ActivateCheat();
        Output.gameObject.SetActive(true);
        Output.text = _helpText;
    }

    protected override void DeactivateCheat()
    {
        Output.gameObject.SetActive(false);
        base.DeactivateCheat();
    }

}
