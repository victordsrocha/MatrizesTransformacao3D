/*
 * Esta classe executa exemplos de uso de Mat4
 */

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    // instância de Mat4
    private Mat4 _matrizMat4;

    // Utilizei para os testes uma esfera contendo somente o "vértice" de seu centro e um cubo com seus oito vértices
    // Os valores dos vértices do cubo são relativos a posição no espaço do próprio cubo
    private Mesh _cuboMesh;
    private GameObject _cubo;
    private GameObject _esfera;

    // inicialização UI
    public Button botaoRotacaoX;
    public Button botaoRotacaoY;
    public Button botaoRotacaoZ;
    public Button botaoTranslacao;
    public Button botaoEscala;
    public Button botaoResetar;
    public Button botaoRotacaoArbitrario1;
    public Button botaoRotacaoArbitrario2;
    public Button botaoTRS;

    // controle de operação
    private int _op = 0;

    // Start is called before the first frame update
    void Start()
    {
        // inicialização UI
        InicializarBotoes();

        // inicialização Mat4
        _matrizMat4 = new Mat4();

        // inicialização esfera e cubo
        _cubo = GameObject.FindGameObjectWithTag("Cubo");
        _cuboMesh = _cubo.GetComponent<MeshFilter>().mesh;
        _esfera = GameObject.FindGameObjectWithTag("Esfera");
    }


    // Update is called once per frame
    void Update()
    {
        ExecutarOperacaoExemplo();
    }


    void ExemploRotacionarEixoX()
    {
        _matrizMat4.RotacaoEixoX(Mathf.PI * 0.01f);
        _esfera.transform.position = _matrizMat4.Transformar(_esfera.transform.position);

        Vector3[] vertices = _cuboMesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = _matrizMat4.Transformar(vertices[i]);
        }

        _cuboMesh.vertices = vertices;
    }

    void AtivarExemploRotacionarX()
    {
        _op = 1;
    }

    void ExemploRotacionarEixoY()
    {
        _matrizMat4.RotacaoEixoY(Mathf.PI * 0.01f);
        _esfera.transform.position = _matrizMat4.Transformar(_esfera.transform.position);

        Vector3[] vertices = _cuboMesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = _matrizMat4.Transformar(vertices[i]);
        }

        _cuboMesh.vertices = vertices;
    }

    void AtivarExemploRotacionarY()
    {
        _op = 2;
    }

    void ExemploRotacionarEixoZ()
    {
        _matrizMat4.RotacaoEixoZ(Mathf.PI * 0.01f);
        _esfera.transform.position = _matrizMat4.Transformar(_esfera.transform.position);

        Vector3[] vertices = _cuboMesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = _matrizMat4.Transformar(vertices[i]);
        }

        _cuboMesh.vertices = vertices;
    }

    void AtivarExemploRotacionarZ()
    {
        _op = 3;
    }


    void ExemploTranslacao()
    {
        _matrizMat4.Translacao(0.02f, 0.02f, 0.02f);
        Vector3[] vertices = _cuboMesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = _matrizMat4.Transformar(vertices[i]);
        }

        _cuboMesh.vertices = vertices;
    }

    void AtivarExemploTranslacao()
    {
        _op = 4;
    }

    void ExemploEscala()
    {
        _matrizMat4.Escala(1.01f, 1.01f, 1.01f);
        Vector3[] vertices = _cuboMesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = _matrizMat4.Transformar(vertices[i]);
        }

        _cuboMesh.vertices = vertices;
    }

    void AtivarExemploEscala()
    {
        _op = 5;
    }

    void ExemploRotacionarEixoArbitrario1()
    {
        Vector3 u = new Vector3(2, 2, 2);
        _matrizMat4.RotacaoEixoArbitrario(Mathf.PI * 0.01f, u);
        _esfera.transform.position = _matrizMat4.Transformar(_esfera.transform.position);

        Vector3[] vertices = _cuboMesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = _matrizMat4.Transformar(vertices[i]);
        }

        _cuboMesh.vertices = vertices;
    }

    public void AtivarExemploRotacionarArbitrario1()
    {
        _op = 6;
    }

    void ExemploRotacionarEixoArbitrario2()
    {
        Vector3 u = new Vector3(-4, 2, 5);
        _matrizMat4.RotacaoEixoArbitrario(Mathf.PI * 0.01f, u);
        _esfera.transform.position = _matrizMat4.Transformar(_esfera.transform.position);

        Vector3[] vertices = _cuboMesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = _matrizMat4.Transformar(vertices[i]);
        }

        _cuboMesh.vertices = vertices;
    }

    public void AtivarExemploRotacionarArbitrario2()
    {
        _op = 7;
    }

    void ExemploTRS()
    {
        var u = new Vector3(0.1f, 0, -0.1f);
        _matrizMat4.TRS(0.002f, 0.001f, 0.003f, Mathf.PI * 0.001f, u, 1.001f);
        _esfera.transform.position = _matrizMat4.Transformar(_esfera.transform.position);

        Vector3[] vertices = _cuboMesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = _matrizMat4.Transformar(vertices[i]);
        }

        _cuboMesh.vertices = vertices;
    }

    public void AtivarExemploTRS()
    {
        _op = 8;
    }

    void Resetar()
    {
        _op = 0;
        _cubo.GetComponent<MeshGenerator>().CriarCubo();
        _esfera.transform.position = new Vector3(3, 3, 0);
    }

    private void ExecutarOperacaoExemplo()
    {
        switch (_op)
        {
            case 1:
                ExemploRotacionarEixoX();
                break;
            case 2:
                ExemploRotacionarEixoY();
                break;
            case 3:
                ExemploRotacionarEixoZ();
                break;
            case 4:
                ExemploTranslacao();
                break;
            case 5:
                ExemploEscala();
                break;
            case 6:
                ExemploRotacionarEixoArbitrario1();
                break;
            case 7:
                ExemploRotacionarEixoArbitrario2();
                break;
            case 8:
                ExemploTRS();
                break;
        }
    }

    private void InicializarBotoes()
    {
        botaoRotacaoX.onClick = new Button.ButtonClickedEvent();
        botaoRotacaoX.onClick.AddListener(AtivarExemploRotacionarX);
        botaoRotacaoY.onClick = new Button.ButtonClickedEvent();
        botaoRotacaoY.onClick.AddListener(AtivarExemploRotacionarY);
        botaoRotacaoZ.onClick = new Button.ButtonClickedEvent();
        botaoRotacaoZ.onClick.AddListener(AtivarExemploRotacionarZ);
        botaoRotacaoArbitrario1.onClick = new Button.ButtonClickedEvent();
        botaoRotacaoArbitrario1.onClick.AddListener(AtivarExemploRotacionarArbitrario1);
        botaoRotacaoArbitrario2.onClick = new Button.ButtonClickedEvent();
        botaoRotacaoArbitrario2.onClick.AddListener(AtivarExemploRotacionarArbitrario2);
        botaoTranslacao.onClick = new Button.ButtonClickedEvent();
        botaoTranslacao.onClick.AddListener(AtivarExemploTranslacao);
        botaoEscala.onClick = new Button.ButtonClickedEvent();
        botaoEscala.onClick.AddListener(AtivarExemploEscala);
        botaoResetar.onClick = new Button.ButtonClickedEvent();
        botaoResetar.onClick.AddListener(Resetar);
        botaoTRS.onClick = new Button.ButtonClickedEvent();
        botaoTRS.onClick.AddListener(AtivarExemploTRS);
    }
}