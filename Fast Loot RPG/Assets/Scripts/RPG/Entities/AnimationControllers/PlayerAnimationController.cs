using RPG.UI;
using UnityEngine;

namespace RPG.Entities.AnimationControllers {
    public class PlayerAnimationController : EntityAnimationController {

        [SerializeField] public Transform weaponSpriteParent;

        public void RotateWeaponSprite() {
            Vector3 mouseWorldPosition = MainCamera.Instance.camera.ScreenToWorldPoint(Input.mousePosition);
            
            float angleRad = Mathf.Atan2(mouseWorldPosition.y - weaponSpriteParent.position.y, mouseWorldPosition.x - weaponSpriteParent.position.x);
            float angleDeg = (180 / Mathf.PI) * angleRad;

            weaponSpriteParent.rotation = Quaternion.Euler(0, 0, angleDeg);
        }
    }
}