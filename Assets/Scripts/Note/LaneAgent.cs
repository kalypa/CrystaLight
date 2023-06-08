using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class LaneAgent : Agent
{
    public Lane lane;
    public override void Initialize()
    {

    }
    public override void OnEpisodeBegin()
    {

    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation((float)lane.aiTime);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        if (actions.DiscreteActions[0] == 1)
        {
            lane.AINoteInput(1);
        }
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {

    }
}
