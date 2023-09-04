using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetlayerBlock : MonoBehaviour
{

    SpriteRenderer spriteRendererBlock;

    //spriteRendererBlock.renderingLayerMask = 1;

    private void Awake()
    {
        spriteRendererBlock = GetComponent<SpriteRenderer>();
    }

    public void OnSetLayerSprite()
    {
        spriteRendererBlock.renderingLayerMask = 1;
    }
    public void OffSetLayerSprite()
    {
        spriteRendererBlock.renderingLayerMask = 0;
    }


}
