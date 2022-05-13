using UnityEngine;
using UnityEngine.UI;

public class HeadquartersUI : MonoBehaviour
{

	[SerializeField] private Button tabProfileButton;
	[SerializeField] private Button tabInventoryButton;
	[SerializeField] private Button tabStoreButton;
    [SerializeField] private Button tabPlayButton;

    [SerializeField] private TabTween tabProfileTween;
    [SerializeField] private TabTween tabInventoryTween;
    [SerializeField] private TabTween tabStoreTween;
    [SerializeField] private TabTween tabPlayTween;

    [SerializeField] private Canvas profileCanvas;
    [SerializeField] private Canvas inventoryCanvas;
    [SerializeField] private Canvas storeCanvas;
    [SerializeField] private Canvas playCanvas;

    private bool _isTransistioning = false;

    private void Awake()
    {

        tabProfileButton.onClick.AddListener(()=> {

            if (_isTransistioning) return;

            _isTransistioning = true;

            profileCanvas.sortingOrder = 1;
            inventoryCanvas.sortingOrder = 0;
            storeCanvas.sortingOrder = 0;
            playCanvas.sortingOrder = 0;
            tabProfileTween.Tween(()=> {
                tabPlayTween.ResetTween();
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
            playCanvas.sortingOrder = 0;
            tabInventoryTween.Tween(()=> {
                tabPlayTween.ResetTween();
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
            playCanvas.sortingOrder = 0;
            tabStoreTween.Tween(()=> {
                tabPlayTween.ResetTween();
                tabInventoryTween.ResetTween();
                tabProfileTween.ResetTween();

                _isTransistioning = false;

            });
        });

        tabPlayButton.onClick.AddListener(() => {

            if (_isTransistioning) return;

            _isTransistioning = true;

            profileCanvas.sortingOrder = 0;
            inventoryCanvas.sortingOrder = 0;
            storeCanvas.sortingOrder = 0;
            playCanvas.sortingOrder = 1;
            tabPlayTween.Tween(()=> {
                tabInventoryTween.ResetTween();
                tabProfileTween.ResetTween();
                tabStoreTween.ResetTween();

                _isTransistioning = false;

            });
        });

    }

}
