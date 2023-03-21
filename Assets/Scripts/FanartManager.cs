using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FanartManager : MonoBehaviour
{
    List<Texture> fanart1x1 = new List<Texture>();
    List<Texture> fanart9x16 = new List<Texture>();
    List<Texture> fanart4x3 = new List<Texture>();
    Material mat1x1, mat9x16, mat4x3;
    int timer = 0;
    int frames = 60;
    int fadeFrames = 5;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        mat1x1 = GameObject.Find("fanart1x1cube").GetComponent<Renderer>().material;
        mat1x1.SetColor("_Color", new Color(0, 0, 0));
        mat9x16 = GameObject.Find("fanart9x16cube").GetComponent<Renderer>().material;
        mat9x16.SetColor("_Color", new Color(0, 0, 0));
        mat4x3 = GameObject.Find("fanart4x3cube").GetComponent<Renderer>().material;
        mat4x3.SetColor("_Color", new Color(0, 0, 0));

        foreach (string path in System.IO.Directory.GetFiles(Application.dataPath + "/Textures/fanart1x1/", "*.png" ))
        {
            fanart1x1.Add(PngToTex2D(path));
        }
        foreach (string path in System.IO.Directory.GetFiles(Application.dataPath + "/Textures/fanart9x16/", "*.png" ))
        {
            fanart9x16.Add(PngToTex2D(path));
        }
        foreach (string path in System.IO.Directory.GetFiles(Application.dataPath + "/Textures/fanart4x3/", "*.png" ))
        {
            fanart4x3.Add(PngToTex2D(path));
        }

        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (frames == 0)
        {
            mat1x1.SetTexture("_MainTex", fanart1x1[0]);
            mat9x16.SetTexture("_MainTex", fanart9x16[0]);
            mat4x3.SetTexture("_MainTex", fanart4x3[0]);

        }
        else if (timer % frames == 0)
        {
            int index1x1 = (timer / frames) % fanart1x1.Count;
            mat1x1.SetTexture("_MainTex", fanart1x1[index1x1]);
            int index9x16 = (timer / frames) % fanart9x16.Count;
            mat9x16.SetTexture("_MainTex", fanart9x16[index9x16]);
            int index4x3 = (timer / frames) % fanart4x3.Count;
            mat4x3.SetTexture("_MainTex", fanart4x3[index4x3]);
        }
        
        if (timer % frames < fadeFrames)
        {
            FadeFromBlack(mat1x1);
            FadeFromBlack(mat9x16);
            FadeFromBlack(mat4x3);
        }
        else if (timer % frames >= frames - fadeFrames)
        {
            FadeToBlack(mat1x1);
            FadeToBlack(mat9x16);
            FadeToBlack(mat4x3);
        }


        timer++;
    }

    Texture2D PngToTex2D (string path)
    {
        BinaryReader bin = new BinaryReader(new FileStream(path, FileMode.Open, FileAccess.Read));
        byte[] rb = bin.ReadBytes((int)bin.BaseStream.Length);
        bin.Close();
        int pos = 16, width = 0, height = 0;
        for (int i = 0; i < 4; i++) width  = width  * 256 + rb[pos++];
        for (int i = 0; i < 4; i++) height = height * 256 + rb[pos++];
        Texture2D texture = new Texture2D(width, height);
        texture.LoadImage(rb);
        return texture;
    }

    void FadeToBlack(Material m)
    {
        Color c = m.GetColor("_Color");
        if (c.r - 1.0f / fadeFrames >= 0)
        {
            m.SetColor("_Color", new Color(c.r - 1.0f / fadeFrames, c.g - 1.0f / fadeFrames, c.b - 1.0f / fadeFrames));
        }
        else 
        {
            m.SetColor("_Color", new Color(0, 0, 0));
        }
    }

    void FadeFromBlack(Material m)
    {
        Color c = m.GetColor("_Color");
        if (c.r + 1.0f / fadeFrames <= 1)
        {
            m.SetColor("_Color", new Color(c.r + 1.0f / fadeFrames, c.g + 1.0f / fadeFrames, c.b + 1.0f / fadeFrames));
        }
        else 
        {
            m.SetColor("_Color", new Color(1, 1, 1));
        }
    }
}
