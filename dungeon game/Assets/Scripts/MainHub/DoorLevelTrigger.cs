using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DoorLevelTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //SceneManager.LoadScene("DungeonSample");
        //SceneManager.LoadScene("FirstScene_1");
        var op = SceneManager.LoadSceneAsync("Floor 2");
        op.completed += (x) => {
            PauseManager.Instance.Hide();
            SoundMixerManager.Instance.Hide();
            PlayerHealth.Instance.SetRestart();
            WeaponPool.Instance.Reset();
            //ShopPool.Instance.Reset();
        };
    }
}