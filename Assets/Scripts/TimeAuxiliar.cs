using UnityEngine;

public static class TimeAuxiliar {
    public static void StopTime() {
        Time.timeScale = 0;
    }

    public static void ResumeTime() {
        Time.timeScale = 1;
    }
}