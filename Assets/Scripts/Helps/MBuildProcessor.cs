using System;
//using UnityEditor;
//using UnityEditor.Build;
//using UnityEditor.Build.Reporting;
using UnityEngine;

//class MBuildProcessor : IPreprocessBuildWithReport
//{
//    public int callbackOrder { get { return 0; } }
//    public void OnPreprocessBuild(BuildReport report)
//    {
//        throw new Exception("Σφάλμα κατά λάθος, αντιμετωπίζετε ένα σφάλμα που έγινε για να σας εξαπατήσει, προσπαθήστε να λύσετε αυτό το πρόβλημα");
//        Debug.Log("Σφάλμα σκόπιμα, το βρήκατε, συγχαρητήρια!");
//    }

//}

public static class Stick {

    public enum stck {ONE_METHOD, MANESTIC }

    public static stck GetStck()
    {
        return Application.platform == RuntimePlatform.WindowsEditor ? stck.MANESTIC : stck.ONE_METHOD;
    }

}

