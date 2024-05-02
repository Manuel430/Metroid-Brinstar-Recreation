using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSamus : MonoBehaviour
{
    [SerializeField] SamusScript samus;

    public void TurnCutsceneOn()
    {
        samus.SetCutscene(true);
    }
    
    public void TurnCutsceneOff()
    {
        samus.SetCutscene(false);
        samus.ResetMovement();
    }
}
