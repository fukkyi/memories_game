using UnityEngine;

public class MemoryObject : MonoBehaviour {

    public void InitObj(string memoryName, Sprite memorySprite) {

        gameObject.name = memoryName;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null) {

            spriteRenderer.sprite = memorySprite;
        }

        if (GetComponent<PolygonCollider2D>() == null) {

            gameObject.AddComponent<PolygonCollider2D>();
        }
    }

    void OnCollisionEnter2D () {

        gameObject.layer = LayerMask.NameToLayer("CollidedMemories");
    }
}
