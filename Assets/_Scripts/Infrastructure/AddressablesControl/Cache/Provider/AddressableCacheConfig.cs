using System;
using System.Collections.Generic;
using UnityEngine;

namespace KingdomCum.FTUE.AddressablesControl.Provider
{
    public class AddressableCacheConfig
    {
        [field: SerializeField] public Dictionary<LifeTimeMode, int> TimeToReleases { get; private set; } = new();
    }
}