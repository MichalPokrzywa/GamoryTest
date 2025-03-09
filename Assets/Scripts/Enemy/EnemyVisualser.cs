using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyVisualser : MonoBehaviour
{
    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private Color baseColor;
    [SerializeField] private Color damageColor;
    [SerializeField] private Color critDamageColor;

    private Material material;

    private void Awake()
    {
        material = renderer.material; 
        material.color = baseColor;  
    }

    public void ShowDamage()
    {
        material.DOColor(damageColor, 0.1f) 
            .OnKill(() => material.DOColor(baseColor, 0.3f));  
    }

    public void ShowCritDamage()
    {
        material.DOColor(critDamageColor, 0.1f)  
            .OnKill(() => material.DOColor(baseColor, 0.3f));  
    }

}
