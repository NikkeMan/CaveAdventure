using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapFade : MonoBehaviour
{
    Tilemap tileMap;
    float alphaMin = 0.25f, alphaMax = 1.0f, alphaCurrent = 1.0f;
    bool isFadingOut = false;
    Color transparent = new Color(1, 1, 1, 0.25f);
    float fadeSpeedMultiplier = 2f; // if 1f = 1 second lerp time, if 2f = 0.5 seconds etc 

    void Start()
    {
        tileMap = gameObject.GetComponent<Tilemap>();
        tileMap.SetTileFlags(tileMap.origin, TileFlags.None);
    }

    void Update()
    {
        if(isFadingOut && alphaCurrent > alphaMin) {
            alphaCurrent -= (Time.deltaTime * fadeSpeedMultiplier);
            tileMap.color = Color.Lerp(transparent, Color.white, alphaCurrent);
        }

        else if (!isFadingOut && alphaCurrent < alphaMax) {
            alphaCurrent += (Time.deltaTime * fadeSpeedMultiplier);
            tileMap.color = Color.Lerp(transparent, Color.white, alphaCurrent);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            isFadingOut = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            isFadingOut = false;
        }
    }
}