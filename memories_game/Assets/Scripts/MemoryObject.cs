using UnityEngine;

public class MemoryObject : ComponentManager {

    void Update()
    {
        if (transform.position.y < -4) {

            GetGameComponent().DecTime(10f);

            if (gameObject.layer != LayerMask.NameToLayer("CollidedMemories")) {

                GetGameComponent().Collided();
            }

            Destroy(gameObject);
        }
    }

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

        if (gameObject.layer != LayerMask.NameToLayer("CollidedMemories")) {

            gameObject.layer = LayerMask.NameToLayer("CollidedMemories");
            GetGameComponent().Collided();
        }
    }
}
