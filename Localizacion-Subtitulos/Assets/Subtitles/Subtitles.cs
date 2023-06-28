using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

/*
 * El objetivo de este componente es hacer de motor de subtitulos.
 * No depende de la herramienta de internacionalización.
 * 
 * En caso de ser usado con la herramienta de internacionalización, 
 * el atributo y "fileName" será inicializado por el LocalizationManager,
 * quien tendrá la el valor correspondiente al idioma actual.
 */

public class Subtitles : MonoBehaviour
{

    // Nombre del fichero de configuracion para los subtitulos
    private string fileName;

    // Ruta de los archivos de subtitulos
    [SerializeField] string subtitlesFilesPath;

    // Texto en la UI donde van a aparecer los subtitulos 
    [SerializeField] Text subtitlesText;

    // Recuadro de fondo necesario para representar los subtitulos 
    // en caso de mala visibilidad
    [SerializeField] UnityEngine.UI.Image subImage;

    // Texto en la UI donde van a aparecer los subtitulos especiales
    // Con subtitulos especiales me refiero a los efectos sonoros, musica y canciones
    [SerializeField] Text specialSubtitlesText;

    // Recuadro de fondo necesario para representar los subtitulos especiales
    [SerializeField] UnityEngine.UI.Image specialSubImage;

    // Estructura de datos para almacenar el contenido del JSON
    private SubtitleData data;


    // Atributos para la lógica del motor

        // Array de subtítulos
        private Subtitle[] subtitles;

        // Bool para indicar si los subtitulos se están mostrando
        // Es decir, si el audio asocieda se está reproduciendo
        private bool active;
        
        // Bool para la logica para evitar cambiar el texto en todos los frames
        // en los que se reproduce un subtitulo concreto y solo realizar el cambio
        // en el frame de transición de un subtitulo a otro
        private bool textChanged;
        
        // Tiempo de audio trancurrido
        private float timeSinceAudioStarted;
        
        // Indice que indica que subtitulo se debe mostrar
        private int currentSubtitleIdx;
        
        // Numero maximo de voces participantes en una conversacion
        // soportadas por el motor de subtitulos. En caso de haber más,
        // el color con el que se representará el subtitulo será el por 
        // defecto
        private const int MAX_VOICES = 5;

    private void Start()
    {
        resetSubtitles();
    }

    private void Update()
    {
        if (active)
        {
            timeSinceAudioStarted += Time.deltaTime;

            // Si se alcanza el tiempo inicial del subtitulo que debe mostrarse
            if (timeSinceAudioStarted >= subtitles[currentSubtitleIdx].startTime && !textChanged)
            {
                changeTextToCurrentSubtitleConfiguration();

                textChanged = true;
            }

            // Si se alcanza el tiempo final del subtitulo que debe mostrarse
            if (timeSinceAudioStarted >= subtitles[currentSubtitleIdx].endTime)
            {
                currentSubtitleIdx++;
                subtitlesText.text = "";
                textChanged = false;
            }
        }

    }

    public void SetSubtitlesFile(string file)
    {
        fileName = file;
    }

    public void LoadSubtitles()
    {
        string path = subtitlesFilesPath + fileName;

        if (!File.Exists(path))
        {
            Debug.LogError("El archivo no se ha podido abrir o no existe para el idioma actual.");
            return;
        }

        string json = File.ReadAllText(path);

        // Se lee el json y se alamcena en la estructura de datos preparada para ello
        data = JsonConvert.DeserializeObject<SubtitleData>(json);

        // Lee el archivo de subtítulos linea por linea
        string[] lines = File.ReadAllLines(subtitlesFilesPath + data.file);

        int nSubtitles = data.nSubtitles;

        // Crear un arreglo para almacenar los subtítulos
        subtitles = new Subtitle[nSubtitles];

        int i = 0, j = 0;
        while (i < lines.Length)
        {
            // 1.- Obtener el número de secuencia
                int sequenceNumber = int.Parse(lines[i]);

            // 2.- Obtener los tiempo de inicio y fin
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

            // 3.- Obtener los textos de los subtitlos (pueden ser de 1 a 3 lineas)

                int c = i + 2; // Se coloca el marcador en la primera linea de subtitulos
                string subText = "";
                while (c < lines.Length && lines[c] != "")
                {
                    subText += lines[c] + "\n";
                    c++;
                }

                i = c + 1;

            // 4.- Crear el objeto subtitulo
                subtitles[j] = new Subtitle(sequenceNumber, startTime, endTime, subText);

                j++;
        }

    }

    private void changeTextToCurrentSubtitleConfiguration()
    {
        resetSubtitles();

        // Se obtiene el texto del subtitulo actual
        string text = subtitles[currentSubtitleIdx].text;

        // El + 1 se realiza porque en el formato .srt el primer subtitulo empieza con el indice 1
        // pero en mi representacion currentSubtitleIdx empieza en 0 (por comodidad a la hora de programar)
        int idx = currentSubtitleIdx + 1;

        // Si es un subtitulo especial (sonido, cancion, musica)
        if (data.sounds.Contains(idx))
        {
            // Se asigna a los subtitulos especiales el texto del subtitulo actual y color de sonidos, musica y canciones
            specialSubtitlesText.text = text;
            specialSubtitlesText.color = SubtitlesColors.soundColor;
            specialSubImage.enabled = true;

            return;
        }

        for (int i = 0; i < data.characters.Count; i++)
        {   
            // Si es un subtitulo normal
            string speaker = "Speaker" + (i + 1).ToString();
            if (data.characters[i][speaker].subtitles.Contains(idx))
            {
                // Se asigna a los subtitulos normales el texto del subtitulo actual
                subtitlesText.text = text;

                // En caso de ser un subtitulo con poca visibilidad se aplica fondo con un color de contraste
                if (data.visibility.Contains(idx))
                    subImage.enabled = true;

                int priority = data.characters[i][speaker].priority;

                // Se asigna el color (si hay mas voces que posibles colores 
                // para representar se utiliza el color por defecto)
                if (priority < MAX_VOICES)
                    subtitlesText.color = SubtitlesColors.colors[priority];
                else
                    subtitlesText.color = SubtitlesColors.defaultColor;

                return;
            }
        }
    }

    private void resetSubtitles()
    {
        subtitlesText.text = "";
        specialSubtitlesText.text = "";
        subImage.enabled = false;
        specialSubImage.enabled = false;
    }

    public void BeginSubtitles()
    {
        resetSubtitles();

        active = true;
        textChanged = false;

        timeSinceAudioStarted = 0.0f;
        currentSubtitleIdx = 0;
    }

    public void EndSubtitles()
    {
        active = false;
    }

}

