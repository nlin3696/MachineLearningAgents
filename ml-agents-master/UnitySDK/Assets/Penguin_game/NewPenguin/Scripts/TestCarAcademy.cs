using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class TestCarAcademy : Academy
{
    private TestCarArea[] penguinAreas;

    public override void AcademyReset()
    {
        // Get the penguin areas
        if (penguinAreas == null)
        {
            penguinAreas = FindObjectsOfType<TestCarArea>();
        }

        // Set up areas
        foreach (TestCarArea penguinArea in penguinAreas)
        {
            penguinArea.feedRadius = resetParameters["coin_radius"];
            penguinArea.ResetArea();
        }
    }
}
