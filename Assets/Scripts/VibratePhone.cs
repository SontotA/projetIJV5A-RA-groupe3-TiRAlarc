using UnityEngine;
using System.Collections;

// Based on work on:
// https://gist.github.com/munkbusiness/9e0a7d41bb9c0eb229fd8f2313941564
// https://gist.github.com/aVolpe/707c8cf46b1bb8dfb363
// https://stackoverflow.com/questions/39651021/vibrate-with-duration-and-pattern/39668630#39668630

/// <summary>
/// Android Vibration support. Uses VibrationEffect on API 26 or above (Android 8)
/// with a fallback implementation
/// </summary>
public class AndroidVibration {

        
    private static AndroidJavaObject s_Vibrator;    
    private static AndroidJavaClass s_VibrationEffectClass;
    private static int s_DefaultAmplitude;
    private static bool s_UseModernAPI=false;
    private static bool s_HasAmplitudeControl=false;
    private static bool s_HasVibrator=false;
   
    const int k_ModernAPIVersion=26;

    /// <summary>
    /// Returns true of the device supports vibration
    /// </summary>
    /// <value>Vibration support</value>
    public static bool HasVibrator {
        get { return s_HasVibrator; } 
    }

    /// <summary>
    /// Returns true of the device supports amplitude control
    /// </summary>
    /// <value>Amplitude control support</value>
    public static bool HasAmplitudeControl {
        get { return s_HasAmplitudeControl; }
    }
    
    /// <summary>
    /// Initialises the class. Called before scene loaded
    /// </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialise() {
        if (Application.platform == RuntimePlatform.Android) {
            int androidAPIlevel=new AndroidJavaClass("android.os.Build$VERSION").GetStatic<int>("SDK_INT");
            s_UseModernAPI=androidAPIlevel>=k_ModernAPIVersion;
            var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            var currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            s_Vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
            s_HasVibrator = s_Vibrator.Call<bool>("hasVibrator");

            if (s_UseModernAPI) {
                s_VibrationEffectClass = new AndroidJavaClass("android.os.VibrationEffect");
                s_DefaultAmplitude = s_VibrationEffectClass.GetStatic<int>("DEFAULT_AMPLITUDE");
                s_HasAmplitudeControl=s_Vibrator.Call<bool>("hasAmplitudeControl"); 
            }
            Debug.LogFormat ("Android Vibration support: Use modern API:{0}, Supports vibration:{1}, Supports Amplitude Control:{2}",s_UseModernAPI,s_HasVibrator,s_HasAmplitudeControl);
        }
    }
    
   
    /// <summary>
    /// Creates a one time vibration
    /// </summary>
    /// <param name="milliseconds">Duration in milliseconds</param>
    /// <param name="amplitude">Strength of vibration. Between 1-255. Use -1 for default</param>
    public static void CreateOneShot(long duration, int amplitude=-1) {
                
        if (amplitude==-1) amplitude=s_DefaultAmplitude;
        //If Android 8.0 (API 26+) or never use the new vibrationeffects
        if (s_UseModernAPI) CreateVibrationEffect("createOneShot", new object[] { duration, amplitude });            
        else LegacyVibrate(duration);                    
    }
 
    /// <summary>
    /// Create vibration waveform with timings
    /// </summary>
    /// <param name="timings">Duration of each of these amplitudes in milliseconds</param>
    /// <param name="repeat">index of where to repeat, -1 for no repeat</param>
    public static void CreateWaveform(long[] timings, int repeat) {
        //Amplitude array varies between no vibration and default_vibration up to the number of timings        
        //If Android 8.0 (API 26+) or never use the new vibrationeffects
        if (s_UseModernAPI) CreateVibrationEffect("createWaveform", new object[] { timings, repeat });            
        else LegacyVibrate(timings, repeat);                           
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="timings">Duration of each of these amplitudes in milliseconds</param>
    /// <param name="amplitudes">Amplitudes for each vibration</param>
    /// <param name="repeat">index of where to repeat, -1 for no repeat</param>
    public static void CreateWaveform(long[] timings, int[] amplitudes, int repeat) {       
        //If Android 8.0 (API 26+) or never use the new vibrationeffects
        if (s_UseModernAPI) CreateVibrationEffect("createWaveform", new object[] { timings, amplitudes, repeat });            
        else LegacyVibrate(timings, repeat);                    
    }

    /// <summary>
    /// Cancels any active vibration effect
    /// </summary>
    public static void Cancel() {        
        if (s_Vibrator!=null) s_Vibrator.Call("cancel");
    }

   /// <summary>
   /// Internal implementation for Android function call
   /// </summary>
   /// <param name="function"></param>
   /// <param name="args"></param>
    private static void CreateVibrationEffect(string function, params object[] args) {
        if (s_Vibrator!=null) {
            AndroidJavaObject vibrationEffect = s_VibrationEffectClass.CallStatic<AndroidJavaObject>(function, args);
            s_Vibrator.Call("vibrate", vibrationEffect);
        }
    }

    /// <summary>
    /// Legacy implementation for API<=25
    /// </summary>
    /// <param name="duration">Duration in milliseconds</param>
    private static void LegacyVibrate(long duration) {
        if (s_Vibrator!=null) s_Vibrator.Call("vibrate", duration);
    }

    /// <summary>
    /// Legacy implementation for API<=25
    /// </summary>
    /// <param name="pattern"Duration of each of these amplitudes in millisecond></param>
    /// <param name="repeat">index of where to repeat, -1 for no repeat</param>
    private static void LegacyVibrate(long[] pattern, int repeat) {
        if (s_Vibrator!=null) s_Vibrator.Call("vibrate", pattern, repeat);
    }     

}