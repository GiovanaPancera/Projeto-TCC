using UnityEngine;

[ExecuteInEditMode]
public class ColorHighlightEffect : MonoBehaviour
{
    public Material effectMaterial;
    [ColorUsage(true, true)]
    public Color[] highlightColors;
    [Range(0f, 1f)]
    public float threshold = 0.1f;
    public Transform[] targets;
    public float[] radii;  
    private const int MaxTargetCount = 10;
    public GameObject personagem;
    public raioLampiao raio;
    
    void Start()
    {
        raio = personagem.GetComponent<raioLampiao>(); 
    }

    void Update()
    {
        if (raio != null && raio.raioDentro == true)
        {
            radii[0] = 0f;
            raio.raioDentro = false;
        }
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (effectMaterial != null && targets != null && targets.Length > 0)
        {
            // Limita o número de alvos passados para o shader
            int targetCount = Mathf.Min(targets.Length, MaxTargetCount);

            Vector4[] targetPositions = new Vector4[MaxTargetCount]; // Garantir tamanho máximo
            Color[] colors = new Color[MaxTargetCount]; // Garantir tamanho máximo
            float[] targetRadii = new float[MaxTargetCount]; // Garantir tamanho máximo

            // Preenche as posições, cores e raios dos alvos
            for (int i = 0; i < targetCount; i++)
            {
                if (targets[i] != null)
                {
                    // Converte a posição mundial do alvo para coordenadas de tela
                    Vector3 screenPos = Camera.main.WorldToViewportPoint(targets[i].position);
                    targetPositions[i] = new Vector4(screenPos.x, screenPos.y, 0, 0);

                    // Atribui a cor do alvo, ou usa branco como fallback
                    colors[i] = (i < highlightColors.Length) ? highlightColors[i] : Color.white;

                    // Atribui o raio do alvo
                    targetRadii[i] = (i < radii.Length) ? radii[i] : 0.3f; // Defina o valor padrão como 0.3f
                }
            }

            // Passa as variáveis para o material (shader)
            effectMaterial.SetInt("_TargetCount", targetCount);
            effectMaterial.SetVectorArray("_TargetPositions", targetPositions);
            effectMaterial.SetColorArray("_HighlightColors", colors);
            effectMaterial.SetFloatArray("_TargetRadii", targetRadii); // Passa o array de raios
            effectMaterial.SetFloat("_Threshold", threshold);

            // Aplica o efeito usando o shader
            Graphics.Blit(src, dest, effectMaterial);
        }
        else
        {
            // Caso não tenha alvos ou o material, apenas desenha a imagem original
            Graphics.Blit(src, dest);
        }
    }
}
