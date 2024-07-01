using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private float puntos;
    private TextMeshProUGUI textMesh;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        textMesh.text = puntos.ToString("0");
    }

    public void SumarPuntos(float puntosEntrada)
    {
        puntos += puntosEntrada;
    }

    public float GetPuntos()
    {
        return puntos;
    }
}
