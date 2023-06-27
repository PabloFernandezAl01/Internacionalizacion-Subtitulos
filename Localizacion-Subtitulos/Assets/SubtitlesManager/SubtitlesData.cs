using System.Collections.Generic;
using UnityEngine;

// Clases para mapear la estructura del JSON
[System.Serializable]
public class SubtitleData
{
    public List<int> sounds;
    public int nSubtitles;
    public List<Dictionary<string, SpeakerData>> characters;
    public string file;
}

[System.Serializable]
public class SpeakerData
{
    public List<int> subtitles;
    public int priority;
}

// Clase estatica con miembros estaticos para guardar la informacion
// de los colores de texto que corresponden a cada voz/persona
// Siguen la norma española de subtitulado para sordos UNE-153010: 2012
public static class SubtitlesColors
{
    public static Color defaultColor = Color.white;

    public static Color soundColor = Color.blue;

    public static Color[] colors = new Color[]
    {
        Color.yellow,
        Color.green,
        Color.cyan,
        Color.magenta,
        Color.white
    };
}


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
