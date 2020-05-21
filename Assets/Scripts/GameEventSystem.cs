using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameEventSystem
{

    public Func<Mob, Mob> OnModCreateAction;
    public Func<Mob, Mob> OnMobDead;
    public Action OnPlayerDead;
    public Action OnUpdateUI;

    public Mob MobCreateEvent(Mob mob)
    {
        if(OnModCreateAction != null)
        {
           return  OnModCreateAction.Invoke(mob);
        }
        return null;
    }

    public Mob MobDead(Mob mob)
    {
        if (OnMobDead != null)
        {
            OnMobDead.Invoke(mob);
        }

        return null;
    }

    public void PlayerDead()
    {
        if(OnPlayerDead != null)
        {
            OnPlayerDead.Invoke();
            Debug.Log("PlayerISDead");
        }
    }

    public void UpdateUI()
    {
        if (OnUpdateUI != null)
        {
            OnUpdateUI.Invoke();

        }
    }


}
