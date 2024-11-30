using NAudio.CoreAudioApi;

namespace MicBooster;

public static class Program
{
    private static void Main()
    {
        Console.WriteLine("=== Microphone Volume Utility ===");

        try
        {
            SetMicrophoneVolume(100);
            Console.WriteLine("Microphone volume updated successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Sets the deault microphone volume to the specified percentage.
    /// </summary>
    /// <param name="volumePercentage">Volume level (0-100).</param>
    private static void SetMicrophoneVolume(int volumePercentage)
    {
        if (volumePercentage is < 0 or > 100)
            throw new ArgumentOutOfRangeException(nameof(volumePercentage), "Volume must be between 0 and 100.");

        using var enumerator = new MMDeviceEnumerator();
        var microphone = enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Communications);

        if (microphone == null)
            throw new InvalidOperationException("No default microphone found.");

        var volume = microphone.AudioEndpointVolume;

        var level = volumePercentage / 100f;
        volume.MasterVolumeLevelScalar = level;

        Console.WriteLine($"Microphone '{microphone.FriendlyName}' volume set to {volumePercentage}%.");
    }
}
