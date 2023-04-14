using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSigletone<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Inst { get; private set; }
    void Awake() => Inst = FindObjectOfType(typeof(T)) as T;
    
}
