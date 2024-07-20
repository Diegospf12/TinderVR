using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

[System.Serializable]
public class Persona
{
    public string fotoURL;
    public string nombre;
    public string descripcion;
}

public class PersonaManager : MonoBehaviour
{
    public Persona[] personas = new Persona[]
    {
        new Persona { fotoURL = "https://images.pexels.com/photos/415829/pexels-photo-415829.jpeg?cs=srgb&dl=pexels-pixabay-415829.jpg&fm=jpg", nombre = "John Doe", descripcion = "Un desarrollador de juegos apasionado por Unity." },
        new Persona { fotoURL = "https://www.shutterstock.com/image-photo/smiling-beautiful-brunette-white-studio-260nw-428734432.jpg", nombre = "Jane Smith", descripcion = "Una artista 3D con un amor por los detalles." },
        new Persona { fotoURL = "https://static.vecteezy.com/system/resources/thumbnails/003/492/377/small/closeup-male-studio-portrait-of-happy-man-looking-at-the-camera-image-free-photo.jpg", nombre = "Juan Pedro", descripcion = "Un diseñador de niveles experto en creación de mundos." },
    };
    public Image fotoUI;       // Referencia a la imagen del UI
    public Text nombreUI;      // Referencia al texto del nombre del UI
    public Text descripcionUI; // Referencia al texto de la descripción del UI
    public Image feedbackUI;   // Referencia a la imagen de feedback (check o cross)
    public Sprite checkSprite; // Sprite de check verde
    public Sprite crossSprite; // Sprite de equis roja

    private int indiceActual = 0;

    void Start()
    {
        // Mostrar la primera persona al inicio
        MostrarPersona(indiceActual);
    }

    // Función para mostrar la persona en el índice dado
    public void MostrarPersona(int indice)
    {
        if (personas.Length == 0) return;

        Persona persona = personas[indice];
        nombreUI.text = persona.nombre;
        descripcionUI.text = persona.descripcion;
        StartCoroutine(CargarImagen(persona.fotoURL));
    }

    // Función para cargar una imagen desde una URL
    IEnumerator CargarImagen(string url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            fotoUI.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
    }

    // Función para avanzar a la siguiente persona
    public void SiguientePersona()
    {
        indiceActual++;
        if (indiceActual >= personas.Length)
        {
            indiceActual = 0; // Reiniciar al primer elemento si se pasa del último
        }
        MostrarPersona(indiceActual);
    }

    // Función para retroceder a la anterior persona
    public void AnteriorPersona()
    {
        indiceActual--;
        if (indiceActual < 0)
        {
            indiceActual = personas.Length - 1; // Ir al último elemento si se pasa del primero
        }
        MostrarPersona(indiceActual);
    }

    // Función para manejar el click del botón verde
    public void BotonVerde()
    {
        StartCoroutine(FeedbackTemporal(checkSprite));
    }

    // Función para manejar el click del botón rojo
    public void BotonRojo()
    {
        StartCoroutine(FeedbackTemporal(crossSprite));
    }

    // Corutina para mostrar feedback temporalmente y luego avanzar a la siguiente persona
    IEnumerator FeedbackTemporal(Sprite feedbackSprite)
    {
        // Ocultar la información actual
        nombreUI.enabled = false;
        descripcionUI.enabled = false;
        fotoUI.enabled = false;

        // Mostrar el feedback
        feedbackUI.sprite = feedbackSprite;
        feedbackUI.enabled = true;

        // Esperar 2 segundos
        yield return new WaitForSeconds(4);

        // Ocultar el feedback
        feedbackUI.enabled = false;

        // Mostrar la siguiente persona
        nombreUI.enabled = true;
        descripcionUI.enabled = true;
        fotoUI.enabled = true;
        SiguientePersona();
    }
}
