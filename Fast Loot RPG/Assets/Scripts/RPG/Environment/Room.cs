using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG.Environment {
    public class Room : MonoBehaviour {

        public RoomOpenings roomOpenings;
        public List<SpawnPoint> SpawnPoints => transform.Find("Spawn Points").GetComponentsInChildren<SpawnPoint>().ToList();
        
    }
}