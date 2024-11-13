using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TensorFlowLite;

public class ImageClassifier : MonoBehaviour
{
    public Button selectImageButton;
    public SalsaMaster salsaMaster;
    private Interpreter interpreter;
    private const string modelPath = "cnn_custom.tflite";

    private readonly string[] labels = new string[]
    {
        "camiseta_amarilla", "camiseta_azul", "camiseta_blanca",
        "camiseta_roja", "vestido_rojo", "vestido_azul",
        "vestido_amarillo", "vestido_blanco"
    };

    void Start()
    {
        try
        {
            InitializeInterpreter();

            selectImageButton.onClick.AddListener(() => SelectAndClassifyImage());
        }
        catch (Exception e)
        {
            Debug.LogError($"Error al inicializar el modelo: {e.Message}");
        }
    }

    void InitializeInterpreter()
    {
        if (interpreter != null)
        {
            interpreter.Dispose();
        }

        string fullPath = Path.Combine(Application.streamingAssetsPath, modelPath);
        byte[] modelData = File.ReadAllBytes(fullPath);

        InterpreterOptions options = new InterpreterOptions();
        options.threads = 1;

        interpreter = new Interpreter(modelData, options);
        interpreter.AllocateTensors();

        int[] inputShape = interpreter.GetInputTensorInfo(0).shape;
        Debug.Log("Dimensión esperada de entrada: " + string.Join(", ", inputShape));

        int[] outputShape = interpreter.GetOutputTensorInfo(0).shape;
        Debug.Log("Dimensión esperada de salida: " + string.Join(", ", outputShape));
    }

    void SelectAndClassifyImage()
    {
        string path = OpenFilePanel("Seleccione una imagen", "", "jpg,png");
        if (string.IsNullOrEmpty(path)) return;

        Texture2D image = LoadImage(path);
        if (image == null)
        {
            Debug.LogError("Error al cargar la imagen.");
            return;
        }

        Texture2D resizedImage = ResizeTexture(image, 128, 128);

        Debug.Log($"Pixel [0,0]: R={resizedImage.GetPixel(0, 0).r}, G={resizedImage.GetPixel(0, 0).g}, B={resizedImage.GetPixel(0, 0).b}");

        // Preparar el tensor de entrada
        float[,,,] input = PrepareInput(resizedImage);
        if (input == null)
        {
            Debug.LogError("Error al preparar el tensor de entrada.");
            return;
        }

        // Reinicializar intérprete para garantizar limpieza
        InitializeInterpreter();

        // Ejecutar inferencia
        float[] result = RunInference(input);
        if (result == null)
        {
            Debug.LogError("Error al ejecutar la inferencia.");
            return;
        }

        // Determinar clase predicha
        int predictedClass = GetPredictedClass(result);
        Debug.Log($"Clase predicha: {labels[predictedClass]}, Probabilidad: {result[predictedClass]}");
        switch (predictedClass)
            {
                case 0: salsaMaster.ChangeToCamisetaAmarilla(); break;
                case 1: salsaMaster.ChangeToCamisetaAzul(); break;
                case 2: salsaMaster.ChangeToCamisetaBlanca(); break;
                case 3: salsaMaster.ChangeToCamisetaRoja(); break;
                case 4: salsaMaster.ChangeToVestidoRojo(); break;
                case 5: salsaMaster.ChangeToVestidoAzul(); break;
                case 6: salsaMaster.ChangeToVestidoAmarillo(); break;
                case 7: salsaMaster.ChangeToVestidoBlanco(); break;
                default: Debug.LogWarning("Clase no reconocida."); break;
            }

    }

    float[,,,] PrepareInput(Texture2D image)
    {
        float[,,,] input = new float[1, 128, 128, 3];
        for (int y = 0; y < 128; y++)
        {
            for (int x = 0; x < 128; x++)
            {
                Color pixel = image.GetPixel(x, y);
                input[0, y, x, 0] = pixel.r;
                input[0, y, x, 1] = pixel.g;
                input[0, y, x, 2] = pixel.b;
            }
        }

        Debug.Log($"Pixel [0,0] después de preparar el tensor: R={input[0, 0, 0, 0]}, G={input[0, 0, 0, 1]}, B={input[0, 0, 0, 2]}");
        return input;
    }

    float[] RunInference(float[,,,] input)
    {
        try
        {
            interpreter.SetInputTensorData(0, input);
            interpreter.Invoke();

            int outputSize = interpreter.GetOutputTensorInfo(0).shape[1];
            float[] output = new float[outputSize];
            interpreter.GetOutputTensorData(0, output);

            Debug.Log("Probabilidades:");
            for (int i = 0; i < output.Length; i++)
            {
                Debug.Log($"Clase {i}: Probabilidad (normalizada): {output[i]}");
            }

            return output;
        }
        catch (Exception e)
        {
            Debug.LogError($"Error al ejecutar la inferencia: {e.Message}");
            return null;
        }
    }

    int GetPredictedClass(float[] output)
    {
        int predictedClass = 0;
        float maxProbability = output[0];

        for (int i = 1; i < output.Length; i++)
        {
            if (output[i] > maxProbability)
            {
                maxProbability = output[i];
                predictedClass = i;
            }
        }

        Debug.Log($"Clase con mayor probabilidad: {predictedClass}, Probabilidad: {maxProbability}");
        return predictedClass;
    }

    Texture2D LoadImage(string filePath)
    {
        byte[] fileData = File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(fileData);
        return texture;
    }

    Texture2D ResizeTexture(Texture2D texture, int width, int height)
    {
        RenderTexture rt = new RenderTexture(width, height, 24);
        RenderTexture.active = rt;
        Graphics.Blit(texture, rt);
        Texture2D result = new Texture2D(width, height);
        result.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        result.Apply();
        return result;
    }

    string OpenFilePanel(string title, string directory, string extension)
    {
#if UNITY_EDITOR
        return UnityEditor.EditorUtility.OpenFilePanel(title, directory, extension);
#else
        return "";
#endif
    }

    private void OnDestroy()
    {
        interpreter?.Dispose();
    }
}