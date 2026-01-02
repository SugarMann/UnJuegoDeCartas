using System;
using SerializeReferenceEditor;
using UnityEngine;

[Serializable]
public class AutoTargetEffect
{
    [field: SerializeReference]
    [field: SR]
    public TargetMode TargetMode { get; private set; }

    [field: SerializeReference]
    [field: SR]
    public Effect Effect { get; private set; }
}