using UnityEngine;
using UnityEngine.UI;

public class HeadquartersUI : MonoBehaviour
{

	[SerializeField] private Button tabProfileButton;
	[SerializeField] private Button tabInventoryButton;
	[SerializeField] private Button tabStoreButton;

    [SerializeField] private TabTween tabProfileTween;
    [SerializeField] private TabTween tabInventoryTween;
    [SerializeField] private TabTween tabStoreTween;

    [SerializeField] private Canvas profileCanvas;
    [SerializeField] private Canvas inventoryCanvas;
    [SerializeField] private Canvas storeCanvas;

    [Header("Play")]
    [SerializeField] private Button playButton;

    private bool _isTransistioning = false;

    private void Awake()
    {

        tabProfileButton.onClick.AddListener(()=> {

            if (_isTransistioning) return;

            _isTransistioning = true;

            profileCanvas.sortingOrder = 1;
            inventoryCanvas.sortingOrder = 0;
            storeCanvas.sortingOrder = 0;
            tabProfileTween.Tween(()=> {
                tabInventoryTween.ResetTween();
                tabStoreTween.ResetTween();

                _isTransistioning = false;

            });
        });

        tabInventoryButton.onClick.AddListener(() => {

            if (_isTransistioning) return;

            _isTransistioning = true;

            profileCanvas.sortingOrder = 0;
            inventoryCanvas.sortingOrder = 1;
            storeCanvas.sortingOrder = 0;
            tabInventoryTween.Tween(()=> {
                tabStoreTween.ResetTween();
                tabProfileTween.ResetTween();

                _isTransistioning = false;

            });
        });

        tabStoreButton.onClick.AddListener(() => {

            if (_isTransistioning) return;

            _isTransistioning = true;

            profileCanvas.sortingOrder = 0;
            inventoryCanvas.sortingOrder = 0;
            storeCanvas.sortingOrder = 1;
            tabStoreTween.Tween(()=> {
                tabInventoryTween.ResetTween();
                tabProfileTween.ResetTween();

                _isTransistioning = false;

            });
        });

    }

}
