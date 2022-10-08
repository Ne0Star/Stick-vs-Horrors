using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TrajectorySystem : MonoBehaviour
{
    public int currentCount = 0;
    public Allocator allocator = Allocator.Persistent;
    public TrajectoryDealer[] trajectories;

    public bool AllowBuild()
    {
        bool result = true;
        foreach (TrajectoryDealer dealer in trajectories)
        {
            if (!dealer.AllowBuild())
            {
                result = false;
            }
        }
        return result;
    }

    private void Awake()
    {
        trajectories = gameObject.GetComponentsInChildren<TrajectoryDealer>();
        foreach (TrajectoryDealer dealer in trajectories)
        {
            dealer.allocator = allocator;
        }
    }

    public void CreatePatron(DynamicData data, Transform patron, Transform target, UnityEvent<Transform, Transform> OnReached, System.Action<string> cansel)
    {
        if (currentCount == trajectories.Length)
        {
            currentCount = 0;
        }
        foreach (TrajectoryDealer d in trajectories)
        {
            if (d.ContainsPatron(patron)) return;
        }

        trajectories[currentCount].CreatePatron(data, patron, target, OnReached, cansel);
        currentCount++;
    }

    public void CreatePatron(DynamicData data, Action<Transform, Transform> value, System.Action<string> cansel)
    {
        UnityEvent<Transform, Transform> tempEvent = new UnityEvent<Transform, Transform>();
        tempEvent?.AddListener((p, t) => value(p, t));
        if (currentCount >= trajectories.Length)
        {
            currentCount = 0;
        }
        foreach (TrajectoryDealer d in trajectories)
        {
            if (d.ContainsPatron(data.patron))
            {

                cansel("Патрон уже существует ");
                return;
            }
        }

        trajectories[currentCount].CreatePatron(data, data.patron, data.target, tempEvent, cansel);
        currentCount++;
    }
}
