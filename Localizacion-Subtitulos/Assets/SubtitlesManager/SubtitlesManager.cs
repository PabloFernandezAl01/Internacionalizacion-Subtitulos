using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Subtitle
{
    public int sequenceNumber;
    public float startTime;
    public float endTime;
    public string text;

    public Subtitle(int sequenceNumber, float startTime, float endTime, string text)
    {
        this.sequenceNumber = sequenceNumber;
        this.startTime = startTime;
        this.endTime = endTime;
        this.text = text;
    }
}

public class SubtitlesManager : MonoBehaviour
{
    [SerializeField] string file;
    public Text subtitlesText;
    private Subtitle[] subtitles;
    private Coroutine displaySubtitlesCoroutine;

    void Start()
    {
        // Cargar el archivo de subt�tulos
        LoadSubtitles(file);
    }

    void LoadSubtitles(string filePath)
    {
        // Leer el archivo de subt�tulos
        string[] lines = File.ReadAllLines("Assets/Resources/Subtitles/" + filePath);

        // Crear un arreglo para almacenar los subt�tulos
        subtitles = new Subtitle[lines.Length / 4];

        // Procesar cada l�nea del archivo
        for (int i = 0, j = 0; i < lines.Length; i += 4, j++)
        {
            // Obtener el n�mero de secuencia
            int sequenceNumber = int.Parse(lines[i]);

            // Obtener los tiempos de inicio y fin
            string[] timeValues = lines[i + 1].Split(new char[] { '-', '>', ':', ',' }, System.StringSplitOptions.RemoveEmptyEntries);
            float startTime = float.Parse(timeValues[0]) * 60 + float.Parse(timeValues[1]) + float.Parse(timeValues[2]) / 1000f;
            float endTime = float.Parse(timeValues[3]) * 60 + float.Parse(timeValues[4]) + float.Parse(timeValues[5]) / 1000f;

            // Obtener el texto del subt�tulo
            string subtitleText = lines[i + 2];

            // Verificar si hay suficiente espacio en el arreglo
            if (j < subtitles.Length)
            {
                // Crear el objeto de subt�tulo
                subtitles[j] = new Subtitle(sequenceNumber, startTime, endTime, subtitleText);
            }
            else
            {
                Debug.LogError("�ndice de subt�tulo fuera de los l�mites del arreglo: " + j);
            }   
        }
    }


    public void PlaySubtitles()
    {
        if (displaySubtitlesCoroutine != null)
        {
            StopCoroutine(displaySubtitlesCoroutine);
        }

        displaySubtitlesCoroutine = StartCoroutine(DisplaySubtitles());
    }

    IEnumerator DisplaySubtitles()
    {
        foreach (Subtitle subtitle in subtitles)
        {
            subtitlesText.text = subtitle.text;

            // Esperar hasta que sea el momento de mostrar el siguiente subt�tulo
            yield return new WaitForSeconds(subtitle.endTime - subtitle.startTime);
        }

        // Borrar el texto de los subt�tulos al finalizar
        subtitlesText.text = "";
    }

}

