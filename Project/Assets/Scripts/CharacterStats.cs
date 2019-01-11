using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : Singleton<CharacterStats>
{

    public CharacterStats_SO m_CharacterDefinition;

    #region Initializations
    void Start()
    {
        if (!m_CharacterDefinition.m_SetManually)
        {
            m_CharacterDefinition.m_MaxHealth = 100;
            m_CharacterDefinition.m_CurrentHealth = 100;
        }
    }
    #endregion

    #region Stat Increasers
    public void ApplyHealth(int healthValue)
    {
        m_CharacterDefinition.ApplyHealth(healthValue);
    }
    #endregion


    #region Stat Reducers
    public void TakeDamage(int amount)
    {
        m_CharacterDefinition.TakeDamage(amount);
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        // This should be triggered by the GameManager during a save point.
        //if(Input.GetMouseButtonDown(2))
        //{
        //    m_CharacterDefinition.saveCharacterStatData();
        //}
    }


    #region Reporters
    public int GetHealth()
    {
        return m_CharacterDefinition.m_CurrentHealth;
    }
    #endregion
}
