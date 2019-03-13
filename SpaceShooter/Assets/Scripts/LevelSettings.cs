using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level Settings", fileName = "New LevelSettings")]
public class LevelSettings : ScriptableObject
{
    [Tooltip("The number of big asteroids to start with.")]
    public int numAsteroids;

    [Tooltip("The number of children an asteroid will break into.")]
    public int numChildren;

    [Range(0, 3), Tooltip("The smallest size asteroid to show.")]
    public int minSize;
}
