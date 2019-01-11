using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStats", menuName = "Character/Stats", order = 1)]
public class CharacterStats_SO : ScriptableObject
{
    #region Fields

    public bool m_SetManually = false;
    public bool m_SaveDataOnClose = false;
    public int m_MaxHealth = 0;
    public int m_CurrentHealth = 0;
    #endregion

    #region Stat Increasers
    public void ApplyHealth(int healthValue)
    {
        if ((m_CurrentHealth + healthValue) > m_MaxHealth)
        {
            m_CurrentHealth = m_MaxHealth;
        }
        else
        {
            m_CurrentHealth += healthValue;
        }
    }
    #endregion

    #region Stat Reducers
    public void TakeDamage(int amount)
    {
        m_CurrentHealth -= amount;
        if (m_CurrentHealth <= 0)
        {
            Death();
        }
    }
    #endregion

    #region Character LevelUp and Death
    private void Death()
    {
        Debug.Log("You died.");
    }
    #endregion

    public void SaveCharacterStatData()
    {
        m_SaveDataOnClose = true;
        EditorUtility.SetDirty(this);
    }
}
