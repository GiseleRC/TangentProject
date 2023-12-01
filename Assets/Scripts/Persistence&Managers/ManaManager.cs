using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ManaManager : Singleton<ManaManager>
{
    //private bool _rechargingMana;
    private TimeSpan timer;
    private int id;

    private void Start()
    {
        GameManager.Instance.LoadPersistentData();
        StartCoroutine(RechargeManaCourutine());

        if (GameManager.Instance.PersistentData.Mana < GameManager.Instance.ConstantsDataStats.MaxManaCapacity)
        {
            timer = GameManager.Instance.PersistentData.NextManaTime - DateTime.Now;
        }
    }

    public bool HasEnoughMana(int mana) => GameManager.Instance.PersistentData.Mana >= mana;

    public void UseMana(int mana)
    {
        if (GameManager.Instance.PersistentData.Mana >= mana)
        {
            GameManager.Instance.PersistentData.Mana -= mana;

            {
                GameManager.Instance.PersistentData.NextManaTime = AddTime(DateTime.Now, GameManager.Instance.ConstantsDataStats.RechargeTime);
                StartCoroutine(RechargeManaCourutine());
            }
        }
    }

    private IEnumerator RechargeManaCourutine()
    {
        //_rechargingMana = true;
        UIMana.Instance.UpdateUI(GameManager.Instance.PersistentData.Mana);

        while (GameManager.Instance.PersistentData.Mana < GameManager.Instance.ConstantsDataStats.MaxManaCapacity)
        {
            DateTime currentT = DateTime.Now;
            DateTime nextT = GameManager.Instance.PersistentData.NextManaTime;

            bool _addingStamina = false;

            while (currentT > nextT)
            {
                if (GameManager.Instance.PersistentData.Mana >= GameManager.Instance.ConstantsDataStats.MaxManaCapacity) break;

                GameManager.Instance.PersistentData.Mana += 1;
                _addingStamina = true;

                DateTime timeToAdd = nextT;
                if (GameManager.Instance.PersistentData.LastManaTime > nextT) timeToAdd = GameManager.Instance.PersistentData.LastManaTime;//Checkear si el usuario cerro la app

                nextT = AddTime(timeToAdd, GameManager.Instance.ConstantsDataStats.RechargeTime);
            }

            if (_addingStamina)
            {
                GameManager.Instance.PersistentData.NextManaTime = nextT;
                GameManager.Instance.PersistentData.LastManaTime = DateTime.Now;
            }

            UIMana.Instance.UpdateUI(GameManager.Instance.PersistentData.Mana);
            GameManager.Instance.SavePersistentData();

            yield return new WaitForEndOfFrame();
        }
        //_rechargingMana = false;
    }

    private DateTime AddTime(DateTime timeToAdd, float timerToRechargeMana)
    {
        return timeToAdd.AddSeconds(timerToRechargeMana);
    }
}