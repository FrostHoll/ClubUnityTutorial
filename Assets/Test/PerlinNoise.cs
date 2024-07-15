using System;
using UnityEngine;

public static class PerlinNoise
{
    // Width and height of the texture in pixels.
    private static int pixWidth = 2048;
    private static int pixHeight = 2048;

    // The number of cycles of the basic noise pattern that are repeated
    // over the width and height of the texture.
    private static float scale = 100f;

    private static Texture2D texture;

    public static float[,] GetPerlinNoise(float xOffset, float yOffset)
    {
        texture = new Texture2D(pixWidth, pixHeight);
        CalcNoise(texture, xOffset, yOffset);
        return TextureToFloatArray2D(texture);
    }

    private static void CalcNoise(Texture2D noiseTex, float xOrg, float yOrg)
    {
        Color[] pix = new Color[noiseTex.width * noiseTex.height];

        // For each pixel in the texture...
        for (float y = 0.0F; y < noiseTex.height; y++)
        {
            for (float x = 0.0F; x < noiseTex.width; x++)
            {
                float xCoord = xOrg + x / noiseTex.width * scale;
                float yCoord = yOrg + y / noiseTex.height * scale;
                float sample = Mathf.PerlinNoise(xCoord, yCoord);
                
                pix[(int)y * noiseTex.width + (int)x] = new Color(sample, sample, sample);
            }
        }
        
        // Copy the pixel data to the texture and load it into the GPU.
        noiseTex.SetPixels(pix);
        noiseTex.Apply();
    }

    private static float[,] TextureToFloatArray2D(Texture2D texture)
    {
        int width = texture.width;
        int height = texture.height;
        float[,] floatArray = new float[width, height];

        Color[] pixels = texture.GetPixels();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // Преобразование цвета в значение float (например, используя канал R)
                floatArray[x, y] = pixels[y * width + x].grayscale;
            }
        }

        return floatArray;
    }
}