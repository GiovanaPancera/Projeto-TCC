using System.Collections;
using UnityEngine;

public class entrarCaverna : MonoBehaviour
{
    public GameObject transicao, personagem, fase, load;
    public Vector3 local;
    private GameObject personagemAtual;
    private Animator anim, anim2;

    void Start()
    {
        anim = transicao.GetComponent<Animator>();
        anim2 = load.GetComponent<Animator>();

        // Verifica se a tag foi salva anteriormente
        if (PlayerPrefs.HasKey("FaseTag") && PlayerPrefs.GetString("FaseTag") == "Plataforma")
        {
            fase.tag = "Plataforma";
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player")) 
        {
            personagemAtual = GameObject.FindWithTag("Player");
            transicao.SetActive(true);
            StartCoroutine(TransicaoCena());
        }
    }

    IEnumerator TransicaoCena()
    {
        anim.SetBool("mudou", true);
        yield return new WaitForSeconds(1f);
        load.SetActive(true);
        anim2.SetBool("mudou", true);
        yield return new WaitForSeconds(1f);

        personagem.transform.position = local;
        personagemAtual.transform.position = local;

        yield return new WaitForSeconds(0.5f);
        load.SetActive(false);
        anim.SetBool("mudou", false);
        anim2.SetBool("mudou", false);
        transicao.SetActive(false);

        // Altera e salva a tag
        fase.tag = "Plataforma";
        PlayerPrefs.SetString("FaseTag", "Plataforma");
        PlayerPrefs.Save();
    }
}
