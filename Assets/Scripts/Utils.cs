using UnityEngine;

public class Utils
{
    public static float Remian2Decimals(float value)
    {
        if (Mathf.Abs(value) < 0.01)
            return 0;

        value *= 100;
        value = Mathf.Floor(value);
        value /= 100;

        return value;
    }
}
