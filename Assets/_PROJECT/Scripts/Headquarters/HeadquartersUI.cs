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

    private void Awake()
    {

        tabProfileButton.onClick.AddListener(()=> {

            tabProfileTween.Tween(()=> {
                tabInventoryTween.ResetTween();
                tabStoreTween.ResetTween();
            });
        });

        tabInventoryButton.onClick.AddListener(() => {

            tabInventoryTween.Tween(()=> {
                tabStoreTween.ResetTween();
                tabProfileTween.ResetTween();
            });
        });

        tabStoreButton.onClick.AddListener(() => {

            tabStoreTween.Tween(()=> {
                tabInventoryTween.ResetTween();
                tabProfileTween.ResetTween();
            });
        });

    }

}
