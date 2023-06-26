using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/*
 * Esta clase se utiliza para representar la informacion
 * de una linea de subtitulos. Contiene informacion como
 * el los momentos de inicio y fin en los que la linea 
 * se debe mostrar, la línea en sí, etc.
 */
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

/*
 * El objetivo de este componente es hacer de motor de subtitulos.
 * No depende de la herramienta de internacionalización.
 * 
 * En caso de ser usado con la herramienta de internacionalización, 
 * el atributo y "fileName" será inicializado por el LocalizationManager,
 * quien tendrá la el valor correspondiente al idioma actual.
 */

public class SubtitlesManager : MonoBehaviour
{

    // Nombre del fichero de configuracion para los subtitulos
    public string fileName;

    // Ruta de los archivos de subtitulos
    [SerializeField] string subtitlesFilesPath;

    // Texto en la UI donde van a aparecer los subtitulos
    private Text subtitlesText;

    // Atributos para la lógica del motor
    private Subtitle[] subtitles;

    // Intervalo de tiempo en el que se recogen los subtitulos
    // Por ejemplo, si es 5 segundos y hay un subtitulo en el segundo 1
    // y otro en el segundo 4, se mostrarán los dos a la vez. Pero en caso
    // de que el segundo se encuentre en el segundo 6, solo se mostrará
    // el primero.
    [SerializeField] float timeInterval;

    // Bool para indicar si los subtitulos se están mostrando
    // Es decir, si el audio se está reproduciendo
    private bool active;

    private float timeSinceAudioStarted;

    private int currentSubtitleIdx;

    private void Start()
    {
        subtitlesText = GetComponent<Text>();

        LoadSubtitles();

        BeginSubtitles();
    }

    private void Update()
    {
        if (active)
        {
            timeSinceAudioStarted += Time.deltaTime;

            if (timeSinceAudioStarted >= subtitles[currentSubtitleIdx].startTime)
            {
                subtitlesText.text = subtitles[currentSubtitleIdx].text;

                if (timeSinceAudioStarted >= subtitles[currentSubtitleIdx].endTime)
                {
                    currentSubtitleIdx++;
                    subtitlesText.text = "";
                }
            }
        }


    }

    public void LoadSubtitles()
    {
        string path = subtitlesFilesPath + fileName;

        if (!File.Exists(path)) return;

        // Leer el archivo de subtítulos
        string[] lines = File.ReadAllLines(subtitlesFilesPath + fileName);

        // Crear un arreglo para almacenar los subtítulos
        subtitles = new Subtitle[lines.Length / 4 + 1];

        for (int i = 0, j = 0; i < lines.Length; i+= 4, j++)
        {
            // Obtener el número de secuencia
            int sequenceNumber = int.Parse(lines[i]);

            string times = lines[i + 1];

            // Divide la cadena en tiempo inicial y tiempo final
            string[] tiempos = times.Split(new string[] { " --> " }, StringSplitOptions.RemoveEmptyEntries);

            // Reemplaza la coma por un punto en ambos tiempos para que TimeSpan reciba el string de texto bien formateado
            tiempos[0] = tiempos[0].Replace(",", ".");
            tiempos[1] = tiempos[1].Replace(",", ".");

            // Parsea los tiempos en formato TimeSpan
            TimeSpan tiempoInicial = TimeSpan.Parse(tiempos[0]);
            TimeSpan tiempoFinal = TimeSpan.Parse(tiempos[1]);

            // Convierte los tiempos a segundos
            float startTime = (float) tiempoInicial.TotalSeconds;
            float endTime = (float) tiempoFinal.TotalSeconds;

            string text = lines[i + 2];

            subtitles[j] = new Subtitle(sequenceNumber, startTime, endTime, text);

        }

    }

    public void BeginSubtitles()
    {
        active = true;

        timeSinceAudioStarted = 0.0f;
        currentSubtitleIdx = 0;
    }

    public void EndSubtitles()
    {
        active = false;
    }

}

