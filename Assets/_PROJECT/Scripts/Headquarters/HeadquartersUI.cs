using UnityEngine;
using UnityEngine.UI;

public class HeadquartersUI : MonoBehaviour
{

	[SerializeField] private Button tabInventoryButton;
	[SerializeField] private Button tabStoreButton;

    [SerializeField] private TabTween tabInventoryTween;
    [SerializeField] private TabTween tabStoreTween;

    [SerializeField] private Canvas inventoryCanvas;
    [SerializeField] private Canvas storeCanvas;

    [Header("Play")]

    [SerializeField] private Button playButton;

    [Header("User Profile Info UI")]

    [SerializeField] private Button userSmallInfoButton;
    [SerializeField] private Button closeUserLargeInfoButton;

    [SerializeField] private GameObject userlargeInfoGameObject;


    private bool _isTransistioning = false;

    private void Awake()
    {

        #region Tab Buttons

        tabInventoryButton.onClick.AddListener(() => {

            if (_isTransistioning) return;

            _isTransistioning = true;

            inventoryCanvas.sortingOrder = 1;
            storeCanvas.sortingOrder = 0;
            tabInventoryTween.Tween(()=> {
                tabStoreTween.ResetTween();

                _isTransistioning = false;

            });
        });

        tabStoreButton.onClick.AddListener(() => {

            if (_isTransistioning) return;

            _isTransistioning = true;

            inventoryCanvas.sortingOrder = 0;
            storeCanvas.sortingOrder = 1;
            tabStoreTween.Tween(()=> {
                tabInventoryTween.ResetTween();

                _isTransistioning = false;

            });
        });

        #endregion

        userSmallInfoButton.onClick.AddListener(() => 
        {
            userlargeInfoGameObject.SetActive(true);
        });

        closeUserLargeInfoButton.onClick.AddListener(() => 
        {
            userlargeInfoGameObject.SetActive(false);
        });

    }

}
