using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelSkill : MonoBehaviour
{
    public void CancelSkillAction()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
