using System.Collections;
using System.Collections.Generic;
using Inventory;
using Inventory.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TransitionType
{
    FirstLevel,
    NextLevel
}
    

public class DoorLevelTrigger : MonoBehaviour
{
    [SerializeField]
    private string sceneName;

    [SerializeField]
    private TransitionType transitionType;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (transitionType)
        {
            case TransitionType.FirstLevel:
                var op = SceneManager.LoadSceneAsync(sceneName);
                op.completed += (x) => {
                    PauseManager.Instance.Hide();
                    SoundMixerManager.Instance.Hide();
                    PlayerHealth.Instance.SetRestart();
                    WeaponPool.Instance.Reset();
                    InventoryController.Instance.ResetInventory();
                    EconomyManager.Instance.Initialise();
                    //ShopPool.Instance.Reset();
                };
                break;

            case TransitionType.NextLevel:
                List<InventoryItemObject> curr =
                        new List<InventoryItemObject>
                        (InventorySystem.Instance.inventoryData.GetCurrentInventoryState().Values);

                var op2 = SceneManager.LoadSceneAsync(sceneName);
                op2.completed += (x) => {
                    PauseManager.Instance.Hide();
                    SoundMixerManager.Instance.Hide();
                    PlayerHealth.Instance.SetRestart();
                    PlayerHealth.Instance.UpdateHealthUI();
                    EconomyManager.Instance.Initialise();
                    InventoryController.Instance.ResetInventory();
                    

                    Debug.Log("weapons before transition");
                    foreach (var item in curr)
                    {
                        Debug.Log(item.weapon.Name);
                    }
                    InventoryController.Instance.ContinueInventory(curr);
                };
                break;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            List<InventoryItemObject> curr =
                        new List<InventoryItemObject>
                        (InventorySystem.Instance.inventoryData.GetCurrentInventoryState().Values);
            Debug.Log("weapons before transition");
            foreach (var item in curr)
            {
                Debug.Log(item.weapon.Name);
            }
        }
    }
}