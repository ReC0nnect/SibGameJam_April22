using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class F
{
    static PrefabSettings PrefabsCached;
    public static PrefabSettings Prefabs {
        get {
            if (!PrefabsCached)
            {
                PrefabsCached = Resources.Load<PrefabSettings>("PrefabSettings");
            }
            return PrefabsCached;
        }
    }

    static CommonSettings SettingsCached;
    public static CommonSettings Settings {
        get {
            if (!SettingsCached)
            {
                SettingsCached = Resources.Load<CommonSettings>("CommonSettings");
            }
            return SettingsCached;
        }
    }
}
