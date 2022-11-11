using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager 
{
    // points added support
    static List<Target> pointsAddedInvokers = new List<Target>();
    static List<UnityAction<int>> pointsAddedListeners =
        new List<UnityAction<int>>();


    #region Points added support

    /// <summary>
    /// Adds the given script as a points added invoker
    /// </summary>
    /// <param name="invoker">invoker</param>
    public static void AddPointsAddedInvoker(Target invoker)
    {
        // add invoker to list and add all listeners to invoker
        pointsAddedInvokers.Add(invoker);
        foreach (UnityAction<int> listener in pointsAddedListeners)
        {
            invoker.AddPointsAddedListener(listener);
        }
    }

    /// <summary>
    /// Adds the given method as a points added listener
    /// </summary>
    /// <param name="listener">listener</param>
    public static void AddPointsAddedListener(UnityAction<int> listener)
    {
        // add listener to list and to all invokers
        pointsAddedListeners.Add(listener);
        foreach (Target invoker in pointsAddedInvokers)
        {
            invoker.AddPointsAddedListener(listener);
        }
    }

    /// <summary>
    /// Remove the given script as a points added invoker
    /// </summary>
    /// <param name="invoker">invoker</param>
    public static void RemovePointsAddedInvoker(Target invoker)
    {
        // remove invoker from list
        pointsAddedInvokers.Remove(invoker);
    }

    #endregion
}
