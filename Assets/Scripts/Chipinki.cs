using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PuzzleConfig", fileName = "Puzzle")]
public class Chipinki : ScriptableObject
{
    public List<Shuraba> Shurabinki;

    [Serializable]
    public class Shuraba
    {
        public Sprite Fulka;
        public List<Sprite> Chastichki;
    }
}
