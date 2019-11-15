using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class CarAcademy : Academy
{
    
    private CarArea[] carAreas;

    public override void AcademyReset()
    {
        // Get the penguin areas
        if (carAreas == null)
        {
            carAreas = FindObjectsOfType<CarArea>();
        }

        // Set up areas
        foreach (CarArea carArea in carAreas)
        {
            carArea.coinRadius = resetParameters["coin_radius"];
            carArea.ResetArea();
        }
    }
}
