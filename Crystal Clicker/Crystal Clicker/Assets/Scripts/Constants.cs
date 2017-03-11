using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants  {


    public enum GemColor {
        Red,
        Green,
        Blue,
        Yellow,
        Purple,
        All
    }




    public static GameObject GetParticleByColor(GemColor myColor) {
        GameObject fragment = null;

        switch (myColor) {
            case GemColor.Red:

                fragment = Resources.Load("Particles/RedFragmentParticle") as GameObject;
                break;

            case GemColor.Yellow:

                fragment = Resources.Load("Particles/YellowFragmentParticle") as GameObject;
                break;

            case GemColor.Green:

                fragment = Resources.Load("Particles/GreenFragmentParticle") as GameObject;
                break;

            case GemColor.Blue:
                fragment = Resources.Load("Particles/BlueFragmentParticle") as GameObject;

                break;

            default:

                fragment = Resources.Load("Particles/BlueFragmentParticle") as GameObject;
                break;
        }
        return fragment;
    }


}
