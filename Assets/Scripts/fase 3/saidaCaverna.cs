using System.Collections;
using UnityEngine;

public class saidaCaverna : MonoBehaviour
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
        if (PlayerPrefs.HasKey("FaseTag") && PlayerPrefs.GetString("FaseTag") == "TopDown")
        {
            fase.tag = "TopDown";
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
        // Altera e salva a tag
        fase.tag = "TopDown";
        PlayerPrefs.SetString("FaseTag", "TopDown");
        PlayerPrefs.Save();

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
        yield return new WaitForSeconds(0.5f);
    }
}
