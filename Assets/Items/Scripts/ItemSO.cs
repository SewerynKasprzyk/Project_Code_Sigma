using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public StatToChange statToChange = new StatToChange();




    public enum StatToChange
    {
        None,
        Health,
        Attack,
        Defense,
        Speed
    }

}
