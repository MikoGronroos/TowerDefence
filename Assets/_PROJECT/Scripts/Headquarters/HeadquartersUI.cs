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

    private void Awake()
    {

        tabProfileButton.onClick.AddListener(()=> {
            tabProfileTween.Tween(()=> {
                tabPlayTween.ResetTween();
                tabInventoryTween.ResetTween();
                tabStoreTween.ResetTween();
            });
        });

        tabInventoryButton.onClick.AddListener(() => {
            tabInventoryTween.Tween(()=> {
                tabPlayTween.ResetTween();
                tabStoreTween.ResetTween();
                tabProfileTween.ResetTween();
            });
        });

        tabStoreButton.onClick.AddListener(() => {
            tabStoreTween.Tween(()=> {
                tabPlayTween.ResetTween();
                tabInventoryTween.ResetTween();
                tabProfileTween.ResetTween();
            });
        });

        tabPlayButton.onClick.AddListener(() => {
            tabPlayTween.Tween(()=> {
                tabInventoryTween.ResetTween();
                tabProfileTween.ResetTween();
                tabStoreTween.ResetTween();
            });
        });

    }

}
