using UnityEngine;
using Finark.Utils;

public class TurretSelection : MonoBehaviour
{

    [SerializeField] private Turret selectedTurret;

    private TurretSelectionUI _turretSelectionUI;

    private void Awake()
    {
        _turretSelectionUI = GetComponent<TurretSelectionUI>();
    }

    private void Update()
    {
#if UNITY_ANDROID
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Vector2 touchRay = Camera.main.ScreenToWorldPoint(Input.touches[0].position);

            RaycastHit2D hit = Physics2D.Raycast(touchRay, (Input.GetTouch(0).position));

            if (hit.transform.TryGetComponent(out Turret turret))
            {
                selectedTurret = turret;
            }
            else
            {
                selectedTurret = null;
            }

        }

#endif
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit2D hit = Physics2D.Raycast(MyUtils.GetMouseWorldPosition(), Vector2.zero);

            if (hit.transform.TryGetComponent(out Turret turret))
            {
                selectedTurret = turret;
                _turretSelectionUI.OpenSelectionUI(selectedTurret);
            }
            else
            {
                if (MyUtils.IsPointerOverUI()) return;

                selectedTurret = null;
                _turretSelectionUI.CloseSelectionUI();
            }

        }

#endif

    }

}
