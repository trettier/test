using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapTransparency : MonoBehaviour
{
    private TilemapRenderer tilemapRenderer;
    private Material tilemapMaterial;

    void Start()
    {
        // �������� TilemapRenderer
        tilemapRenderer = GetComponent<TilemapRenderer>();

        // ����������� ����� ������, ������� ������������ ������������
        tilemapMaterial = tilemapRenderer.material;

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SetTransparency(0.1f); // ������� Tilemap ��������������
        }
    }

    // ����� ��� ��������� ������������
    public void SetTransparency(float alpha)
    {
        // ���������, ��� ����� ��������� � ��������� �� 0 �� 1
        alpha = Mathf.Clamp01(alpha);

        // �������� ������� ���� ���������
        Color color = tilemapMaterial.color;

        // �������� �����-�����
        color.a = alpha;

        // ������������� ����� ���� ������� � ��������
        tilemapMaterial.color = color;
    }
}
