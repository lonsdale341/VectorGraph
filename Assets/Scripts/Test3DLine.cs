using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

public class Test3DLine : MonoBehaviour
{
    public GameObject CameraMain;
    public int segments;
    public int numberOfLines;
    public float distanceBetweenLines;
    public bool doLoop = true;
    public Texture2D lineTex;
    public Texture2D frontTex;
    public float lineWidth = 2.0f;
    List<Vector3> splinePoints;
    VectorLine spline;
    float index = 0;
    // Start is called before the first frame update
    void Start()
    {
        VectorLine.SetCamera3D(CameraMain);
        VectorLine.SetEndCap("arrow", EndCap.Both, 0, -3 , -3, 3 , lineTex, frontTex, frontTex);
        var points = new List<Vector3>();
        // Lines down X axis
        for (int i = -numberOfLines; i <= numberOfLines; i++)
        {
            if (i != 0)
            {
                points.Add(new Vector3(i * distanceBetweenLines, -((numberOfLines) * distanceBetweenLines), 0));
                points.Add(new Vector3(i * distanceBetweenLines, (numberOfLines) * distanceBetweenLines, 0));
            }

        }
        // Lines down Z axis
        for (int i = -numberOfLines; i <= numberOfLines; i++)
        {
            if (i != 0)
            {
                points.Add(new Vector3(-((numberOfLines) * distanceBetweenLines), i * distanceBetweenLines, 0));
                points.Add(new Vector3((numberOfLines) * distanceBetweenLines, i * distanceBetweenLines, 0));
            }

        }
        var line = new VectorLine("Grid", points,  lineWidth * distanceBetweenLines);
        line.SetColor(Color.white);
        line.Draw3D();
        splinePoints = new List<Vector3>();


        //spline = new VectorLine("Grid", new List<Vector3>(7000), 4);

        for (float i = -20; i <= 20; i += 0.05f)
        {
            //Debug.Log(index);
            // spline.points3[index] = new Vector3(i, i * i, 0);
            //index++;
            if ((i * i) <= numberOfLines && (i * i) >= -numberOfLines)
            {
                splinePoints.Add(new Vector3(i * distanceBetweenLines, i * i * distanceBetweenLines, 0));
            }
        }
        Debug.Log(splinePoints.Count);
        //spline = new VectorLine("Spline", new List<Vector3>(segments + 1), 6.0f, LineType.Continuous);


        var XOY_points = new List<Vector3>();
        
       // XOY_points.Add(new Vector3(0, (numberOfLines * distanceBetweenLines), 0));
       // XOY_points.Add(new Vector3(0, -(numberOfLines * distanceBetweenLines), 0));
        var XO_line = new VectorLine("XO", new List<Vector3>(), 6 * lineWidth * distanceBetweenLines);
        XO_line.points3.Add(new Vector3(-(numberOfLines * distanceBetweenLines), 0, 0));
        XO_line.points3.Add(new Vector3((numberOfLines * distanceBetweenLines), 0, 0));
        XO_line.endCap = "arrow";
        XO_line.SetColor(Color.black);
        XO_line.Draw3D();
        var YO_line = new VectorLine("YO", new List<Vector3>(), 6 * lineWidth * distanceBetweenLines);
        YO_line.points3.Add(new Vector3(0, (numberOfLines * distanceBetweenLines), 0));
        YO_line.points3.Add(new Vector3(0, -(numberOfLines * distanceBetweenLines), 0));
        YO_line.endCap = "arrow";
        YO_line.SetColor(Color.black);
        YO_line.Draw3D();
        spline = new VectorLine("Spline", splinePoints, 10 * lineWidth * distanceBetweenLines, LineType.Continuous);
        //spline.texture = lineTex;
        // spline.continuousTexture = true;
        spline.joins = Joins.Fill;

        spline.SetColor(Color.red);
        spline.endCap = "arrow";
        //spline.MakeSpline(splinePoints.ToArray());
        Debug.Log(spline.points3.Count);
        spline.Draw3D();
        var grid = GameObject.Find("Grid");
        var graph = GameObject.Find("Spline");
        var xo = GameObject.Find("XO");
        var yo = GameObject.Find("YO");
        var controller= GameObject.Find("Controller");
        grid.transform.parent = controller.transform;
        graph.transform.parent = controller.transform;
        graph.transform.localPosition = new Vector3(0,0,-0.1f);
        xo.transform.parent = controller.transform;
        yo.transform.parent = controller.transform;
        xo.transform.localPosition = new Vector3(0, 0, -0.1f);
        yo.transform.localPosition = new Vector3(0, 0, -0.1f);
        grid.transform.localPosition = new Vector3(0, 0, 0);


    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetOffset()
    {
        //spline.textureOffset = index;
        spline.SetColor(Color.red);
       // spline.joins = Joins.Weld;
        spline.Draw3D();
       // index += 0.25f;
    }
    public void SetKoef(float value)
    {
        spline.points3.Clear();
        splinePoints.Clear();

        for (float i = -50; i <= 50; i += 0.1f)
        {
            if (i == 0)
            {
                splinePoints.Add(new Vector3(i, value / 0.00000000000000000000000000000000000000001f, 0));
            }
            else
            {
                splinePoints.Add(new Vector3(i, value / i, 0));
            }

        }

        spline.points3 = splinePoints;
        //spline = new VectorLine("Spline", new List<Vector3>(segments + 1), 6.0f, LineType.Continuous);
        //spline.MakeSpline(splinePoints.ToArray(), segments, doLoop);
        spline.SetColor(Color.red);
        spline.Draw3D();
    }
}
