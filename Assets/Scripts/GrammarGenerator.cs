using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;


public enum MyceliumKind
{
    Organic,
    Asymetric,
    Tridimensional,
    Creeping, //rampant
    Interlace
}

public class GrammarGenerator : MonoBehaviour
{
    public MyceliumKind myceliumKind;
    public int nbiterations = 3;
    private List<string> rules = new List<string>();
    void Start()
    {
        switch (myceliumKind) 
        { 
            case MyceliumKind.Organic:
                rules.Add("F[+F]F[*F]F[-F]F[/F]");
                rules.Add("F[-F]F[/F]");
                rules.Add("F[*F][-F]");
                break;
            case MyceliumKind.Asymetric:
                rules.Add("F[+F]F[*F]F[/F]");
                rules.Add("F[-F]F[*F]");
                rules.Add("FF[+F][/F]");
                break;
            case MyceliumKind.Tridimensional:
                rules.Add("F[+F][-F][*F][/F]");
                rules.Add("F*F[+F][-F]");
                rules.Add("F[*F][-F]/F");
                rules.Add("F[**F]F[/F]");
                break;
            case MyceliumKind.Creeping:
                rules.Add("FF[+F][*F]F");
                rules.Add("F[/F]F[-F]");
                rules.Add("FF[*F][/F]");
                break;
            case MyceliumKind.Interlace:
                rules.Add("F[+F[*F]]F");
                rules.Add("F[-F[/F]]F");
                rules.Add("F[*F][+F][/F]");
                break;
        }
    }

    public string GenerateCommand()
    {
        string command = "F";
        for (int i = 0; i < nbiterations; i++)
        {
            StringBuilder next = new StringBuilder();
            foreach (char c in command)
            {
                if (rules != null && rules.Count > 0 && c =='F')
                {
                    int index = Random.Range(0, rules.Count);
                    next.Append(rules[index]);
                }
                else
                {
                    next.Append(c);
                }
            }
            command = next.ToString();
        }
        return command;
    }
}
