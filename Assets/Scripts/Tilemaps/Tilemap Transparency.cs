using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapTransparency : MonoBehaviour
{
    private TilemapRenderer tilemapRenderer;
    private Material tilemapMaterial;

    void Start()
    {
        // Получаем TilemapRenderer
        tilemapRenderer = GetComponent<TilemapRenderer>();

        // Присваиваем новый шейдер, который поддерживает прозрачность
        tilemapMaterial = tilemapRenderer.material;

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SetTransparency(0.1f); // Сделать Tilemap полупрозрачным
        }
    }

    // Метод для изменения прозрачности
    public void SetTransparency(float alpha)
    {
        // Убедитесь, что альфа находится в диапазоне от 0 до 1
        alpha = Mathf.Clamp01(alpha);

        // Получаем текущий цвет материала
        Color color = tilemapMaterial.color;

        // Изменяем альфа-канал
        color.a = alpha;

        // Устанавливаем новый цвет обратно в материал
        tilemapMaterial.color = color;
    }
}
