using UnityEngine;
using UnityEngine.UI;

public class imagem : MonoBehaviour
{
    private Image img;  
    public transforma  personagem;   
    public Sprite kerolyne, kerocat;    
   
    void Start()
    {
        img = GetComponent<Image>();
    }
   
    void Update()
    {
        if (personagem.indiceAtual % 2 == 0)
        {
            img.sprite = kerolyne;
        }
        else
        {
            img.sprite = kerocat;
        }
    }
}
