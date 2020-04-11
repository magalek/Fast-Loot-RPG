using System;
using System.Collections.Generic;
using System.Linq;
using RPG.Controllers;
using RPG.Utility;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RPG.Environment {
    public class Room : MonoBehaviour {

        public RoomOpening roomOpening;
        public List<SpawnPoint> SpawnPoints => transform.Find("Spawn Points").GetComponentsInChildren<SpawnPoint>().ToList();
        
    }
}