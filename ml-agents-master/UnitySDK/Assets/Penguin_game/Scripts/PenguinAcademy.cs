using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents; //need this to inheirt

public class PenguinAcademy : Academy 
{

    private PenguinArea[] penguinAreas;

    public override void AcademyReset()
    {
        if (penguinAreas == null)
        {
            penguinAreas = FindObjectsOfType<PenguinArea>();
        }

        //Set up areas
        foreach (PenguinArea penguinArea in penguinAreas)
        {
            penguinArea.fishSpeed = resetParameters["fish_speed"];
            penguinArea.feedRadius = resetParameters["feed_radius"];
            penguinArea.ResetArea();
        }
    }
}
