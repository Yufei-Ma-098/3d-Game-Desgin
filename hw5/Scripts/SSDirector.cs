using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSDirector : System.Object {

    public ISceneController currentController;

   


    private static SSDirector director;

    
    private SSDirector() { }

    public static SSDirector Director
    {
        get
        {
            if (director == null)
            {
                director = new SSDirector();
            }

            return director;
        }
        set
        {
            if (director == null)
            {
                director = new SSDirector();
            }
        }
    }

}
