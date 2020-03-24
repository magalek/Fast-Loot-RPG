using System.Collections.Generic;
using UnityEngine;

namespace RPG.Utility {
    public static class ExtensionMethods {
        public static T RandomObject<T>(this List<T> list) {
            int rand = Random.Range(0, list.Count);
            return list[rand];
        }
    }
}